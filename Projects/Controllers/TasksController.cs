using Projects.Models;
using System.Linq;
using System.Web.Mvc;

namespace Projects.Controllers
{
    public class TasksController : AdvancedController
    {


        public ActionResult Index()
        {
            if (!IsLogin()) return RedirectToAction("index", "dashboard");
            return View(DB.GetTaskInfo((Models.User)Session[SessionNames.User]).GroupBy(
                t => t.ProjectName,
                (key, e) => new TasksCollection { ProjectName = key, TasksList = e.ToList<TaskInfo>() }).ToList());
        }

        [HttpGet]
        public ActionResult Add(int? id)
        {
            if (id == null) RedirectToAction("index", "projects");

            if (!IsLogin()) return RedirectToAction("index", "dashboard");
            var projectTask = new ProjectTask();

            return View(projectTask);
        }

        [HttpPost]
        public ActionResult Add(int? id, ProjectTask task)
        {
            if (id == null) RedirectToAction("index", "projects");

            if (!IsLogin()) return RedirectToAction("index", "dashboard");
            if (ModelState.IsValid)
            {
                task.Project_id = (int)id;
                var addState = DB.CreateTask(task);

                if (addState)
                {


                    return RedirectToAction("index", "projects");

                }
            }
            return View(task);
        }


        [HttpGet]
        public ActionResult Update(int id)
        {
            if (!IsLogin()) return RedirectToAction("index", "dashboard");
            return View(DB.GetTask(id));
        }

        [HttpPost]
        public ActionResult Update(int id, ProjectTask task)
        {
            if (!IsLogin()) return RedirectToAction("index", "dashboard");
            task.Id = id;
            var updateState = DB.UpdateTask(task);
            if (updateState == true)
            {

                return RedirectToAction("index");
            }
            else
            {
                return View(task);
            }
        }

    }
}