using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication3.HelperClasses;

namespace WebApplication3.Models
{

    public class StudentDbConnectionClass
    {
        public static List<Section> searchSections(String searchType, String searchParameter, String searchYear, String searchSemester)
        {
            List<Section> sections = new List<Section>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT [section_id],[course_id],[course_name],[instructor],[days],[start_time],[end_time],[semster],[year],[building_full_name],[room_number],[type],[capactiy],[seats_remaining] FROM [HarborViewUniversity].[dbo].[section_view] WHERE " + searchType + " LIKE '%" + searchParameter + "%' AND [year] = '" + searchYear + "' AND semster = '" + searchSemester + "' " +
                "ORDER BY course_id";
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

        public static List<Hold> viewHolds(String userID)
        {
            List<Hold> holds = new List<Hold>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT  [hold_type_name] ,[semester] ,[year] FROM [HarborViewUniversity].[dbo].[holds_view] WHERE [user_id] = " + userID;
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        holds.Add(new Hold(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                    }
                }
                connection.Close();
            }

            return holds;
        }

        public static List<Advisor> viewAdvisor(String userID)
        {
            List<Advisor> advisors = new List<Advisor>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT [faculty_name], [department_full_name], [faculty_email] FROM [HarborViewUniversity].[dbo].[advisor_view] WHERE [student_id] = " + userID;
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        advisors.Add(new Advisor(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                    }
                }
                connection.Close();
            }

            return advisors;
        }

        public static SectionSearchHelper createSectionSearchHelper()
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String deptString = "SELECT [department_full_name] FROM [HarborViewUniversity].[dbo].[department]";
            String dayString = "SELECT DISTINCT [days] FROM [HarborViewUniversity].[dbo].[section_view]";
            String timeString = "SELECT DISTINCT FORMAT(CAST([start_time] AS datetime), 'h:mm tt') AS start_time, FORMAT(CAST([end_time] AS datetime), 'h:mm tt') AS end_time, CAST([end_time] AS datetime) AS order_time FROM [HarborViewUniversity].[dbo].[time_slot] ORDER BY order_time";
            String yearString = "SELECT DISTINCT [year] FROM [HarborViewUniversity].[dbo].[section_view]";
            String semesterString = "SELECT DISTINCT [semster] FROM [HarborViewUniversity].[dbo].[section_view]";
            List<String> departments = new List<string>();
            List<String> days = new List<string>();
            List<String> times = new List<string>();
            List<String> years = new List<string>();
            List<String> semesters = new List<string>();
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

