﻿using System.Web.Mvc;

namespace Projects.Controllers
{
    public class DashboardController : Projects.AdvancedController
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (!IsLogin()) return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult LogOff()
        {
            Session[SessionNames.User] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}