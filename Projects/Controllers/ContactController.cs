using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projects.Controllers
{
    public class ContactController : Projects.AdvancedController
    {
        // GET: Contact
        public ActionResult Index()
        {
            if (!IsLogin()) return RedirectToAction("Index", "Login");
            return View();
        }
    }
}