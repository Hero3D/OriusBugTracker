using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataLibrary.DataAccess
{
    public static class SQLDataAccess
    {
        public static string GetConnectionString(string connectionName="OriusDB")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data);
            }
        }

        public static int ExecuteData(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql);
            }
        }

        public static int CallStoredProcedure(string procedureName, SQLParameter[] parameters = null)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                var cmd = new SqlCommand();
                cmd.Connection = (SqlConnection)cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedureName;

                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i].Name, parameters[i].Data);
                }

                cnn.Open();
                var records = cmd.ExecuteNonQuery();
                cnn.Close();

                return records;
            }
        }
    }
}
