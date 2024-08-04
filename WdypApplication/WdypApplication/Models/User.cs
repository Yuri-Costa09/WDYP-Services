using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WdypApplication.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Login is required")]
        public string Name { get; set; }

        public User()
        {
        }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
            
           
        }
    }
}
