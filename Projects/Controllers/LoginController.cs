using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projects.Controllers
{
    public class LoginController : ProjectController
    {
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(UserLogin loginData)
        {
            if (IsLogin()) return RedirectToAction("index", "Dashboard");

            if (ModelState.IsValid)
            {
                var userData = DB.GetUser(loginData);
                if (userData.Id > 0)
                {
                    Session[SessionNames.User] = userData;
                    return RedirectToAction("index", "Dashboard");
                }
                else
                {
                    loginData.ErrorMessage = "That account doesn't exist";
                    return View(loginData);
                }

            }


            return View(loginData);

        }
    }
}