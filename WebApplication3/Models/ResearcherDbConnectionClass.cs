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
        //Display grade by course
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

        public static String getAverageGradeByCourse(String courseId, String semester, String year)
        {
            decimal grade = 0;
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            string getGradeString;
            if (courseId.ToLower().Equals("all"))
            {
                getGradeString = "SELECT [dbo].[getGradeAvgForAllCourses] ()";
            }
            else
            {
                getGradeString = "SELECT [dbo].[getGradeAvgByCourse] ( " + courseId + " ,'%" + year + "%' ,'%" + semester + "%')";
            }
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(getGradeString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            grade = reader.GetDecimal(0);
                        } else
                        {
                            grade = -1;
                        }
                    }
                }
                connection.Close();
            }
            return GradeList.convertPointsToGrade((double)grade);
        }
        //Display grade by major

        public static List<Major> listAllMajors()
        {
            List<Major> allMajors = new List<Major>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            string getCoursesString = "SELECT [major_id], [major_name] FROM major";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(getCoursesString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        allMajors.Add(new Major(reader.GetInt32(0).ToString(), reader.GetString(1)));
                    }
                }
                connection.Close();
            }
            return allMajors;
        }

        public static String getAverageGradeByMajor(String majorId, String semester, String year)
        {
            decimal grade = 0;
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            string getGradeString;
            getGradeString = "SELECT [dbo].[getGradeAvgByCourse] ( " + majorId + " ,'%" + year + "%' ,'%" + semester + "%')";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(getGradeString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            grade = reader.GetDecimal(0);
                        }
                        else
                        {
                            grade = -1;
                        }
                    }
                }
                connection.Close();
            }
            return GradeList.convertPointsToGrade((double)grade);
        }

        //Display grade by professor

        public static String getAverageGradeByProfessor(String professorId, String semester, String year)
        {
            decimal grade = 0;
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            string getGradeString = "SELECT [dbo].[getGradeAvgByProfesor] ( " + professorId + " ,'%" + year + "%' ,'%" + semester + "%')";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(getGradeString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            grade = reader.GetDecimal(0);
                        }
                        else
                        {
                            grade = -1;
                        }
                    }
                }
                connection.Close();
            }
            return GradeList.convertPointsToGrade((double)grade);
        }
    }
}