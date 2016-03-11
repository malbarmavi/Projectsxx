using System;
using System.Collections.Generic;

namespace Projects.Models
{
    public class TasksCollection
    {
        public String ProjectName { get; set; }
        public List<TaskInfo> TasksList { get; set; }
    }
}