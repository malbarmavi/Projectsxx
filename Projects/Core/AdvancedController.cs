using System.Web.Mvc;
using Projects.Models;

namespace Projects
{
    public class AdvancedController : Controller
    {

        /// <summary>
        /// Check if the user login
        /// </summary>
        /// <returns>False if not login</returns>

        public bool IsLogin() => Session[SessionNames.User] != null && (Session[SessionNames.User] as User).Id != 0;




    }
}