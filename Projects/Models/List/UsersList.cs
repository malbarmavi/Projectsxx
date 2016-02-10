using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Projects.Models
{
    public class UsersList
    {

        public List<SelectListItem> DataList { get; set; } = DB.GetSeletUsersList();
        [Required(ErrorMessage = "Every task must have one user at less")]
        public int[] SelectedIndex { get; set; }
    }
}