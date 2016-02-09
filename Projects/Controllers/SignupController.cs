using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projects.Controllers
{
    public class SignupController : Projects.AdvancedController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (IsLogin()) return RedirectToAction("index", "Dashboard");
            //save to database
            if (ModelState.IsValid)
            {
                if (DB.IsUserExsist(user.UserName))
                {
                    View(user);
                }
                bool state = DB.CreateUser(user);
                if (state == true)
                {
                    return RedirectToAction("index", "Login");
                }
            }

            return View(user);
        }
    }
}