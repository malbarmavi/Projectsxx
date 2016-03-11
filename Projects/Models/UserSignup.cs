using System.ComponentModel.DataAnnotations;
namespace Projects.Models
{
    public class UserSignup : User
    {
        [Display(Name = "Company Name")]
        [Required]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Company Type")]
        public string CompanyType { get; set; }
    }
}