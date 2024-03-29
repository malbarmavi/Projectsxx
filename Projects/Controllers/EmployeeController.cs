﻿using Projects.Models;
using System.Web.Mvc;

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
                return View(DB.GetUserList(((Models.User)Session[SessionNames.User]).CompanyId));
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
                user.CompanyId = ((Models.User)Session[SessionNames.User]).CompanyId;
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

            DB.DeleteUser(id);

            return RedirectToAction("index");
        }

    }
}