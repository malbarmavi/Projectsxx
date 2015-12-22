using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Projects.Models
{
    public class ProjectTask
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public TaskState State { get; set; }
        [Required]
        public PriorityState Priority { get; set; }
        [Required]
        public int Project_id { get; set; }
        public UsersList Users { get; set; } = new UsersList();
    }

}