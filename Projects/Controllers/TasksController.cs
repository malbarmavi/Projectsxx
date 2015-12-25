using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projects.Models;

namespace Projects.Controllers
{
    public class TasksController : AdvancedController
    {


        public ActionResult Index()
        {
            if (!IsLogin()) return RedirectToAction("index", "dashboard");

            var task = new ProjectsList();
            task.DataList.Add(new SelectListItem() { Text = "first", Value = "0" });
            task.DataList.Add(new SelectListItem() { Text = "secnd", Value = "11" });
            task.DataList.Add(new SelectListItem() { Text = "third", Value = "12" });
            task.DataList.Add(new SelectListItem() { Text = "forth", Value = "33" });
            return View(task);
        }

        [HttpGet]
        public ActionResult Add()
        {
            if (!IsLogin()) return RedirectToAction("index", "dashboard");
            var projectTask = new ProjectTask();
            return View(projectTask);
        }

        [HttpPost]
        public ActionResult Add(ProjectTask task)
        {
            if (!IsLogin()) return RedirectToAction("index", "dashboard");
            if (ModelState.IsValid)
            {
                var addState = DB.CreateTask(task);
                if (addState)
                {
                    return RedirectToAction("index");

                }
            }
            return View(task);
        }

    }
}