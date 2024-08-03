using System.ComponentModel.DataAnnotations;

namespace WdypApplication.Models
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Login is required")]
        public string Name { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string PassWord { get; set; }


        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirming Password is required")]
        [Compare("PassWord", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassWord { get; set; }

        public User()
        {
        }

        public User(int id, string name, string passWord, string confirmpassword)
        {
            Id = id;
            Name = name;
            PassWord = passWord;
            ConfirmPassWord = confirmpassword;
        }
    }
}
