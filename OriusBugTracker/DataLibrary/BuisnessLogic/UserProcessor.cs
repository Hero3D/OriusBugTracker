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
            var parameters = new SQLParameter[]
            {
                new SQLParameter("@Username", username),
                new SQLParameter("@Password", password),
                new SQLParameter("@Email", email)
            };

            return SQLDataAccess.CallStoredProcedure("dbo.spUser_Create", parameters);
        }

        public static List<UserModel> LoadUsers()
        {
            string sql = @"exec dbo.spUser_GetAll";

            return SQLDataAccess.LoadData<UserModel>(sql);
        }
    }
}
