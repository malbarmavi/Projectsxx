using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projects.Models;

namespace Projects.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Company company)
        {
            return View();
        }

    }
}