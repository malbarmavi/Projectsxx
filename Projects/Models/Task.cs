using System.ComponentModel.DataAnnotations;

namespace Projects.Models
{
    public class ProjectTask
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public TaskState State { get; set; }
        [Required]
        public PriorityState Priority { get; set; }
        [Required]
        public int Project_id { get; set; }
        public UsersList Users { get; set; } = new UsersList();

        public ProjectTask()
        {
            this.Priority = PriorityState.Normal;

        }
    }

}