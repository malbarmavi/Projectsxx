using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Projects.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string About { get; set; }
        [Display(Name = "Facebook Link")]
        public string FacebookUrl { get; set; }
        [Display(Name = "Twitter Link")]
        public string TwitterUrl { get; set; }
        [Display(Name = "Website Link")]
        public string WebsiteUrl { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

    }
}