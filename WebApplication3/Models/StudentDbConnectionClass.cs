using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication3.HelperClasses;

namespace WebApplication3.Models
{

    public class StudentDbConnectionClass
    {
        public static List<Section> displayEnrollables(String searchYear, String searchSemester, String instructor, String days, String time, String courseID, String courseName, String department)
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
            String deptString = "SELECT [department_full_name] FROM [HarborViewUniversity].[dbo].[department] WHERE department_id != 11";
            String dayString = "SELECT DISTINCT [days] FROM [HarborViewUniversity].[dbo].[section_view]";
            String timeString = "SELECT DISTINCT FORMAT(CAST([start_time] AS datetime), 'h:mm tt') AS start_time, CAST([start_time] AS datetime) AS order_time FROM [HarborViewUniversity].[dbo].[time_slot] ORDER BY order_time";
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

        public static EnrollmentSemesterHelper createAddEnrollmentViewScheduleHelper()
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String deptString = "SELECT [department_full_name] FROM [HarborViewUniversity].[dbo].[department] WHERE department_id != 11";
            String dayString = "SELECT DISTINCT [days] FROM [HarborViewUniversity].[dbo].[section_view]";
            String timeString = "SELECT DISTINCT FORMAT(CAST([start_time] AS datetime), 'h:mm tt') AS start_time, FORMAT(CAST([end_time] AS datetime), 'h:mm tt') AS end_time, CAST([end_time] AS datetime) AS order_time FROM [HarborViewUniversity].[dbo].[time_slot] ORDER BY order_time";
            List<String> departments = new List<string>();
            List<String> days = new List<string>();
            List<String> times = new List<string>();
            List<String> semesters = new List<string>();
            //Grab semester and year selector data
            if (SemesterDataHelper.canRegisterForCurrentSemester())
            {
                semesters.Add(SemesterDataHelper.getSemesterSeason() + " " + SemesterDataHelper.getSemesterYear());
            }
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

        public static EnrollmentSemesterHelper createRemoveEnrollmentViewScheduleHelper()
        {
            List<String> semesters = new List<string>();
            if (SemesterDataHelper.canDropForCurrentSemester())
            {
                semesters.Add(SemesterDataHelper.getSemesterSeason() + " " + SemesterDataHelper.getSemesterYear());
            }
            semesters.Add(SemesterDataHelper.getNextSemesterSeason() + " " + SemesterDataHelper.getNextSemesterYear());

            return new EnrollmentSemesterHelper(semesters);
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
            String capacityString = "select (capactiy - seats_taken) as seats_remaining,course_name, course_credits, course.course_id from section inner join course on course.course_id = section.course_id WHERE section_id = " + sectionID;
            String ftString = "select max_credits from student_full_time where student_id = " + studentID + " and semester = '" + semester + "' and [year] = '" + year + "'";
            String ptString = "select max_credits from student_part_time where student_id = " + studentID + " and semester = '" + semester + "' and [year] = '" + year + "'";
            String creditsString = "SELECT COALESCE((select sum(c.course_credits) as credits_count from enrollment e inner join section s on s.section_id = e.section_id inner join course c on c.course_id = s.course_id where student_id = " + studentID + " and s.semster = '" + semester + "' and [year] = '" + year + "' group by course_credits), 0) AS 'credit_count'";
            String insertString = " INSERT INTO enrollment(student_id, section_id) VALUES(" + studentID + ", " + sectionID + ")";
            String getToBeEnrolledString = "SELECT start_time, day_1, day_2, day_3 FROM section JOIN time_slot on section.time_slot_id = time_slot.[period] WHERE section_id = " + sectionID;
            String getCurrentScheduleString = "SELECT start_time, day_1, day_2, day_3 FROM [dbo].[enrollment] JOIN section on section.section_id = enrollment.section_id JOIN time_slot on section.time_slot_id = time_slot.[period] WHERE [year] = '" + year + "' AND semster = '" + semester + "' AND student_id = " + studentID;
            String holdString = "SELECT [hold_type_name] FROM [HarborViewUniversity].[dbo].[holds_view] WHERE [user_id] = " + studentID;
            String takeSeatString = "UPDATE [dbo].[section] SET [seats_taken] = [seats_taken] + 1 WHERE section_id = " + sectionID;
            String courseID = "";
            String courseName = "ERROR: Unable to connect to database!";
            Section newSection;
            int newClassCredits = 0;
            int currentCredits = 0;
            int maxCredits = 0;
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
                        //check the remaining seats
                        int t1 = reader1.GetByte(0);
                        if (reader1.GetByte(0) >= 1)
                        {
                            //store new class credits
                            newClassCredits = reader1.GetByte(2);
                            courseID = reader1.GetInt32(3).ToString();
                        }
                        else
                        {
                            //close everything and report the error
                            reader1.Close();
                            connection.Close();
                            return "Unable to add " + courseName + ". Section is full.";
                        }
                        reader1.Close();
                    }

                }

