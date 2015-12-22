using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projects.Models
{
    public class ProjectsList
    {
        public List<SelectListItem> DataList { get; set; } = new List<SelectListItem>();
        public int SelectedIndex { get; set; }
    }

}