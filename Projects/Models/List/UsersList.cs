using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projects.Models
{
    public class UsersList
    {

        public List<SelectListItem> DataList { get; set; } = DB.GetSeletUsersList();
        public int[] SelectedIndex { get; set; }
    }
}