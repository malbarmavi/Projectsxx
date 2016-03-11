using Projects.Models;
using System.Web.Mvc;

namespace Projects.Controllers
{
    public class ProjectsController : AdvancedController
    {
        // GET: Projects
        public ActionResult Index()
        {
            if (!IsLogin()) return RedirectToAction("index", "dashboard");


            return View(DB.GetProjectDashData((Session[SessionNames.User] as Models.User).CompanyId));
        }
        [HttpGet]
        public ActionResult Add(int id = 0)
        {

            if (!IsLogin()) return RedirectToAction("index", "dashboard");

            return View(new Project());
        }
        [HttpPost]
        public ActionResult Add(Project project, int id = 0)
        {
            if (ModelState.IsValid)
            {
                project.UserId = (Session[SessionNames.User] as User).Id;
                //project.CreteDate = DateTime.Now;
                project.Parent = id;
                project.CompanyId = (Session[SessionNames.User] as User).CompanyId;
                bool addState = DB.CreateProject(project);
                if (addState)
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View(project);
                }
            }
            else
            {
                return View(project);
            }

        }
        [HttpGet]
        public ActionResult archive(int id)
        {
            DB.SetProjectArchive(id);
            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            if (!IsLogin()) return RedirectToAction("Index", "Login");
            if (((Models.User)Session[SessionNames.User]).Role == UserRole.Employee) return RedirectToAction("index", "Dashboard");

            DB.ExecuteNonQuery($@"delete from project where id = {id};
                                 delete from task_users where task_id in (select id from task where project_id = {id});
                                 delete from task where project_id = {id}  ");

            return RedirectToAction("index");
        }

    }
}