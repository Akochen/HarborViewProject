using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication3.HelperClasses;

namespace WebApplication3.Models
{
    public class FacultyDbConnectionClass
    {
        public static List<Section> viewSchedule(String userID, String semester, String year)
        {
            List<Section> sections = new List<Section>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT * FROM [HarborViewUniversity].[dbo].[faculty_schedule_view] WHERE faculty_id = " + userID + " AND semster = '" + semester + "' AND [year] = '" + year + "'";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sections.Add(new Section(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4),
                            reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetInt32(8), reader.GetInt32(9)));
                    }
                }
                connection.Close();
            }

            return sections;
        }

        public static List<StudentEnrollment> viewEnrolleeList(String sectionID)
        {
            List<StudentEnrollment> students = new List<StudentEnrollment>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT * FROM [HarborViewUniversity].[dbo].[enrollee_student_grades] WHERE [section_id] = " + sectionID;
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new StudentEnrollment(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4),
                            reader.GetDateTime(5).ToShortDateString(), reader.GetInt32(8).ToString(), reader.GetString(9), reader.GetString(6), reader.GetString(7)
                            , reader.GetByte(10).ToString()));
                    }
                    connection.Close();
                }
                return students;
            }
        }

        public static String insertGrade(String studentID,String sectionID,String courseName,String semester,String year, String grade,String credits)
        {
            List<StudentEnrollment> students = new List<StudentEnrollment>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String insertString = "INSERT INTO [dbo].[student_semester_history]([student_id],[section_id],[course_name],[semester],[year],[grade],[credits])" +
                "VALUES("+studentID+","+sectionID+",'" + courseName + "','" + semester + "','" + year + "','" + grade + "'," + credits + ")";
            String result = "";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(insertString, connection);
                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                    result = "Success message";
                }
                catch
                {
                    result = "ERROR: Student already has a grade for this section!";
                }
                connection.Close();

                return result;
            }
        }
    }
}
//INSERT INTO [dbo].[student_semester_history]([student_id],[section_id],[course_name],[semester],[year],[grade],[credits])VALUES(,,'','','','',)