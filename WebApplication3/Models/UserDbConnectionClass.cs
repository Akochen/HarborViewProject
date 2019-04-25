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
            List<CatalogCourse> coursesList = new List<CatalogCourse>();
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(courseString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    coursesList.Add(new CatalogCourse(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetByte(4)));
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

        public static String isFacultyAdvisor(String email)
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            //String queryString = "SELECT [user_id] FROM [dbo].[user] WHERE [email] = '" + email + "'";
            String queryString = @"SELECT
                                   CASE WHEN EXISTS 
                                    (
                                      SELECT * FROM [advisor_view] WHERE faculty_email = '"+email+@"'
                                    ) THEN '1' ELSE '0' END";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                String result = "";
                if (reader.Read())
                {
                    result = reader.GetString(0);
                    connection.Close();
                }
                return result;
            }
        }

        public static EnrollmentSemesterHelper createViewScheduleHelper()
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String deptString = "SELECT [department_full_name] FROM [HarborViewUniversity].[dbo].[department]";
            String dayString = "SELECT DISTINCT [days] FROM [HarborViewUniversity].[dbo].[section_view]";
            String timeString = "SELECT DISTINCT FORMAT(CAST([start_time] AS datetime), 'h:mm tt') AS start_time, FORMAT(CAST([end_time] AS datetime), 'h:mm tt') AS end_time, CAST([end_time] AS datetime) AS order_time FROM [HarborViewUniversity].[dbo].[time_slot] ORDER BY order_time";
            List<String> departments = new List<string>();
            List<String> days = new List<string>();
            List<String> times = new List<string>();
            List<String> semesters = new List<string>();
            //Grab semester and year selector data
            semesters.Add(SemesterDataHelper.getSemesterSeason() + " " + SemesterDataHelper.getSemesterYear());
            semesters.Add(SemesterDataHelper.getNextSemesterSeason() + " " + SemesterDataHelper.getNextSemesterYear());

            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command1 = new SqlCommand(deptString, connection);
                connection.Open();
                using (var reader = command1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departments.Add(reader.GetString(0));
                    }
                }

                SqlCommand command2 = new SqlCommand(dayString, connection);
                using (var reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        days.Add(reader.GetString(0));
                    }
                }

                SqlCommand command3 = new SqlCommand(timeString, connection);
                using (var reader = command3.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        times.Add(reader.GetString(0));
                    }
                }

                connection.Close();
            }

            return new EnrollmentSemesterHelper(departments, times, days, semesters);
        }

        public static List<Section> searchSections(String searchYear, String searchSemester, String instructor, String days, String time, String courseID, String courseName, String department)
        {
            List<Section> sections = new List<Section>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT [section_id],[course_id],[course_name],[instructor],[days],[start_time],[end_time],[semster],[year],[building_full_name],[room_number],[type],[capactiy],[seats_remaining] FROM [HarborViewUniversity].[dbo].[section_view] " +
                "WHERE  [year] = '" + searchYear + "' AND semster = '" + searchSemester + "' AND [instructor] LIKE '%" + instructor + "%' AND [days] = '" + days + "' AND [start_time] LIKE '%" + time + "%' " +
                "AND [course_id] LIKE '%" + courseID + "%' AND [course_name] LIKE '%" + courseName + "%' AND [department_full_name] LIKE '%" + department + "%'  ORDER BY course_id";
            if (days.Equals(""))
            {
                queryString = "SELECT [section_id],[course_id],[course_name],[instructor],[days],[start_time],[end_time],[semster],[year],[building_full_name],[room_number],[type],[capactiy],[seats_remaining] FROM [HarborViewUniversity].[dbo].[section_view] " +
                "WHERE  [year] = '" + searchYear + "' AND semster = '" + searchSemester + "' AND [instructor] LIKE '%" + instructor + "%' AND [start_time] LIKE '%" + time + "%' " +
                "AND [course_id] LIKE '%" + courseID + "%' AND [course_name] LIKE '%" + courseName + "%' AND [department_full_name] LIKE '%" + department + "%'  ORDER BY course_id";
            }
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sections.Add(new Section(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7)
                            , reader.GetString(8), reader.GetString(9), reader.GetInt32(10), reader.GetString(11), reader.GetByte(12), reader.GetByte(13)));
                    }
                }
                connection.Close();
            }

            return sections;
        }
    }
}