using Projects.Models;
using System.Web.Mvc;

namespace Projects.Controllers
{
    public class LoginController : Projects.AdvancedController
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (IsLogin()) return RedirectToAction("index", "Dashboard");
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserLogin loginData)
        {

            if (ModelState.IsValid)
            {
                var userData = DB.GetUser(loginData);
                if (userData.Id > 0)
                {
                    Session[SessionNames.User] = userData;
                    return RedirectToAction("index", "Dashboard");
                }
                else
                {
                    loginData.ErrorMessage = "That account doesn't exist";
                    return View(loginData);
                }

            }


            return View(loginData);

        }
    }
}