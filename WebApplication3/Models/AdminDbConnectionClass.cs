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
    public class AdminDbConnectionClass
    {
        public static List<Department> getDepartmentInfo()
        {
            List<Department> departments = new List<Department>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "select department_id,department_full_name from department";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departments.Add(new Department(reader.GetInt32(0).ToString(), reader.GetString(1)));
                    }
                }
                connection.Close();
            }
            return departments;
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

        public static List<Enrollment> viewStudentSchedule(String userID, String year, String semester)
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
        public static EnrollmentSemesterHelper createViewStudentScheduleHelper()
        {
            List<String> semesters = new List<string>();
            semesters.Add(SemesterDataHelper.getSemesterSeason() + " " + SemesterDataHelper.getSemesterYear());
            semesters.Add(SemesterDataHelper.getNextSemesterSeason() + " " + SemesterDataHelper.getNextSemesterYear());

            return new EnrollmentSemesterHelper(semesters);
        }

        public static StudentTranscriptHelper viewTranscript(int studentId)
        {

            List<Section> sectionList = new List<Section>();
            StudentInfo studentInfo = new StudentInfo();
            //StudentTranscriptHelper studentTranscript = new StudentTranscriptHelper(sectionList, studentInfo);
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String getSectionDetails = @"SELECT sv.[section_id],[course_id],sv.[course_name],[instructor],[days],[start_time],[end_time],[semster],sv.[year] ,[type],[building_full_name],[room_number],sh.grade FROM [HarborViewUniversity].[dbo].[section_view] sv inner join student_semester_history sh on sh.section_id = sv .section_id WHERE student_id = " + studentId + " ORDER BY [year], semster";
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
            String getStudentInfo = "select * FROM [HarborViewUniversity].[dbo].[student_info] WHERE user_id = " + studentId;
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

        public static List<Advisor> createAdvisorSelectorHelper()
        {
            List<Advisor> advisors = new List<Advisor>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT DISTINCT [faculty_id],[faculty_name] FROM [HarborViewUniversity].[dbo].[advisor_view]";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        advisors.Add(new Advisor(reader.GetInt32(0).ToString(), reader.GetString(1)));
                    }
                }
                connection.Close();
            }

            return advisors;
        }

        public static List<Advisee> viewAdvisorAdviseeList(String userID)
        {
            List<Advisee> adviseeList = new List<Advisee>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT first_name,last_name,phone_number,dob,street_name,city,state,zip,student_id FROM [HarborViewUniversity].[dbo].[advisor_view] av inner join [user] u on u.user_id = av.student_id where av.faculty_id = " + userID + "";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        adviseeList.Add(new Advisee(reader.GetInt32(8).ToString(), reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3).ToShortDateString(), reader.GetString(4),
                            reader.GetString(5), reader.GetString(6), reader.GetInt32(7).ToString()));
                    }
                }
                connection.Close();
            }

            return adviseeList;
        }

        public static List<FacultySchedule> viewFacultySchedule(String userID)
        {
            List<FacultySchedule> classList = new List<FacultySchedule>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT [course_name],[days],[start_time],[end_time],[semster],[year],[building_full_name],[room_number] FROM [HarborViewUniversity].[dbo].[faculty_schedule_view] WHERE [faculty_id] = " + userID;
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        classList.Add(new FacultySchedule(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6)
                            , reader.GetInt32(7).ToString()));
                    }
                }
                connection.Close();
            }

            return classList;
        }

        public static List<StudentEnrollment> createUpdateGradeList(String studentID)
        {
            List<StudentEnrollment> classesTaken = new List<StudentEnrollment>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = @"SELECT DISTINCT s.course_id,c.course_name,sh.section_id,sh.grade, sh.student_id
                                FROM student_semester_history sh
                                INNER JOIN section s ON s.section_id = sh.section_id
                                INNER JOIN course c ON c.course_id = s.course_id
                                where sh.student_id = " + studentID;
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        classesTaken.Add(new StudentEnrollment(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetInt32(2).ToString(), reader.GetString(3), reader.GetInt32(4).ToString()));
                    }
                }
                connection.Close();
            }

            return classesTaken;
        }

        public static String updateGrade(StudentEnrollment s)
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "UPDATE student_semester_history SET grade = '" + s.grade + "' WHERE student_id = " + s.studentID + " AND section_id = " + s.sectionID;
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            return "";
        }


        public static AddCourse addCourseHelper2()
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String deptString = "SELECT [department_full_name] FROM [HarborViewUniversity].[dbo].[department] order by department_full_name";
            String courseString = "select course_name from course order by course_name";
            //String majorString = "SELECT [major_name] FROM [HarborViewUniversity].[dbo].[major]";
            //String minorString = "SELECT [minor_name] FROM [HarborViewUniversity].[dbo].[minor]";
            List<Department> departments = new List<Department>();
            List<Course> courses = new List<Course>();
            //List<Major> majors = new List<Major>();
            //List<Minor> minors = new List<Minor>();
            AddCourse helper = new AddCourse(departments, courses);
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand c1 = new SqlCommand(deptString, connection);
                connection.Open();
                using (var reader = c1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departments.Add(new Department(reader.GetString(0)));
                    }
                    connection.Close();
                }
                SqlCommand c2 = new SqlCommand(courseString, connection);
                connection.Open();
                using (var reader = c2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course(reader.GetString(0)));
                    }
                    connection.Close();
                }
                //SqlCommand c2 = new SqlCommand(majorString, connection);
                //connection.Open();
                //using (var reader = c2.ExecuteReader())
                //{
                //    while (reader.Read())
                //    {
                //        majors.Add(new Major(reader.GetString(0)));
                //    }
                //    connection.Close();
                //}
                //SqlCommand c3 = new SqlCommand(minorString, connection);
                //connection.Open();
                //using (var reader = c3.ExecuteReader())
                //{
                //    while (reader.Read())
                //    {
                //        minors.Add(new Minor(reader.GetString(0)));
                //    }
                //    connection.Close();
                //}
            }
            return helper;
        }

        public static String addCourse(AddCourse acForm)
        {
            string test = acForm.department;

            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String getCourseNumbersString = @"SELECT c.course_num FROM course c INNER JOIN department d ON d.department_id = c.department_id WHERE d.department_full_name = " + acForm.department;
            String getDeptId = " SELECT department_id FROM department WHERE department_full_name = " + acForm.department;
            String getPr1Id = "select course_id from course where course_name = " + acForm.pr1 + " AND department_id = " + getDeptId;
            String getPr2Id = "select course_id from course where course_name = " + acForm.pr2 + " AND department_id = " + getDeptId;
            String getCr1Id = "select course_id from course where course_name = " + acForm.cr1 + " AND department_id = " + getDeptId;
            String getCr2Id = "select course_id from course where course_name = " + acForm.cr2 + " AND department_id = " + getDeptId;
            List<String> currentCourseNumbers;
            String returnMessage = "Message FAIL";
            String newCourseID = "";
            //String getMajorId = "SELECT major_id FROM major WHERE major_name = " + acForm.major;
            //String getMinorId = "SELECT minor_id FROM minor WHERE minor_name = " + acForm.minor;
            //String majorReqTableInsert = "";
            //String minorReqTableInsert = "";
            try
            {  //check course number
                using (SqlConnection connection = new SqlConnection(cString))
                {
                    SqlCommand c1 = new SqlCommand(getCourseNumbersString, connection);
                    currentCourseNumbers = new List<string>();
                    connection.Open();
                    using (var reader = c1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            currentCourseNumbers.Add(reader.GetInt32(0).ToString());
                        }
                        connection.Close();
                    }

                }
                for (int i = 0; i < currentCourseNumbers.Count; i++) //check if duplicate course number exists
                {
                    if (acForm.courseNumber.Equals(currentCourseNumbers.ElementAt(i)))
                    {
                        return "This course number already exists in this department. Please select another course number";
                    }
                }
                //check elective validation
                if (acForm.isElective.Equals("Yes"))
                {
                    acForm.isElective.Equals("1");
                }
                else acForm.isElective.Equals("0");


                //check graduate course validation
                if (acForm.isGrad.Equals("Yes"))
                {
                    acForm.isGrad.Equals("1");
                }
                else acForm.isGrad.Equals("0");

                //var isMajorReq = Int32.Parse(acForm.isMajorReq);
                //var isMinorReq = Int32.Parse(acForm.isMinorReq);
                ////major req
                //if (acForm.isMajorReq.Equals("Yes")) {
                //    acForm.isMajorReq = "1";
                //}
                ////minor req 
                //if (acForm.isMinorReq.Equals("Yes")) {
                //    acForm.isMinorReq = "1";
                //}
                //  }


                //(<department_id, int,>
                // ,<course_num, int,>
                // ,<course_name, varchar(50),>
                // ,<course_description, varchar(max),>
                // ,<course_credits, tinyint,>
                // ,<is_elective, bit,>
                // ,<is_graduate_course, bit,>)
                var department = Int32.Parse(getDeptId);
                var courseNumber = Int32.Parse(acForm.courseNumber);
                var courseName = acForm.courseName;
                var description = acForm.description;
                int credits = Int32.Parse(acForm.credits);
                var isElective = Int32.Parse(acForm.isElective);
                var isGrad = Int32.Parse(acForm.isGrad);
                //var major = Int32.Parse(getMajorId);
                //var minor = Int32.Parse(getMinorId);

                String courseTableInsertString = @"INSERT INTO [dbo].[course]([department_id],[course_num],[course_name],[course_description],[course_credits],[is_elective],[is_graduate_course])
                                                    VALUES(" + department + "," + courseNumber + ",'" + acForm.courseName + "','" + acForm.description + "','" + credits + "','" + isElective + "'," + isGrad + ")";
                using (SqlConnection connection = new SqlConnection(cString))
                {
                    SqlCommand command = new SqlCommand(courseTableInsertString, connection);
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                        returnMessage = "New Course Added Successfully";
                    }
                    catch
                    {
                        returnMessage = "ERROR: The Course Failed to Insert Correctly";
                    }
                    connection.Close();
                }
                var pr1 = acForm.pr1;
                var pr2 = acForm.pr2;
                var cr1 = acForm.cr1;
                var cr2 = acForm.cr2;
                //check pre req
                //get id of the new course number to use for insert into pre req tables
                String getNewCourseIDString = "select course_id from course WHERE course.course_num = " + courseNumber + "AND  course.department_id = " + department;
                using (SqlConnection connection = new SqlConnection(cString))
                {
                    SqlCommand c1 = new SqlCommand(getNewCourseIDString, connection);
                    connection.Open();
                    using (var reader = c1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            newCourseID = reader.GetInt32(0).ToString();
                        }
                        connection.Close();
                    }
                }
                if (!(acForm.pr1.Equals("Optional")))
                {
                    String preReqTableInsert = @"INSERT INTO [dbo].[prereq]([course_id],[pre_req_course_id]) VALUES((" + newCourseID + "," + pr1 + "";
                    //insert query
                    //update return message
                    using (SqlConnection connection = new SqlConnection(cString))
                    {
                        SqlCommand command = new SqlCommand(preReqTableInsert, connection);
                        connection.Open();
                        try
                        {
                            command.ExecuteNonQuery();
                            // returnMessage = "New Course Added Successfully";
                        }
                        catch
                        {
                            returnMessage = "ERROR: The Prereq Failed to Insert Correctly";
                        }
                        connection.Close();
                    }
                }
                if (acForm.pr2.Equals("Yes"))
                {
                    String preReqTableInsert2 = @"INSERT INTO [dbo].[prereq]([course_id],[pre_req_course_id]) VALUES((" + newCourseID + "," + pr2 + "";
                    //insert query
                    //update return message
                    using (SqlConnection connection = new SqlConnection(cString))
                    {
                        SqlCommand command = new SqlCommand(preReqTableInsert2, connection);
                        connection.Open();
                        try
                        {
                            command.ExecuteNonQuery();
                            // returnMessage = "New Course Added Successfully";
                        }
                        catch
                        {
                            returnMessage = "ERROR: The Prereq Failed to Insert Correctly";
                        }
                        connection.Close();
                    }
                }
                //check course req
                if (acForm.cr1.Equals("Yes"))
                {
                    String preReqTableInsert3 = @"INSERT INTO [dbo].[prereq]([course_id],[pre_req_course_id]) VALUES((" + newCourseID + "," + cr1 + "";
                    //insert query
                    //update return message
                    using (SqlConnection connection = new SqlConnection(cString))
                    {
                        SqlCommand command = new SqlCommand(preReqTableInsert3, connection);
                        connection.Open();
                        try
                        {
                            command.ExecuteNonQuery();
                            //returnMessage = "New Course Added Successfully";
                        }
                        catch
                        {
                            returnMessage = "ERROR: The Course Req Failed to Insert Correctly";
                        }
                        connection.Close();
                    }
                }
                if (acForm.cr2.Equals("Yes"))
                {
                    String preReqTableInsert4 = @"INSERT INTO [dbo].[prereq]([course_id],[pre_req_course_id]) VALUES((" + newCourseID + "," + cr2 + "";
                    //insert query
                    //update return message
                    using (SqlConnection connection = new SqlConnection(cString))
                    {
                        SqlCommand command = new SqlCommand(preReqTableInsert4, connection);
                        connection.Open();
                        try
                        {
                            command.ExecuteNonQuery();
                            //returnMessage = "Course Req Added";
                        }
                        catch
                        {
                            returnMessage = "ERROR: The Course Req Failed to Insert Correctly";
                        }
                        connection.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return returnMessage;

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

        public static String removeHold(String holdType, String studentID, String year, String semester)
        {
            List<Hold> holds = new List<Hold>();
            int holdTypeID = 0;
            if (holdType.Equals("Academic"))
            {
                holdTypeID = 1;
            }
            else if (holdType.Equals("Financial"))
            {
                holdTypeID = 3;
            }
            else if (holdType.Equals("Administrative"))
            {
                holdTypeID = 2;
            }
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String insertString = "DELETE FROM hold WHERE student_id = " + studentID + "AND hold_type_id = " + holdTypeID;
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
                    result = "ERROR: Could not remove hold";
                }
                connection.Close();

                return result;
            }

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
                        studentsMajors.Add(new Major(reader.GetString(1), reader.GetInt32(0).ToString()));
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
            String getClassesTakenString = @"SELECT DISTINCT s.course_id,sh.grade
                                            FROM student_semester_history sh
                                            INNER JOIN section s ON s.section_id = sh.section_id
                                            INNER JOIN course c ON c.course_id = s.course_id
                                            inner join department d on d.department_id = c.department_id
                                            INNER join major m on m.department_id = d.department_id
                                            where sh.student_id = 1";
            //SQL to get out of major classes taken
            String getOutOfMajorClassesTaken = "SELECT [course_id], [course_number], [course_name], [prereqs], [course_credits], [grade] FROM out_of_major_reqs_view WHERE student_id = " + studentID + " AND  course_id not in(select course_id from major_requirements where major_id = " + majorID + ")";
            //SQL to get classes currently being taken
            String getClassesInProgressString = "SELECT s.course_id FROM enrollment e INNER JOIN section s ON s.section_id = e.section_id WHERE s.semster = 'spring' AND s.[year] = '2019' AND e.student_id = 1";
            //SQL to get prereqs for major courses
            String getPrereqsString = "SELECT [course_id] ,[prereq_course_id] ,[prereq_course_name] FROM [HarborViewUniversity].[dbo].[prereq_view] WHERE [major_id] = " + majorID;
            //Connection String
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            //List of major reqs
            List<DegreeAuditMajorReqs> majorReqs = new List<DegreeAuditMajorReqs>();
            //List of courses taken
            Hashtable coursesTaken = new Hashtable();
            //Prerequisites for courses
            DataTable prereqTable = new DataTable();
            //List of courses currently being taken
            List<string> inProgress = new List<string>();
            //Lists for major electives
            List<DegreeAuditElectives> majorElectives = new List<DegreeAuditElectives>();
            //List of out of major classes taken
            List<DegreeAuditOutOfMajorReqs> outOfMajorReqs = new List<DegreeAuditOutOfMajorReqs>();
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
<<<<<<< HEAD

                //Get prereqs for major reqs
                SqlCommand cmd = new SqlCommand(getPrereqsString, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(prereqTable);
                da.Dispose();

                //Major Electives
                //get list of major electives
                SqlCommand command3 = new SqlCommand(getMajorElectivesString, connection);
                using (var reader = command3.ExecuteReader())
=======
                String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
                String insertString = "DELETE FROM hold WHERE student_id = " + studentID + " AND hold_type_id = " + holdTypeID;
                String result = "";
                using (SqlConnection connection = new SqlConnection(cString))
>>>>>>> 9797b08... Updated Student Information Works
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
                        try
                        {
                            outOfMajorReqs.Add(new DegreeAuditOutOfMajorReqs(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetByte(4), reader.GetString(5)));
                        }
                        catch
                        {
                            outOfMajorReqs.Add(new DegreeAuditOutOfMajorReqs(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetByte(4), ""));
                        }
                    }
                }

                connection.Close();
            }
            //Major reqs data processing
            foreach (var mr in majorReqs)
            {
                //if taken course is passed, mark as complete
                if (coursesTaken.Contains(mr.courseID))
                {
                    if (GradeList.isPassing((string)coursesTaken[mr.courseID]))
                    {
                        mr.courseStatus = "&#x2611";
                    }
                }
                //if course is in progress, mark as in progress
                if (inProgress.Contains(mr.courseID))
                {
                    mr.courseStatus = "&#x2610";
                }
                //sort prereqs by taken or not taken
                foreach (DataRow dr in prereqTable.Select("course_id = '" + mr.courseID + "'"))
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
            }
            //major electives data processing
            foreach (var el in majorElectives)
            {
                //if taken course is passed, mark as complete
                if (coursesTaken.Contains(el.courseID))
                {
                    if (GradeList.isPassing((string)coursesTaken[el.courseID]))
                    {
                        el.courseStatus = "&#x2611";
                        el.grade = (string)coursesTaken[el.courseID];
                    }
                }

                //if course is in progress, mark as in progreess
                if (inProgress.Contains(el.courseID))
                {
                    el.courseStatus = "&#x2610";
                }

                //sort and add prereqs
                foreach (DataRow dr in prereqTable.Select("course_id = '" + el.courseID + "'"))
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

<<<<<<< HEAD
            return new DegreeAuditData(majorReqs, majorElectives, outOfMajorReqs);
        }
=======
        public static String UpdateStudentInformation(StudentInfo s, String studentID)
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "UPDATE user_info SET street_name = '" + s.streetName + "', city = '" + s.city + "', [state] = '" + s.state + "', zip = " + s.zip + " WHERE user_id = " + studentID;
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            return "";
        }

        public static List<StudentInfo> ViewStudentInformation(String streetName, String city, String state, String zip, String userID)
        {
            List<StudentInfo> info = new List<StudentInfo>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String insertString = "SELECT street_name, city, [state], zip FROM [dbo].[user_info] WHERE [user_id] = " + userID;
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(insertString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        info.Add(new StudentInfo(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3).ToString()));
                    }
                }
                connection.Close();

                return info;
            }
        }

>>>>>>> 9797b08... Updated Student Information Works
    }

}