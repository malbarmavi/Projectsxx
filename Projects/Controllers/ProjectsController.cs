﻿using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projects.Controllers
{
    public class ProjectsController : AdvancedController
    {
        // GET: Projects
        public ActionResult Index()
        {
            if (!IsLogin()) return RedirectToAction("index", "dashboard");
            return View();
        }
        [HttpGet]
        public ActionResult Add()
        {

            if (!IsLogin()) return RedirectToAction("index", "dashboard");

            return View(new Project() { DaedLine = DateTime.Now });
        }
        [HttpPost]
        public ActionResult Add(Project project)
        {
            if (ModelState.IsValid)
            {
                project.UserId = (Session[SessionNames.User] as User).Id;
                project.CreteDate = DateTime.Now;
                project.Parent = 0;
                bool addState = DB.CreateProject(project);
                if (addState)
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View(project);
                }
            }
            else
            {
                return View(project);
            }

        }
    }
}