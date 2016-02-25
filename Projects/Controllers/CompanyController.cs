using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projects.Models;

namespace Projects.Controllers
{
    public class CompanyController : Projects.AdvancedController
    {
        // GET: Company
        public ActionResult Index()
        {
            if (!IsLogin()) return RedirectToAction("Index", "Login");
            return View(DB.GetCompany((Session[SessionNames.User] as User).CompanyId));
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View(DB.GetCompany((Session[SessionNames.User] as User).CompanyId));
        }

        [HttpPost]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                company.Id = (Session[SessionNames.User] as User).CompanyId;
                var state = DB.UpdateCompany(company);
                if (state == true)
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View(company);
                }
            }
            else
            {
                return View(company);
            }
        }

    }
}