using System.ComponentModel.DataAnnotations;

namespace WdypApplication.Models
{
    public class RegisterViewModel
    {
        public int Id { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Login is required")]
        public string Name { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]

        [Required(ErrorMessage = "Password is required")]
        public string PassWord { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirming Password is required")]
        [Compare("PassWord", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassWord { get; set; }
    }
}
