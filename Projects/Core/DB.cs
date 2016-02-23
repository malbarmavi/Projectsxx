using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helper;
using System.Data;
using Projects.Models;
using System.Web.Mvc;

namespace Projects
{
    public static class DB
    {

        readonly static string connectionStrin = "Data Source = (local);Initial Catalog=projects;User id=sa;Password=123;";

        public static DataTable GetData(string sqlStatement)
        {
            return DataBase.GetData(sqlStatement, connectionStrin);
        }

        public static bool ExecuteNonQuery(string sqlStatement)
        {
            return DataBase.ExecuteNonQuery(sqlStatement, connectionStrin);
        }

        public static bool CreateUser(User user)
        {


            var insertStatement =
                string.Format(@"INSERT INTO [projects].[dbo].[users] ([user_name] ,[password],[first_name],[last_name] ,[email],[notes],role,[ceo],company_id) VALUES (
                                '{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},{8}) ", user.UserName, Cryptography.generateMD5(user.Password), user.FirstName, user.LastName, user.Email, user.Notes, (int)user.Role, user.CEO ? 1 : 0, user.CompanyId);
            try
            {
                return DB.ExecuteNonQuery(insertStatement);


            }
            catch (Exception)
            {

                return false;
            }


        }


        public static User GetUser(UserLogin user)
        {

            var result = new User();
            var queryStatement = String.Format(@"select id,[user_name],[password],first_name,last_name,email,notes,role,ceo,company_id from users
                                   where [USER_NAME] like '{0}' and [password] like '{1}'", user.UserName, Cryptography.generateMD5(user.Password));

            DataTable tbResult = GetData(queryStatement);
            if (tbResult.Rows.Count == 1)
            {
                DataRow dataResult = tbResult.Rows[0];
                result.Id = dataResult["id"].ToString().ToInt();
                result.UserName = dataResult["user_name"].ToString();
                result.FirstName = dataResult["first_name"].ToString();
                result.LastName = dataResult["last_name"].ToString();
                result.Email = dataResult["email"].ToString();
                result.Notes = dataResult["email"].ToString();
                result.Role = (UserRole)dataResult["role"].ToString().ToInt();
                result.CEO = Convert.ToBoolean(dataResult["ceo"].ToString());
                result.CompanyId = dataResult["company_id"].ToString().ToInt();
            }

            return result;
        }

        public static bool IsUserExsist(string userName)
        {
            return GetData(String.Format("select id from users where [user_name] like '{0}'", userName)).Rows.Count > 0 ? true : false;
        }

        public static List<User> GetUserList(int companyId)
        {
            var users = new List<User>();
            DataTable usersData = GetData("SELECT [id],[user_name],[password],[first_name],[last_name],[email],[role],[notes] FROM [dbo].[users] Where ceo=0 and company_id=" + companyId.ToString());

            foreach (DataRow row in usersData.Rows)
            {
                users.Add(new User
                {
                    Id = row["id"].ToString().ToInt(),
                    UserName = row["user_name"].ToString(),
                    FirstName = row["first_name"].ToString(),
                    LastName = row["last_name"].ToString(),
                    Email = row["email"].ToString(),
                    Role = (UserRole)row["role"].ToString().ToInt(),
                    Notes = row["notes"].ToString()

                });
            }

            return users;
        }

        public static bool CreateProject(Project projectData)
        {
            var insertStatement =
            string.Format(@"insert into [projects].[dbo].[project]([name],[create_date],[dead_line],[notes],[parent],[user_id],[company_id]) values (
                                '{0}','{1}','{2}','{3}',{4},{5},{6}) ",
                                projectData.Name, projectData.CreteDate, projectData.DaedLine, projectData.Description, projectData.Parent, projectData.UserId, projectData.CompanyId);
            try
            {
                return DB.ExecuteNonQuery(insertStatement);

            }
            catch (Exception)
            {

                return false;
            }
        }

        public static List<SelectListItem> GetSeletUsersList()
        {
            var users = new List<SelectListItem>();
            DataTable usersData = GetData("SELECT [id],[user_name],[password],[first_name],[last_name],[email],[notes] FROM [dbo].[users]");

            foreach (DataRow row in usersData.Rows)
            {
                users.Add(new SelectListItem()
                {
                    Text = row["user_name"].ToString(),
                    Value = row["id"].ToString()
                }
                );
            }

            return users;
        }

        public static bool CreateTask(ProjectTask task)
        {
            var insertStatement =
            string.Format($@"INSERT INTO [projects].[dbo].[task]([name],[description],[priority],[state],[notes],[project_id]) values (
                                '{task.Name}','{task.Description}',{(int)task.Priority},{(int)task.State},'{""}',{task.Project_id}) ");

            try
            {
                ExecuteNonQuery(insertStatement);
                var task_id = Helper.DataBase.ExecuteScalar("Select MAX(ID) from dbo.task", connectionStrin).Data;

                foreach (int userId in task.Users.SelectedIndex)
                {
                    DB.ExecuteNonQuery($"INSERT INTO [projects].[dbo].[task_users] ([task_id],[user_id])VALUES ({task_id},{userId})");
                }

                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public static List<ProjectInfo> GetProjectDashData(int companyId)
        {
            var result = new List<ProjectInfo>();
            var dt = GetData($@" select id,name,parent
                                ,(select COUNT(id) from task where project_id = project.id) 'TaskCount' 
                                ,(select COUNT(id) from task where project_id = project.id and state=0 ) 'Undecided' 
                                ,(select COUNT(id) from task where project_id = project.id and state=1 ) 'InProcess' 
                                ,(select COUNT(id) from task where project_id = project.id and state=2 ) 'Faile' 
                                ,(select COUNT(id) from task where project_id = project.id and state=3 ) 'Success' 
                                from project where archive=0 and company_id ={companyId}");

            //Get row data to list
            foreach (DataRow data in dt.Rows)
            {
                result.Add(new ProjectInfo()
                {
                    Id = data["id"].ToString().ToInt(),
                    Parent = data["parent"].ToString().ToInt(),
                    Name = data["name"].ToString(),
                    TaskCount = data["TaskCount"].ToString().ToInt(),
                    Undecided = data["Undecided"].ToString().ToInt(),
                    InProcess = data["InProcess"].ToString().ToInt(),
                    Faile = data["Faile"].ToString().ToInt(),
                    Success = data["Success"].ToString().ToInt(),
                    Precent = (data["Success"].ToString().ToInt() == 0) || data["TaskCount"].ToString().ToInt() == 0 ? 0 : ((data["Success"].ToString().ToInt() * 100) / data["TaskCount"].ToString().ToInt())
                });
            }

            return result;
        }

        public static string GetProjectName(int id, string projectName)
            => id == 0 ? projectName : $"{DataBase.ExecuteScalar($"Select name from project where id={id} ", connectionStrin).Data.ToString()} - {projectName}";


        public static List<TaskInfo> GetTaskInfo(string user_id)
        {
            var result = new List<TaskInfo>();
            var dt = GetData($@"select id,name ,case 
                when state = 0 then 'Undecided' 
                when state = 1 then 'InProcess'
                when state = 2 then 'Faile'
                when state = 3 then 'Success'
                end 'state'
            ,case
                when priority = 0 then 'low'
                when priority = 1 then 'normal'
                when priority = 2 then 'high'
                end 'priority_text'	 
                ,(select name from project where project.id = project_id) 'project_name'
                from task where  project_id in (  select id from project where project.archive = 0) and id in (select task_id from task_users where user_id = {user_id}  ) order by project_id");

            foreach (DataRow data in dt.Rows)
            {
                result.Add(new TaskInfo()
                {
                    Id = data["id"].ToString(),
                    Name = data["name"].ToString(),
                    State = data["state"].ToString(),
                    priority = data["priority_text"].ToString(),
                    ProjectName = data["project_name"].ToString()
                });
            }

            return result;
        }


        public static bool UpdateTask(ProjectTask task)
        {
            try
            {
                ExecuteNonQuery($@"UPDATE [projects].[dbo].[task] Set state ={(int)task.State} , priority={(int)task.Priority} , description='{task.Description}' where id={task.Id}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static ProjectTask GetTask(int taskId)
        {
            var taskData = GetData($"select [id],[name],[description],[priority],[state],[notes],[project_id] from task where id={taskId}").Rows[0];

            return new ProjectTask()
            {
                Name = taskData["name"].ToString(),
                Priority = (PriorityState)taskData["priority"].ToString().ToInt(),
                State = (TaskState)taskData["state"].ToString().ToInt(),
                Description = taskData["description"].ToString()
            };
        }

        public static bool SetProjectArchive(int id)
            => ExecuteNonQuery($"update project set archive=1 where id = {id}");
        public static bool CreateCeoUser(UserSignup user)
        {
            try
            {
                bool insertNewCpmpany = CreateCompany(user.CompanyName, user.CompanyType);
                int companyId = DataBase.ExecuteScalar("select IDENT_CURRENT('company') 'id'", connectionStrin).Data.ToString().ToInt();
                user.CompanyId = companyId;
                bool insertNewUser = CreateUser(user);
                return insertNewUser && insertNewCpmpany;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public static bool CreateCompany(string name, string type)
        {
            try
            {
                return ExecuteNonQuery($"insert into company (name,[type]) values ('{name}','{type}')");
            }
            catch (Exception)
            {

                return false;
            }
        }



    }
}