using Helper;
using Projects.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projects
{
    public static class DB
    {
        //نص الاتصال بمخدم قواعد البيانات
        readonly static string connectionStrin = "Data Source = (local);Initial Catalog=projects;User id=sa;Password=123;";

        //SQLتنفيذ جمل ال   
        // وجلب الناتج على شكل جدول
        public static DataTable GetData(string sqlStatement)
        {
            return DataBase.GetData(sqlStatement, connectionStrin);
        }
        //لتنفيذ تعليمات مثل 
        //insert , delete ,update
        public static bool ExecuteNonQuery(string sqlStatement)
        {
            return DataBase.ExecuteNonQuery(sqlStatement, connectionStrin);
        }

        //انشاء موظف جديد
        public static bool CreateUser(User user)
        {

            var insertStatement = $@"INSERT INTO
            [projects].[dbo].[users] ([user_name] ,[password],[first_name],[last_name] ,[email],[notes],role,[ceo],company_id) VALUES (
			'{user.UserName}','{Cryptography.generateMD5(user.Password)}','{user.FirstName}','{user.LastName}','{user.Email}','{user.Email}',
            {(int)user.Role},{(user.CEO ? 1 : 0)},{user.CompanyId}) ";
            try
            {
                return DB.ExecuteNonQuery(insertStatement);


            }
            catch (Exception)
            {

                return false;
            }


        }

        //حذف موظف
        public static bool DeleteUser(int id)
        => ExecuteNonQuery($"delete from users where id={id}");

        //انشاء حساب المدير العام و الشركة عند التسجيل ضمن النظام
        public static User GetUser(UserLogin user)
        {

            var result = new User();
            var queryStatement =
                string.Format(@"select id,[user_name],[password],first_name,last_name,email,notes,role,ceo,company_id from users
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

        //فحص اذا كان اسم المستخدم موجود مسبقا ضمن النظام
        public static bool IsUserExsist(string userName)
        {
            return GetData(String.Format("select id from users where [user_name] like '{0}'", userName)).Rows.Count > 0 ? true : false;
        }

        //قائمة بكافة الموظفين
        public static List<User> GetUserList(int companyId)
        {
            var users = new List<User>();
            DataTable usersData = GetData(
                "SELECT [id],[user_name],[password],[first_name],[last_name],[email],[role],[notes] FROM [dbo].[users] Where ceo=0 and company_id=" + companyId.ToString());

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

        //انشاء مشروع جديد
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

        //جلب قائمة بكافة المستخدمين  -الموظفين - و ذلك من اجل القائمة المنسدلة
        public static List<SelectListItem> GetSeletUsersList()
        {
            var users = new List<SelectListItem>();
            DataTable usersData = GetData($"SELECT [id],[user_name],[password],[first_name],[last_name],[email],[notes] FROM [dbo].[users] where company_id={(HttpContext.Current.Session[SessionNames.User] as User).CompanyId}");

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

        //انشاء مهمة جددية لمشروع معين
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

        //جلب معلومات المشاريع كافة لعرضها ضمن صفحة المشاريع
        public static List<ProjectInfo> GetProjectDashData(int companyId)
        {
            var result = new List<ProjectInfo>();
            var dt = GetData($@" select id,name,parent,create_date,dead_line
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
                    Precent = (data["Success"].ToString().ToInt() == 0) || data["TaskCount"].ToString().ToInt() == 0 ? 0 : ((data["Success"].ToString().ToInt() * 100) / data["TaskCount"].ToString().ToInt()),
                    CreteDate = Convert.ToDateTime(data["create_date"].ToString()),
                    DaedLine = Convert.ToDateTime(data["dead_line"].ToString())
                });
            }

            return result;
        }

        //جلب اسم المشروع
        public static string GetProjectName(int id, string projectName)
        => id == 0 ? projectName : $"{DataBase.ExecuteScalar($"Select name from project where id={id} ", connectionStrin).Data.ToString()} - {projectName}";

        //جلب كافة المهام للمستخدم الحالي و حالة كل مهمة و الاولوية الخاصة بكل واحدة
        public static List<TaskInfo> GetTaskInfo(User user)
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
			from task where  project_id in (  select id from project where project.archive = 0) and id in (select task_id from task_users where user_id = {user.Id}  
            {(user.Role == UserRole.Employee ? "and state <>3 " : "")} ) order by project_id");

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

        //تعديل بيانات مهمة معينة مثلا حالة التنفيذ
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

        //جلب بيانات مهمة من اجل تعديل بياناتها
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

        //تعين حالة مشروع الى ارشفة و تعني نجاح المشروع
        public static bool SetProjectArchive(int id)
        => ExecuteNonQuery($"update project set archive=1 where id = {id}");

        //انشاء المستخدم الرئيسي للنظام عند التسجيل 
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

        //انشاء شركة عن التسجيل
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

        //جلب بيانات الشركة الحالية من قاعدة البينات
        public static Company GetCompany(int companyId)
        {
            var companyData =
            GetData($@"SELECT [id],[name],[address],[type],[about],[facebook_url],[twitter_url],[website_url],[phone_number] 
						FROM [projects].[dbo].[company] where id ={companyId}").Rows[0];

            return new Company()
            {
                Id = companyData["id"].ToString().ToInt(),
                Name = companyData["name"].ToString(),
                Type = companyData["type"].ToString(),
                About = companyData["about"].ToString(),
                FacebookUrl = companyData["facebook_url"].ToString(),
                TwitterUrl = companyData["twitter_url"].ToString(),
                WebsiteUrl = companyData["website_url"].ToString(),
                PhoneNumber = companyData["phone_number"].ToString(),
                Address = companyData["address"].ToString()
            };
        }

        //تعديل بيانات الشركة
        public static bool UpdateCompany(Company company)
        {
            return DB.ExecuteNonQuery($@"UPDATE [projects].[dbo].[company]
									SET [name] = '{company.Name}'
										,[address] = '{company.Address}'
										,[type] = '{company.Type}'
										,[about] = '{company.About}'
										,[facebook_url] = '{company.FacebookUrl}'
										,[twitter_url] = '{company.TwitterUrl}'
										,[website_url] = '{company.WebsiteUrl}'
										,[phone_number] = '{company.PhoneNumber}'
									WHERE id={company.Id}");
        }

        //جلب عدد الاشعارات 
        public static int GetNotificationCount(int companyId)
        {
            return GetNotificationList(companyId).Where(n => n.Message?.Length > 0).ToList().Count;
        }


        //جلب جميع الاشعاؤات لعرضها ضمن صفحة الاشعارات
        public static List<Notify> GetNotificationList(int companyId)
        {
            var result = new List<Notify>();


            result.Add(GetNotification($"select  0 'count' ,'Company about not set' 'message' from company where about ='' and id={companyId}", false));

            result.Add(GetNotification($"select COUNT(id) 'count' ,'Project will end in less hat 10 days' 'message' from project where DATEDIFF(DAY,GETDATE(),dead_line) <10  and archive = 0 and company_id={companyId}"));
            result.Add(GetNotification($"select COUNT(id)'count' ,'Projects has not task'  'message' from project where id not in (select project_id from task) and archive = 0 and company_id={companyId}"));

            return result;
        }

        //دالة مساعدة عند جلب الاشعارات 
        static Notify GetNotification(string sqlStatement, bool countable = true)
        {
            var staticData = GetData(sqlStatement);
            if (staticData.Rows.Count > 0)
            {
                return new Notify()
                {
                    Count = staticData.Rows[0]["count"].ToString(),
                    Message = countable ? staticData.Rows[0]["count"].ToString().ToInt() == 0 ? "" : staticData.Rows[0]["message"].ToString() : staticData.Rows[0]["message"].ToString(),
                    ShowCount = countable
                };
            }
            else
            {
                return new Notify() { Count = "0", Message = string.Empty };
            }

        }


        //دالة خاصة بالتقارير
        public static DataTable GetArchiveProjects()
        => GetData(@"	
	select 
		name,
		case when parent = 0 then 'Primary'else 'Sub' end 'type',
		create_date,
		dead_line,
		(select COUNT(task.id) from task where task.project_id = project.id)'task_count' ,
		(select count(user_id) from(select distinct user_id from task_users where task_id in (select id from task where project_id = project.id)) users) 'user_count' 
	from project where archive = 1  order by id");

        //دالة خاصة بالتقارير
        public static DataTable GetAllProjects()
        => GetData(@"	
	select 
		name,
		archive,
		case when parent = 0 then 'Primary'else 'Sub' end 'type',
		create_date,
		dead_line,
		(select COUNT(task.id) from task where task.project_id = project.id)'task_count' ,
		(select count(user_id) from(select distinct user_id from task_users where task_id in (select id from task where project_id = project.id)) users) 'user_count' 
	from project  order by id");

        //دالة خاصة بالتقارير
        public static DataTable GetFailProjects()
        => GetData(@"	
	select 
		name,
		case when parent = 0 then 'Primary'else 'Sub' end 'type',
		create_date,
		dead_line,
		(select COUNT(task.id) from task where task.project_id = project.id)'task_count' ,
		(select count(user_id) from(select distinct user_id from task_users where task_id in (select id from task where project_id = project.id)) users) 'user_count' 
	from project where dead_line < GETDATE() order by id");

        //دالة خاصة بالتقارير
        public static DataTable GetCompanyUsers(int company_id)
        => GetData($@"
			select user_name,first_name ,last_name,email,role,phone_number,ceo  from users where company_id={company_id}");
    }
}
