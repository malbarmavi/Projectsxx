using System.ComponentModel.DataAnnotations;
namespace Projects.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "User Role")]
        public UserRole Role { get; set; }
        public bool CEO { get; set; } = false;
        public int CompanyId { get; set; }
        public string Notes { get; set; }
    }
}