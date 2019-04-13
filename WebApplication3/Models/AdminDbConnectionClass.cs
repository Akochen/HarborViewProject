using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication3.HelperClasses;


namespace WebApplication3.Models
{
    public class AdminDbConnectionClass
    {
        public static List<Department> getDepartmentInfo()
        {
            List<Department> departments = new List<Department>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "select department_full_name from department";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departments.Add(new Department(reader.GetInt32(0).ToString(), reader.GetString(1)));
                    }
                }
                connection.Close();
            }
            return departments;
        }
    }
}