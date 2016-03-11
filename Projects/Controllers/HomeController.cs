using System.Web.Mvc;

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