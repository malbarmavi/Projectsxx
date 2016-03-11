using Projects.Models;
using System.Linq;
using System.Web.Mvc;
namespace Projects.Controllers
{
    public class NotificationController : Projects.AdvancedController
    {
        // GET: Notification
        public ActionResult Index()
        {
            if (!IsLogin()) return RedirectToAction("Index", "Login");

            return View(DB.GetNotificationList((Session[SessionNames.User] as User).CompanyId).Where(n => n.Message?.Length > 0).ToList());

        }
    }
}