using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projects.Models;

namespace Projects.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {

            //list of Em.
            return View(DB.GetUserList());
        }

        public ActionResult Add()
        {
            //Add
            return View();
        }
        [HttpPost]
        public ActionResult Add(User user)
        {


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
        [HttpPost]
        public ActionResult Delete(int id)
        {
            return RedirectToAction("index");
        }

    }
}