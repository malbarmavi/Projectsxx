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
        public DateTime CreteDate { get; set; }
        [Required]
        public DateTime DaedLine { get; set; }
        public string Description { get; set; }
        public int Parent { get; set; }
        public int UserId { get; set; }


    }
}