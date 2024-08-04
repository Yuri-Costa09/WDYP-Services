using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WdypApplication.Data;
using WdypApplication.Models;

namespace WdypApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        // adicionar context do DB.
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        // GET: Users/Create
        // Pagina de cadastro de usuario
        public IActionResult Create()
        {
            return View();
        }


        // POST: Users/Create
        // Cadastrar usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Name,PassWord,ConfirmPassWord")] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    Name = model.Name,
                    PassWord = model.PassWord
                };

                _context.Add(newUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        // POST: Users/Login
        // Logar usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Name,PassWord")] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Name.ToLower() == model.Name.ToLower() && u.PassWord == model.PassWord);

                if (existingUser != null)
                {
                    // Usuário encontrado, redirecionar para a página inicial ou outra página
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Usuário não encontrado, adicionar mensagem de erro
                    ModelState.AddModelError(string.Empty, "Nome de usuário ou senha inválidos.");
                }
            }
            return View(model);
        }


        // GET: Users/Delete/5
        // Deletar usuario
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
