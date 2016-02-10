using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projects.Models
{
    //public class ProjectsList
    //{
    //    public List<SelectListItem> DataList { get; set; } = new List<SelectListItem>();
    //    public int SelectedIndex { get; set; }
    //}


    public class ProjectInfo : Project
    {


        public int TaskCount { get; set; }
        public int Undecided { get; set; }
        public int InProcess { get; set; }
        public int Faile { get; set; }
        public int Success { get; set; }
        public Double Precent { get; set; }


    }
}