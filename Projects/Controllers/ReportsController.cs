using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projects.Models;
namespace Projects.Controllers
{
    public class ReportsController : Projects.AdvancedController
    {
        // GET: Reports
        public ActionResult Index()
        {
            if (!IsLogin()) return RedirectToAction("Index", "Login");
            return View();
        }

        public ActionResult ArchiveProjects()
        {
            if (!IsLogin()) return RedirectToAction("Index", "Login");
            return View(DB.GetArchiveProjects());
        }

        public ActionResult AllProjects()
        {
            if (!IsLogin()) return RedirectToAction("Index", "Login");
            return View(DB.GetAllProjects());
        }

        public ActionResult FailProjects()
        {
            if (!IsLogin()) return RedirectToAction("Index", "Login");
            return View(DB.GetFailProjects());

        }

        public ActionResult UsersList()
        {
            if (!IsLogin()) return RedirectToAction("Index", "Login");
            return View(DB.GetCompanyUsers((Session[SessionNames.User] as User).CompanyId));
        }
    }
}