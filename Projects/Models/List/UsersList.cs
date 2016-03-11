using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Projects.Models
{
    public class UsersList
    {

        public List<SelectListItem> DataList { get; set; }=DB.GetSeletUsersList();
        [Required(ErrorMessage = "Every task must have one user at less")]
        public int[] SelectedIndex { get; set; } 
        public UsersList()
        {
            
        }
    }
}