                //Check if student has holds
                command = new SqlCommand(holdString, connection);
                using (var reader7 = command.ExecuteReader())
                {
                    List<String> holds = new List<String>();
                    while (reader7.Read())
                    {
                        holds.Add(reader7.GetString(0));
                    }
                    if (holds.Count > 0)
                    {
                        String holdsResult = "\\n";
                        foreach (String s in holds)
                        {
                            holdsResult += s + "\\n";
                        }
                        //close everything and report the error
                        reader7.Close();
                        connection.Close();
                        return "Unable to add " + courseName + ". You have holds:" + holdsResult;
                    }
                }

                //Check time conflict
                command = new SqlCommand(getToBeEnrolledString, connection);
                using (var reader5 = command.ExecuteReader())
                {
                    //Check time conflict
                    if (reader5.Read())
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
                            reader6.Close();
                            foreach (Section sec in scheduleList)
                            {
                                String secStart = sec.startTime;
                                String newStart = newSection.startTime;
                                if (sec.startTime.Equals(newSection.startTime))
                                {
                                    if (sec.day3.Equals(""))
                                    {
                                        if (newSection.day1.Equals(sec.day1) || newSection.day2.Equals(sec.day2))
                                        {
                                            reader6.Close();
                                            connection.Close();
                                            return "Error: You have a class at this time already!";
                                        }
                                        else
                                        {
                                            if (newSection.day2.Equals(sec.day1) || newSection.day2.Equals(sec.day2))
                                            {
                                                reader6.Close();
                                                connection.Close();
                                                return "Error: You have a class at this time already!";
                                            }
                                            else
                                            {
                                                if (newSection.day3.Equals(sec.day1) || newSection.day3.Equals(sec.day2))
                                                {
                                                    reader6.Close();
                                                    connection.Close();
                                                    return "Error: You have a class at this time already!";
                                                }
                                            }
                                        }
                                    }
                                    else if (sec.day3.Equals("") && sec.day2.Equals(""))
                                    {
                                        if (newSection.day1.Equals(sec.day1))
                                        {
                                            reader6.Close();
                                            connection.Close();
                                            return "Error: You have a class at this time already!";
                                        }
                                        else
                                        {
                                            if (newSection.day2.Equals(sec.day1))
                                            {
                                                reader6.Close();
                                                connection.Close();
                                                return "Error: You have a class at this time already!";
                                            }
                                            else
                                            {
                                                if (newSection.day3.Equals(sec.day1))
                                                {
                                                    reader6.Close();
                                                    connection.Close();
                                                    return "Error: You have a class at this time already!";
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (newSection.day1.Equals(sec.day1) || newSection.Equals(sec.day2) || newSection.day1.Equals(sec.day3))
                                        {
                                            reader6.Close();
                                            connection.Close();
                                            return "Error: You have a class at this time already!";
                                        }
                                        else
                                        {
                                            if (newSection.day2.Equals(sec.day1) || newSection.day2.Equals(sec.day2) || newSection.day2.Equals(sec.day3))
                                            {
                                                reader6.Close();
                                                connection.Close();
                                                return "Error: You have a class at this time already!";
                                            }
                                            else
                                            {
                                                if (newSection.day3.Equals(sec.day1) || newSection.day3.Equals(sec.day2) || newSection.day3.Equals(sec.day3))
                                                {
                                                    reader6.Close();
                                                    connection.Close();
                                                    return "Error: You have a class at this time already!";
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }

                //Check prereqs are met
                String prereqString = "SELECT [pre_req_course_id] FROM [HarborViewUniversity].[dbo].[prereq] WHERE course_id = " + courseID;
                String getCoursesTakenString = "SELECT [course_id] student_id FROM [HarborViewUniversity].[dbo].[student_semester_history] JOIN section sec on sec.section_id = student_semester_history.section_id WHERE grade NOT IN ('C-','D+','D','D-','F') AND [student_id] = " + studentID;
                command = new SqlCommand(prereqString, connection);
                using (var reader8 = command.ExecuteReader())
                {
                    List<int> prereqs = new List<int>();
                    List<int> coursesTaken = new List<int>();
                    while (reader8.Read())
                    {
                        prereqs.Add(reader8.GetInt32(0));
                    }
                    reader8.Close();
                    if (prereqs.Count > 0)
                    {
                        command = new SqlCommand(getCoursesTakenString, connection);
                        using (var reader9 = command.ExecuteReader())
                        {
                            while (reader9.Read())
                            {
                                coursesTaken.Add(reader9.GetInt32(0));
                            }
                            reader9.Close();
                        }
                        if (coursesTaken.Count < 1)
                        {
                            return "Error: You do not meet the prerequisites for this class!";
                        }
                        else
                        {
                            int count = 0;
                            foreach (int prereq in prereqs)
                            {
                                foreach (int courseTaken in coursesTaken)
                                {
                                    if (prereq == courseTaken)
                                    {
                                        count++;
                                    }
                                }
                            }
                            if (count != prereqs.Count)
                            {
                                return "Error: You do not meet the prerequisites for this class!";
                            }
                        }
                    }
                }

                //Get current credits total for student
                command = new SqlCommand(creditsString, connection);
                using (var reader2 = command.ExecuteReader())
                {
                    if (reader2.Read())
                    {
                        currentCredits = reader2.GetInt32(0);
                    }
                    reader2.Close();
                }

                //check if full-time
                command = new SqlCommand(ftString, connection);
                using (var reader3 = command.ExecuteReader())
                {
                    //if full time get max credits
                    if (reader3.Read())
                    {
                        maxCredits = reader3.GetByte(0);
                    }
                    reader3.Close();
                }

                //Check if part time
                command = new SqlCommand(ptString, connection);
                using (var reader4 = command.ExecuteReader())
                {
                    //if part time get max credits
                    if (reader4.Read())
                    {
                        maxCredits = reader4.GetByte(0);
                    }
                    reader4.Close();
                }

                //if max credits is still 0 they aren't registered as a student in the current semester
                if (maxCredits == 0)
                {
                    connection.Close();
                    return "ERROR: You are not a student!";
                }

                //Test for max credits
                if (currentCredits + newClassCredits >= maxCredits)
                {
                    connection.Close();
                    return "ERROR: That would put you over your max credits!";
                }
                else
                {
                    try
                    {
                        //do insert
                        command = new SqlCommand(insertString, connection);
                        command.ExecuteNonQuery();
                        command = new SqlCommand(takeSeatString, connection);
                        command.ExecuteNonQuery();
                        connection.Close();
                        return "You have successfully registered for " + courseName;
                    }
                    catch
                    {
                        connection.Close();
                        return "You are already registered for " + courseName;
                    }
                }
            };
        }

        private static bool canAdd(int year)
        {
            if (SemesterDataHelper.getSemesterSeason().Equals("Spring"))
            {
                if (year >= int.Parse(SemesterDataHelper.getSemesterYear()))
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

        public static List<Section> submitDropClass(String userID, String year, String semester)
        {
            List<Section> sections = new List<Section>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT [section_id],[course_id],[course_name],[professor],[days],[start_time],[end_time],[semster],[year],[user_id] FROM [HarborViewUniversity].[dbo].[remove_enrollment] WHERE [user_id] = " + userID + " AND [semster] = '" + semester + "' AND [year] = '" + year + "'";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sections.Add(new Section(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
                            reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8)));
                    }
                }
                connection.Close();
            }

            return sections;
        }

        //public static List<Section> removeEnrollment(int userID, int sectionID)
        public static String removeEnrollment(int userID, int sectionID, String semester, String year)
        {
            List<Section> sections = new List<Section>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String deleteString = "DELETE FROM enrollment WHERE student_id = " + userID + " and section_id = " + sectionID + "";
            String updateEnrollmentCount = "UPDATE [dbo].[section] SET [seats_taken] = [seats_taken] - 1 WHERE section_id = " + sectionID + "";

            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(deleteString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sections.Add(new Section(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7)
                            , reader.GetString(8), reader.GetString(9), reader.GetInt32(10), reader.GetString(11), reader.GetByte(12), reader.GetByte(13)));
                    }
                }
                SqlCommand command2 = new SqlCommand(updateEnrollmentCount, connection);
                command2.ExecuteNonQuery();
                connection.Close();
            }

            return "You Have Succesfully Removed The Section";
        }

