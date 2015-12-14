using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helper;
using System.Data;
using Projects.Models;
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
                String.Format(@"INSERT INTO [projects].[dbo].[users] ([user_name] ,[password],[first_name],[last_name] ,[email],[notes]) VALUES (
                                '{0}','{1}','{2}','{3}','{4}','{5}') ", user.UserName, Cryptography.generateMD5(user.Password), user.FirstName, user.LastName, user.Email, user.Notes);
            try
            {
                return DB.ExecuteNonQuery(insertStatement);

            }
            catch (Exception)
            {

                return false;
            }


        }
        //public static bool CreateCompany()
        //{

        //    return true;
        //}
        //public static int GetCompanyId(string companyName)
        //{
        //    return 0;
        //}

        public static User GetUser(UserLogin user)
        {

            var result = new User();
            var queryStatement = String.Format(@"select id,[user_name],[password],first_name,last_name,email,notes from users
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
            }

            return result;
        }

        public static bool IsUserExsist(string userName)
        {
            return GetData(String.Format("select id from users where [user_name] like '{0}'", userName)).Rows.Count > 0 ? true : false;
        }

        public static List<User> GetUserList()
        {
            var users = new List<User>();
            DataTable usersData = GetData("SELECT [id],[user_name],[password],[first_name],[last_name],[email],[notes] FROM [dbo].[users]");

            foreach (DataRow row in usersData.Rows)
            {
                users.Add(new User
                {
                    Id = row["id"].ToString().ToInt(),
                    UserName = row["user_name"].ToString(),
                    FirstName = row["first_name"].ToString(),
                    LastName = row["last_name"].ToString(),
                    Email = row["email"].ToString(),
                    Notes = row["notes"].ToString()
                });
            }

            return users;
        }

    }
}