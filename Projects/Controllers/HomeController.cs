using System.Web.Mvc;
using Projects.Models;

namespace Projects.Controllers
{
    public class HomeController : Projects.AdvancedController
    {
        [HttpGet]
        public ActionResult Index()
        {

            return RedirectToAction("Index", "Login");
        }






    }
}