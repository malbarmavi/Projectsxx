using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projects.Models
{
    public class TaskInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string priority { get; set; }
        public string ProjectName { get; set; }
    }
}