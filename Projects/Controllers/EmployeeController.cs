using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projects.Models;

namespace Projects.Controllers
{
    public class EmployeeController : Projects.AdvancedController
    {
        // GET: Employee
        public ActionResult Index()
        {
            if (!IsLogin()) return RedirectToAction("Index", "Login");
            if (((Models.User)Session[SessionNames.User]).Role == UserRole.Employee)
            {
                return RedirectToAction("index", "Dashboard");
            }
            else
            {

                //list of Em.
                return View(DB.GetUserList());
            }
        }

        public ActionResult Add()
        {
            if (!IsLogin()) return RedirectToAction("Index", "Login");
            if (((Models.User)Session[SessionNames.User]).Role == UserRole.Employee) return RedirectToAction("index", "Dashboard");

            //Add
            return View();
        }
        [HttpPost]
        public ActionResult Add(User user)
        {
            if (((Models.User)Session[SessionNames.User]).Role == UserRole.Employee) return RedirectToAction("index", "Dashboard");
            if (ModelState.IsValid)
            {

                if (DB.IsUserExsist(user.UserName))
                {
                    View(user);
                }
                bool state = DB.CreateUser(user);
                if (state == true)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (!IsLogin()) return RedirectToAction("Index", "Login");
            if (((Models.User)Session[SessionNames.User]).Role == UserRole.Employee) return RedirectToAction("index", "Dashboard");



            return RedirectToAction("index");
        }

    }
}