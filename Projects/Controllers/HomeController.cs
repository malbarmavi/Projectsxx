using System.Web.Mvc;
using Projects.Models;

namespace Projects.Controllers
{
    public class HomeController : ProjectController
    {
        [HttpGet]
        public ActionResult Index()
        {

            return RedirectToAction("Index", "Login");
        }






    }
}