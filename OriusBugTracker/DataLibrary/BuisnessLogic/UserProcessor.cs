using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BuisnessLogic
{
    public static class UserProcessor
    {
        public static int CreateUser(string username, string password, string email)
        {
            var sql = $@"exec dbo.spUser_Create @Username = '{username}', @Password = '{password}', @Email = '{email}'";

            return SQLDataAccess.ExecuteData(sql);
        }

        public static List<UserModel> LoadUsers()
        {
            string sql = @"exec dbo.spUser_GetAll";

            return SQLDataAccess.LoadData<UserModel>(sql);
        }
    }
}
