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
            string getCoursesString = "SELECT [major_id], [major_name] FROM major ORDER BY [major].[major_name]";
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

        //Get list of years and semster seasons
        public static SemesterList createSemesterList()
        {
            List<String> years = new List<String>();
            List<String> seasons = new List<String>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            string getYearsString = "SELECT DISTINCT [year] FROM [HarborViewUniversity].[dbo].[section] ORDER BY [section].[year] ASC";
            string getSeasonsString = "SELECT DISTINCT [semster] FROM [HarborViewUniversity].[dbo].[section] ORDER BY [section].[semster]";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(getYearsString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        years.Add((reader.GetString(0)));
                    }
                }
                SqlCommand command2 = new SqlCommand(getSeasonsString, connection);
                using (var reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        seasons.Add((reader.GetString(0)));
                    }
                }
                connection.Close();
            }
            return new SemesterList(seasons, years);
        }

        //Get all majors and their student counts
        public static List<ResearcherMajorData> getStudentsByMajorCount(String year, String semester)
        {
            List<ResearcherMajorData> dataList = new List<ResearcherMajorData>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            string getDataString = @"SELECT DISTINCT [m].[major_name], CAST(COUNT(sml.student_id) as varchar(10)) FROM student_major_list sml
                                        LEFT JOIN student_full_time sft ON sft.student_id = sml.student_id
                                        LEFT JOIN student_part_time spt ON spt.student_id = sml.student_id
                                        JOIN major m ON m.major_id = sml.major_id
                                        WHERE ([sft].[year] = '" + year + "' OR [spt].[year] = '" + year + "') AND ([sft].[semester] = '" + semester + "' OR [spt].[semester] = '" + semester + "')"+
                                        "GROUP BY [m].[major_name] ";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(getDataString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new ResearcherMajorData(reader.GetString(0), reader.GetString(1)));
                    }
                }
                connection.Close();
            }
            return dataList;
        }
    }
}