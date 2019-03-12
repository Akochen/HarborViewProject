using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace WebApplication3.Models
{
    public class UserDbConnectionClass
    {
        public static String login(String queryString)
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;

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
        
        public static String displayCatalogCourses()
        {
            String queryString = "SELECT TOP(1000)[Course_Name], [Course], [Prereqs], [Description], [Credits] FROM[HarborViewUniversity].[dbo].[catalog_courses]";
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String table = "<table>";

            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    table += "<tr>";
                    table += "<td>" + reader.GetString(0) + "</td>";
                    table += "<td>" + reader.GetString(1) + "</td>";
                    table += "<td>" + reader.GetString(2) + "</td>";
                    table += "<td>" + reader.GetString(3) + "</td>";
                    table += "<td>" + reader.GetByte(4) + "</td>";
                    table += "</tr>";
                }
            }
            table += "</table>";
            return table;
        }
    }
}