                SqlCommand command4 = new SqlCommand(yearString, connection);
                using (var reader = command4.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        years.Add(reader.GetString(0));
                    }
                }

                SqlCommand command5 = new SqlCommand(semesterString, connection);
                using (var reader = command5.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        semesters.Add(reader.GetString(0));
                    }
                }

                connection.Close();
            }

            return new SectionSearchHelper(departments, times, days, years, semesters);
        }

        public static ViewScheduleHelper createScheduleViewHelper()
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String yearString = "SELECT DISTINCT [year] FROM [HarborViewUniversity].[dbo].[section_view]";
            String semesterString = "SELECT DISTINCT [semster] FROM [HarborViewUniversity].[dbo].[section_view]";
            List<String> years = new List<string>();
            List<String> semesters = new List<string>();
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command4 = new SqlCommand(yearString, connection);
                connection.Open();
                using (var reader = command4.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        years.Add(reader.GetString(0));
                    }
                }

                SqlCommand command5 = new SqlCommand(semesterString, connection);
                using (var reader = command5.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        semesters.Add(reader.GetString(0));
                    }
                }

                connection.Close();
            }

            return new ViewScheduleHelper(years, semesters);
        }

        public static List<Enrollment> viewSchedule(String userID, String year, String semester)
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT [course_id],[course_name],[instructor],[days],[start_time],[end_time],[semster],[year],[type],[building_full_name],[room_number] FROM [HarborViewUniversity].[dbo].[enrollment_view] WHERE [user_id] = " + userID + " AND [semster] = '" + semester + "' AND [year] = '" + year + "'";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        enrollments.Add(new Enrollment(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6)
                            , reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetInt32(10).ToString()));
                    }
                }
                connection.Close();
            }

            return enrollments;
        }

        public static String register(int sectionID, int studentID, String year, String semester)
        {
            if (!canAdd(int.Parse(year)))
            {
                return "Error: That semester is no longer available to have classes added!";
            }
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String capacityString = "select (capactiy - seats_taken) as seats_remaining,course_name, course_credits from section inner join course on course.course_id = section.course_id WHERE section_id = " + sectionID;
            String ftString = "select max_credits from student_full_time where student_id = " + studentID + " and semester = '" + semester + "' and [year] = '" + year + "'";
            String ptString = "select max_credits from student_part_time where student_id = " + studentID + " and semester = '" + semester + "' and [year] = '" + year + "'";
            String creditsString = "SELECT COALESCE((select sum(c.course_credits) as credits_count from enrollment e inner join section s on s.section_id = e.section_id inner join course c on c.course_id = s.course_id where student_id = " + studentID + " and s.semster = '" + SemesterDataHelper.getSemesterSeason() + "' and [year] = '" + SemesterDataHelper.getSemesterYear() + "' group by course_credits), 0) AS 'credit_count'";
            String insertString = " INSERT INTO enrollment(student_id, section_id) VALUES(" + studentID + ", " + sectionID + ")";
            String getToBeEnrolledString = "SELECT start_time, day_1, day_2, day_3 FROM section JOIN time_slot on section.time_slot_id = time_slot.[period]";
            String getCurrentScheduleString = "SELECT start_time, day_1, day_2, day_3 FROM [dbo].[enrollment] JOIN section on section.section_id = enrollment.section_id JOIN time_slot on section.time_slot_id = time_slot.[period] WHERE [year] = '2019' AND semster = 'fall' AND student_id = 1";
            String courseName = "ERROR: Unable to connect to database!";
            String result = "";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                //Check seat capacity
                SqlCommand command = new SqlCommand(capacityString, connection);
                connection.Open();
                using (var reader1 = command.ExecuteReader())
                {
                    if (reader1.Read())
                    {
                        courseName = reader1.GetString(1);
                        if (reader1.GetByte(0) > 1)
                        {
                            int newClassCredits = reader1.GetByte(2);
                            //Check credits currently being takem
                            command = new SqlCommand(getToBeEnrolledString, connection);
                            reader1.Close();
                            using (var reader5 = command.ExecuteReader())
                            {
                                Section newSection;
                                if(reader5.Read())
                                {
                                    newSection = new Section(reader5.GetString(0), reader5.GetString(1), reader5.GetString(2), reader5.GetString(3));
                                    command = new SqlCommand(getCurrentScheduleString, connection);
                                    reader5.Close();
                                    using (var reader6 = command.ExecuteReader())
                                    {
                                        List<Section> scheduleList = new List<Section>();
                                        while (reader6.Read())
                                        {
                                            scheduleList.Add(new Section(reader6.GetString(0), reader6.GetString(1), reader6.GetString(2), reader6.GetString(3)));
                                        }
                                        foreach (Section sec in scheduleList)
                                        {
                                            if (newSection.day1.Equals(sec.day1)  || newSection.Equals(sec.day2) || newSection.day1.Equals(sec.day3))
                                            {
                                                result = "Error: You have a class at this time already!";
                                            }
                                            else
                                            {
                                                if (newSection.day2.Equals(sec.day1) || newSection.day2.Equals(sec.day2) || newSection.day2.Equals(sec.day3))
                                                {
                                                    result = "Error: You have a class at this time already!";
                                                }
                                                else
                                                {
                                                    if (newSection.day3.Equals(sec.day1) || newSection.day3.Equals(sec.day2) || newSection.day3.Equals(sec.day3))
                                                    {
                                                        result = "Error: You have a class at this time already!";
                                                    }
                                                    else
                                                    {
                                                        command = new SqlCommand(creditsString, connection);
                                                        reader5.Close();
                                                        using (var reader2 = command.ExecuteReader())
                                                        {
                                                            if (reader2.Read())
                                                            {
                                                                int currentCredits = reader2.GetInt32(0);
                                                                int test1 = currentCredits + newClassCredits;
                                                                //check if full-time
                                                                command = new SqlCommand(ftString, connection);
                                                                reader2.Close();
                                                                using (var reader3 = command.ExecuteReader())
                                                                {
                                                                    //if full time get max credits
                                                                    if (reader3.Read())
                                                                    {
                                                                        int test2 = reader3.GetByte(0);
                                                                        if ((currentCredits + newClassCredits <= reader3.GetByte(0)))
                                                                        {
                                                                            try
                                                                            {
                                                                                //do insert
                                                                                command = new SqlCommand(insertString, connection);
                                                                                command.ExecuteNonQuery();
                                                                                result = "You have successfully registered for " + courseName;
                                                                                reader3.Close();
                                                                            }
                                                                            catch
                                                                            {
                                                                                result = "You are already registered for " + courseName;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            result = "You cannot exceed the maximum credit limit!";
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        //Check if part time
                                                                        command = new SqlCommand(ptString, connection);
                                                                        reader3.Close();
                                                                        using (var reader4 = command.ExecuteReader())
                                                                        {
                                                                            if (reader4.Read())
                                                                            {
                                                                                if (currentCredits + newClassCredits <= reader4.GetByte(0))
                                                                                {
                                                                                    try
                                                                                    {
                                                                                        //yeet it into the table
                                                                                        command = new SqlCommand(insertString, connection);
                                                                                        command.ExecuteNonQuery();
                                                                                        result = "You have successfully registered for " + courseName;
                                                                                        reader4.Close();
                                                                                    }
                                                                                    catch
                                                                                    {
                                                                                        result = "You are already registered for " + courseName;
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    result = "You cannot exceed the maximum credit limit!";
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                result = "ERROR: You are not currently a student.";
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                            }

                        }
                        else
                        {
                            result = "Unable to add " + courseName + ". Section is full.";
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }

        private static bool canAdd(int year)
        {
            if (SemesterDataHelper.getSemesterSeason().Equals("Spring"))
            {
                if(year >= int.Parse( SemesterDataHelper.getSemesterYear()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (year > int.Parse(SemesterDataHelper.getSemesterYear()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

}