        public static StudentTranscriptHelper viewTranscript(int userId)
        {

            List<Section> sectionList = new List<Section>();
            StudentInfo studentInfo = new StudentInfo();
            //StudentTranscriptHelper studentTranscript = new StudentTranscriptHelper(sectionList, studentInfo);
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String getSectionDetails = @"SELECT sv.[section_id],[course_id],sv.[course_name],[instructor],[days],[start_time],[end_time],[semster],sv.[year] ,[type],[building_full_name],[room_number],sh.grade FROM [HarborViewUniversity].[dbo].[section_view] sv inner join student_semester_history sh on sh.section_id = sv .section_id WHERE student_id = " + userId + " ORDER BY [year], semster";
            //Gets the Section details
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(getSectionDetails, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sectionList.Add(new Section(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7)
                         , reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetInt32(11), reader.GetString(12)));
                    }
                }
                connection.Close();
            }
            //Get student info
            String cString2 = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String getStudentInfo = "select * FROM [HarborViewUniversity].[dbo].[student_info] WHERE user_id = " + userId;
            using (SqlConnection connection = new SqlConnection(cString2))
            {
                SqlCommand command = new SqlCommand(getStudentInfo, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        studentInfo.studentID = reader.GetInt32(0).ToString();
                        studentInfo.firstName = reader.GetString(1);
                        studentInfo.lastName = reader.GetString(2);
                        studentInfo.email = reader.GetString(3);
                        studentInfo.dob = reader.GetDateTime(4).ToShortDateString();
                        studentInfo.phoneNumber = reader.GetString(5);
                        studentInfo.street = reader.GetString(6);
                        studentInfo.city = reader.GetString(7);
                        studentInfo.state = reader.GetString(8);
                        studentInfo.zip = reader.GetInt32(9).ToString();
                        studentInfo.major = reader.GetString(10);
                        studentInfo.minor = reader.GetString(11);
                    }
                }
                connection.Close();
            }
            StudentTranscriptHelper studentTranscript = new StudentTranscriptHelper(sectionList, studentInfo);
            return studentTranscript;
        }


        public static List<Major> createDegreeAuditSelector(String studentID)
        {
            String getStudentInfoString = "select * FROM [HarborViewUniversity].[dbo].[student_info] WHERE user_id = " + studentID;
            String getMajorString = "SELECT m.major_id,major_name FROM student_major_list sml INNER JOIN major m ON m.major_id = sml.major_id WHERE student_id = " + studentID;
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            List<Major> studentsMajors = new List<Major>();
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(getMajorString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        studentsMajors.Add(new Major(reader.GetInt32(0).ToString(), reader.GetString(1)));
                    }
                }
            }
            return studentsMajors;
        }



        public static DegreeAuditData degreeAudit(String studentID, String majorID)
        {
            //SQL Statements for major reqs
            String getMajorInfoString = @"SELECT c.course_id,CONCAT(d.department_short_name,c.course_num)AS course_number,c.course_name,c.course_credits
                                            FROM major_requirements mr
                                            INNER JOIN course c ON c.course_id = mr.course_id
                                            INNER JOIN major m ON m.major_id = mr.major_id
                                            INNER JOIN department d ON d.department_id = m.department_id
                                            WHERE m.major_id = " + majorID;
            //SQL Statements for electives
            String getMajorElectivesString = @"SELECT c.course_id, CONCAT(d.department_short_name, c.course_num) AS 'course_num' ,c.course_name, c.course_credits
                                                FROM [HarborViewUniversity].[dbo].[major_elective] m
                                                JOIN course c ON c.course_id = m.course_id
                                                JOIN department d ON c.department_id = d.department_id
                                                WHERE m.major_id = " + majorID;
            //SQL to get classes already taken
            String getClassesTakenString = "SELECT DISTINCT s.course_id,sh.grade FROM student_semester_history sh INNER JOIN section s ON s.section_id = sh.section_id INNER JOIN course c ON c.course_id = s.course_id inner join department d on d.department_id = c.department_id INNER join major m on m.department_id = d.department_id where sh.student_id = " + studentID;
            //SQL to get out of major classes taken
            String getOutOfMajorClassesTaken = "SELECT [course_id], [course_number], [course_name], [prereqs], [course_credits], [grade] FROM out_of_major_reqs_view WHERE student_id = " + studentID + "AND course_id NOT IN(select course_id from major_requirements where major_id = " + majorID + ") AND course_id NOT IN(select course_id from major_elective where major_id = " + majorID + ")";
            //SQL to get classes currently being taken
            String getClassesInProgressString = "SELECT s.course_id FROM enrollment e INNER JOIN section s ON s.section_id = e.section_id WHERE s.semster = 'spring' AND s.[year] = '2019' AND e.student_id = " + studentID;
            //SQL to get prereqs for major courses
            String getPrereqsString = "SELECT [course_id] ,[prereq_course_id] ,[prereq_course_name] FROM [HarborViewUniversity].[dbo].[prereq_view] WHERE [major_id] = " + majorID;
            //SQL to get requirements for minor
            String getMinorReqsString = "SELECT c.course_id,CONCAT(d.department_short_name,c.course_num)AS course_number,c.course_name,c.course_credits FROM minor_requirements mr INNER JOIN course c ON c.course_id = mr.course_id INNER JOIN minor m ON m.minor_id = mr.minor_id INNER JOIN department d ON d.department_id = m.department_id WHERE m.minor_id = (SELECT [minor_id] FROM [student_minor_list] WHERE [student_id] = " + studentID + ")";
            //SQL to get prereqs for minor courses
            String getMinorPrereqsString = "SELECT[course_id],[prereq_course_id] ,[prereq_course_name] FROM[HarborViewUniversity].[dbo].[prereq_view_minors] WHERE[minor_id] = (SELECT[minor_id] FROM [student_minor_list] WHERE[student_id] = " + studentID + ")";
            //Connection String
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            //List of major reqs
            List<DegreeAuditMajorReqs> majorReqs = new List<DegreeAuditMajorReqs>();
            //List of courses taken
            Hashtable coursesTaken = new Hashtable();
            //Prerequisites for major courses
            DataTable majorPrereqTable = new DataTable();
            //Prerequisites for minor courses
            DataTable minorPrereqTable = new DataTable();
            //List of courses currently being taken
            List<string> inProgress = new List<string>();
            //Lists for major electives
            List<DegreeAuditElectives> majorElectives = new List<DegreeAuditElectives>();
            //List of out of major classes taken
            List<DegreeAuditOutOfMajorReqs> outOfMajorReqs = new List<DegreeAuditOutOfMajorReqs>();
            //List of Minor Requirements
            List<DegreeAuditMajorReqs> minorReqs = new List<DegreeAuditMajorReqs>();
            //Create connect
            using (SqlConnection connection = new SqlConnection(cString))
            {
                //Major Requirements
                //Get list of major requirements
                SqlCommand command = new SqlCommand(getMajorInfoString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        majorReqs.Add(new DegreeAuditMajorReqs(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetByte(3)));
                    }
                }

                //Get classes taken by student
                SqlCommand command2 = new SqlCommand(getClassesTakenString, connection);
                using (var reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        coursesTaken.Add(reader.GetInt32(0).ToString(), reader.GetString(1));
                    }
                }

                //Get prereqs for major reqs
                SqlCommand cmd = new SqlCommand(getPrereqsString, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(majorPrereqTable);
                da.Dispose();

                //Major Electives
                //get list of major electives
                SqlCommand command3 = new SqlCommand(getMajorElectivesString, connection);
                using (var reader = command3.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        majorElectives.Add(new DegreeAuditElectives(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetByte(3)));
                    }
                }
                //Get classes currently being taken
                SqlCommand command4 = new SqlCommand(getClassesInProgressString, connection);
                using (var reader = command4.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        inProgress.Add(reader.GetInt32(0).ToString());
                    }
                }

                //Out of Major Requirements
                SqlCommand command5 = new SqlCommand(getOutOfMajorClassesTaken, connection);
                using (var reader = command5.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //[course_id], [course_number], [course_name], [prereqs], [course_credits], [grade]
                        string courseId;
                        string courseNumber;
                        string courseName;
                        string prereqs;
                        int courseCredits;
                        string grade;

                        try { courseId = reader.GetInt32(0).ToString(); } catch { courseId = ""; }
                        try { courseNumber = reader.GetString(1); } catch { courseNumber = ""; }
                        try { courseName = reader.GetString(2); } catch { courseName = ""; }
                        try { prereqs = reader.GetString(3); } catch { prereqs = ""; }
                        try { courseCredits = reader.GetByte(4); } catch { courseCredits = 0; }
                        try { grade = reader.GetString(5); } catch { grade = ""; }

                        outOfMajorReqs.Add(new DegreeAuditOutOfMajorReqs(courseId, courseNumber, courseName, prereqs, courseCredits, grade));
                        
                    }
                }

                //Minor Requirements
                //Get list of minor requirements
                SqlCommand command6 = new SqlCommand(getMinorReqsString, connection);
                using (var reader = command6.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        minorReqs.Add(new DegreeAuditMajorReqs(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetByte(3)));
                    }
                }

                //Get prereqs for minor reqs
                SqlCommand cmd2 = new SqlCommand(getMinorPrereqsString, connection);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da2.Fill(minorPrereqTable);
                da2.Dispose();

                connection.Close();
            }
            //Major reqs data processing
            foreach (var mr in majorReqs)
            {
                //if course is in progress, mark as in progress
                if (inProgress.Contains(mr.courseID))
                {
                    mr.courseStatus = "&#x2610";
                }
                //if taken course is passed, mark as complete
                if (coursesTaken.Contains(mr.courseID))
                {
                    if (GradeList.isPassing((string)coursesTaken[mr.courseID]))
                    {
                        mr.courseStatus = "&#x2611";
                    }
                }
                //foreach buildingid
                //listofroomsforbuildingx.add( datarow dr prereqTable.Select(where buildingid = currentbuilingid))

                foreach (DataRow dr in majorPrereqTable.Select("course_id = '" + mr.courseID + "'"))
                {
                    if (coursesTaken.Contains(dr[1] + ""))
                    {
                        mr.prereqsTaken += " " + dr[2] + ",";
                        mr.grade = (string)coursesTaken[mr.courseID];
                    }
                    else
                    {
                        mr.prereqsToTake += " " + dr[2] + ",";
                    }
                }
                if (coursesTaken.Contains(mr.courseID + ""))
                {
                    mr.grade = (string)coursesTaken[mr.courseID];
                }
            }
            //major electives data processing
            foreach (var el in majorElectives)
            {
                //if course is in progress, mark as in progreess
                if (inProgress.Contains(el.courseID))
                {
                    el.courseStatus = "&#x2610";
                }

                //if taken course is passed, mark as complete
                if (coursesTaken.Contains(el.courseID))
                {
                    if (GradeList.isPassing((string)coursesTaken[el.courseID]))
                    {
                        el.courseStatus = "&#x2611";
                        el.grade = (string)coursesTaken[el.courseID];
                    }
                }

                //sort and add prereqs
                foreach (DataRow dr in majorPrereqTable.Select("course_id = '" + el.courseID + "'"))
                {
                    if (coursesTaken.Contains(dr[1] + ""))
                    {
                        el.prereqsTaken += " " + dr[2] + ",";
                    }
                    else
                    {
                        el.prereqsToTake += " " + dr[2] + ",";
                    }
                }

                if (coursesTaken.Contains(el.courseID))
                {
                    el.grade = (string)coursesTaken[el.courseID];
                }
            }

            //Out of Major Requirements data processing
            foreach (var oom in outOfMajorReqs)
            {
                if (oom.grade.Equals(""))
                {
                    oom.courseStatus = "&#x2610";
                }
                else
                {
                    if (GradeList.isPassing(oom.grade))
                    {
                        oom.courseStatus = "&#x2611";
                    }
                    else
                    {
                        oom.courseStatus = "&#x2612";
                    }
                }
            }

            foreach (var min in minorReqs)
            {
                //if course is in progress, mark as in progreess
                if (inProgress.Contains(min.courseID))
                {
                    min.courseStatus = "&#x2610";
                }

                //if taken course is passed, mark as complete
                if (coursesTaken.Contains(min.courseID))
                {
                    if (GradeList.isPassing((string)coursesTaken[min.courseID]))
                    {
                        min.courseStatus = "&#x2611";
                        min.grade = (string)coursesTaken[min.courseID];
                    }
                }

                //sort and add prereqs
                foreach (DataRow dr in minorPrereqTable.Select("course_id = '" + min.courseID + "'"))
                {
                    if (coursesTaken.Contains(dr[1] + ""))
                    {
                        min.prereqsTaken += " " + dr[2] + ",";
                    }
                    else
                    {
                        min.prereqsToTake += " " + dr[2] + ",";
                    }
                }

                if (coursesTaken.Contains(min.courseID))
                {
                    min.grade = (string)coursesTaken[min.courseID];
                }
            }

            return new DegreeAuditData(majorReqs, majorElectives, outOfMajorReqs, minorReqs);
        }


    }

}