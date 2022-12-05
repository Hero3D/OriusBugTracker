using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BuisnessLogic
{
    public static class RoleProcessor
    {
        public static List<RoleModel> LoadRoles()
        {
            var sql = @"exec dbo.spRoles_GetAll";

            return SQLDataAccess.LoadData<RoleModel>(sql);
        }

        public static List<UserRoleModel> LoadUserRoles()
        {
            var sql = @"exec spUserRole_GetAll";

            return SQLDataAccess.LoadData<UserRoleModel>(sql);
        }

        public static List<string> GetUserRole(string username)
        {
            var sql = $"spUserRole_GetRoleFromUsername @Username = '{username}'";

            return SQLDataAccess.LoadData<string>(sql);
        }
    }
}
