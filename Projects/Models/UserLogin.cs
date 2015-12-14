using System.ComponentModel.DataAnnotations;

namespace Projects.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
    }
}