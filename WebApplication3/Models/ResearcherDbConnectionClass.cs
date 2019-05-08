using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication3.HelperClasses;

namespace WebApplication3.Models
{
    public class ResearcherDbConnectionClass
    {
        public static List<CatalogCourse> ListAllCourses()
        {
            List<CatalogCourse> allCourses = new List<CatalogCourse>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            string getCoursesString = "SELECT course.course_id, course.course_name FROM course ORDER BY course.course_name";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(getCoursesString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        allCourses.Add(new CatalogCourse(reader.GetInt32(0).ToString(), reader.GetString(1)));
                    }
                }
                connection.Close();
            }
            return allCourses;
        }

        public static String getAverageGradeByCourse(String courseId)
        {

            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            string getGradeString = "SELECT dbo.getGradeAvgByCourse(" + courseId + ")";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                
                connection.Close();
            }
            return "";
        }
    }
}