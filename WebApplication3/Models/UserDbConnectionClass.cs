using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using WebApplication3.HelperClasses;

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

        public static String getUserID(String email)
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT [user_id] FROM [dbo].[user] WHERE [email] = '" + email + "'";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                String output = "";
                if (reader.Read())
                {
                    output = reader.GetInt32(0).ToString();
                    connection.Close();
                }
                return output;
            }
        }

        public static Catalog createCatalog()
        {
            String courseString = "SELECT [Course_Name], [Course], [Prereqs], [Description], [Credits] FROM[HarborViewUniversity].[dbo].[catalog_courses] ORDER BY [Course_Name] ASC";
            //Fill Courses List
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            List<Course> coursesList = new List<Course>();
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(courseString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    coursesList.Add(new Course(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetByte(4)));
                }
                connection.Close();
            }
            //Fill Majors List
            String majorString = "SELECT [major_name],[Requirements], [degree_name] FROM [dbo].[catalog_majors]";
            List<Major> majorsList = new List<Major>();
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(majorString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    majorsList.Add(new Major(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                }
                connection.Close();
            }
            //Fill Minors List
            String minorString = "SELECT [minor_name],[Requirements] FROM [dbo].[catalog_minors]";
            List<Minor> minorsList = new List<Minor>();
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(minorString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    minorsList.Add(new Minor(reader.GetString(0), reader.GetString(1)));
                }
                connection.Close();
            }
            //Store lists inn Catalog
            Catalog catalog = new Catalog(coursesList, majorsList, minorsList);
            return catalog;
        }

        public static String changePasswordCheck(String email, String id, String password)
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT COUNT([email]) FROM [HarborViewUniversity].[dbo].[user] WHERE [email] = '" + email + "' AND [user_id] = " + id;
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                int output = -1;
                if (reader.Read())
                {
                    output = reader.GetInt32(0);
                    connection.Close();
                }
                if(output == -1)
                {
                    return "Error: Please check your internet connection!";
                } else if (output == 0)
                {
                    return "Error: The E-Mail you have entered is incorrect!";
                } else if(output == 1)
                {
                    changePassword(email, password);
                    return "Your password has been successfully changed";
                } else
                {
                    return "Error: Please contact an administrator <br />(Error code: 232)";
                }
            }
        }

        public static void changePassword(String email, String password)
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "UPDATE [HarborViewUniversity].[dbo].[user] SET password = '" + password + "' WHERE email = '" + email + "';";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}