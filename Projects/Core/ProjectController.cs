using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Projects.Models;

namespace Projects
{
    public class ProjectController : Controller
    {

        /// <summary>
        /// Check if the user login
        /// </summary>
        /// <returns>False if not login</returns>

        public bool IsLogin()
        {
            if (Session[SessionNames.User] == null || (Session[SessionNames.User] as User).Id == 0) return false;

            return true;
        }


    }
}