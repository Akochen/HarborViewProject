using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class UserDbConnectionClass
    {
        public static String login(String queryString)
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String html = "<ul>";

            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    String output = reader.GetString(0);
                    connection.Close();
                    return output;
                } else
                {
                    connection.Close();
                    return "Error: Incorrect username or password.";
                }
            }
        }
    }
}