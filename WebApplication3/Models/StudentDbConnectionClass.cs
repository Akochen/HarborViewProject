using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{

    public class StudentDbConnectionClass
    {
        public static String selectStudents(String queryString)
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String html = "<ul>";

            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using ( var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        html += "<li> Name: " + reader.GetString(0) + " " + reader.GetString(1) + " </li>";
                    }
                }
                connection.Close();
            }
            html += "</ul>";
            return html;
        }
    }

}