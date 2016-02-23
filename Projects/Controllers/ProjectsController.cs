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


            return View(DB.GetProjectDashData((Session[SessionNames.User] as Models.User).CompanyId));
        }
        [HttpGet]
        public ActionResult Add(int id = 0)
        {

            if (!IsLogin()) return RedirectToAction("index", "dashboard");

            return View(new Project());
        }
        [HttpPost]
        public ActionResult Add(Project project, int id = 0)
        {
            if (ModelState.IsValid)
            {
                project.UserId = (Session[SessionNames.User] as User).Id;
                //project.CreteDate = DateTime.Now;
                project.Parent = id;
                project.CompanyId = (Session[SessionNames.User] as User).CompanyId;
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
        [HttpGet]
        public ActionResult archive(int id)
        {
            DB.SetProjectArchive(id);
            return RedirectToAction("index");
        }

    }
}