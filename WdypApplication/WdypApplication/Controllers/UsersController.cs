using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WdypApplication.Data;
using WdypApplication.Models;

namespace WdypApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        // adicionar context do DB.
        public UsersController(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
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

        // POST: Users/Register
        // Cadastrar usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Name,PassWord,ConfirmPassWord")] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    UserName = model.Name,
                    Name = model.Name
                };

                var hashedPassword = _passwordHasher.HashPassword(newUser, model.PassWord);
                newUser.PasswordHash = hashedPassword;

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
                    .FirstOrDefaultAsync(u => u.Name.ToLower() == model.Name.ToLower());

                if (existingUser != null)
                {
                    var result = _passwordHasher.VerifyHashedPassword(existingUser, existingUser.PasswordHash, model.PassWord);
                    if (result == PasswordVerificationResult.Success)
                    {
                        // Usuário encontrado e senha verificada, redirecionar para a página inicial ou outra página
                        return RedirectToAction("Index", "Home");
                    }
                }

                // Usuário não encontrado ou senha inválida, adicionar mensagem de erro
                ModelState.AddModelError(string.Empty, "Nome de usuário ou senha inválidos.");
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