using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Projects.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Crete Date")]
        public DateTime CreteDate { get; set; }
        [Required]
        [Display(Name = "Daed Line")]
        public DateTime DaedLine { get; set; }
        public string Description { get; set; }
        public int Parent { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public Project()
        {
            CreteDate = DaedLine = DateTime.Now;

        }

    }
}