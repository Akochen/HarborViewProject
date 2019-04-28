using System;
using System.Collections.Generic;
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
            String deptString = "SELECT [department_full_name],department_id FROM [HarborViewUniversity].[dbo].[department] order by department_full_name";
            String courseString = "select course_name, course_id from course order by course_name";
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
                        departments.Add(new Department(reader.GetString(0),reader.GetInt32(1).ToString()));
                    }
                    connection.Close();
                }
                SqlCommand c2 = new SqlCommand(courseString, connection);
                connection.Open();
                using (var reader = c2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course(reader.GetString(0),reader.GetInt32(1).ToString()));
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
            String getCourseNumbersString = @"SELECT c.course_num FROM course c INNER JOIN department d ON d.department_id = c.department_id WHERE d.department_id = " + acForm.department;
            //String getDeptId = " SELECT department_id FROM department WHERE department_full_name = " + acForm.department;
            //String getPr1Id = "select course_id from course where course_name = "+acForm.pr1+" AND department_id = "+ getDeptId;
            //String getPr2Id = "select course_id from course where course_name = " + acForm.pr2 + " AND department_id = " + getDeptId;
            //String getCr1Id = "select course_id from course where course_name = " + acForm.cr1 + " AND department_id = " + getDeptId;
            //String getCr2Id = "select course_id from course where course_name = " + acForm.cr2 + " AND department_id = " + getDeptId;
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

                }//check if duplicate course number exists
                for (int i = 0; i < currentCourseNumbers.Count; i++)
                {
                    if (acForm.courseNumber.Equals(currentCourseNumbers.ElementAt(i)))
                    {
                        return "This course number already exists in this department. Please select another course number";
                    }
                }
                //check elective validation
                if (acForm.isElective.Equals("Yes")) {
                    acForm.isElective ="1";
                }
                else acForm.isElective = "0";


                //check graduate course validation
                if (acForm.isGrad.Equals("Yes")) {
                    acForm.isGrad ="1";
                }
                else acForm.isGrad ="0";

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
                var department = Int32.Parse(acForm.department);
                var courseNumber = Int32.Parse(acForm.courseNumber);
                var courseName = acForm.courseName;
                var description = acForm.description;
                var credits = Int32.Parse(acForm.credits);
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
                if (!(acForm.pr1.Equals("default")))
                {
                    String preReqTableInsert = @"INSERT INTO [dbo].[prereq]([course_id],[pre_req_course_id]) VALUES(" + newCourseID + "," + pr1 + ")";
                    //insert query
                    //update return message
                    using (SqlConnection connection = new SqlConnection(cString))
                    {
                        SqlCommand command = new SqlCommand(preReqTableInsert, connection);
                        connection.Open();
                        try
                        {
                            command.ExecuteNonQuery();
                            String pr1Msg = "You have successfully added a prereq";
                            returnMessage = returnMessage + "\\n" +pr1Msg;
                        }
                        catch
                        {
                            returnMessage = returnMessage + "\\n"+"ERROR: The Prereq Failed to Insert Correctly";
                        }
                        connection.Close();
                    }
                }
                if (!(acForm.pr2.Equals("default")))
                {
                    String preReqTableInsert2 = @"INSERT INTO [dbo].[prereq]([course_id],[pre_req_course_id]) VALUES(" + newCourseID + "," + pr2 + ")";
                    //insert query
                    //update return message
                    using (SqlConnection connection = new SqlConnection(cString))
                    {
                        SqlCommand command = new SqlCommand(preReqTableInsert2, connection);
                        connection.Open();
                        try
                        {
                            command.ExecuteNonQuery();
                            String pr2Msg = "You have successfully added a prereq";
                            returnMessage = returnMessage + "\\n" + pr2Msg;
                        }
                        catch
                        {
                            returnMessage = returnMessage + "\\n"+"ERROR: The Prereq Failed to Insert Correctly";
                        }
                        connection.Close();
                    }
                }
                //check course req
                if (!(acForm.cr1.Equals("default")))
                {
                    String preReqTableInsert3 = @"INSERT INTO [dbo].[prereq]([course_id],[pre_req_course_id]) VALUES(" + newCourseID + "," + cr1 + ")";
                    //insert query
                    //update return message
                    using (SqlConnection connection = new SqlConnection(cString))
                    {
                        SqlCommand command = new SqlCommand(preReqTableInsert3, connection);
                        connection.Open();
                        try
                        {
                            command.ExecuteNonQuery();
                            String cr1Msg = "You have successfully added a course req";
                            returnMessage = returnMessage + "\\n" + cr1Msg;
                        }
                        catch
                        {
                            returnMessage = returnMessage +"\\n"+ "ERROR: The Course Req Failed to Insert Correctly";
                        }
                        connection.Close();
                    }
                }
                if (!(acForm.cr2.Equals("default")))
                {
                    String preReqTableInsert4 = @"INSERT INTO [dbo].[prereq]([course_id],[pre_req_course_id]) VALUES(" + newCourseID + "," + cr2 + ")";
                    //insert query
                    //update return message
                    using (SqlConnection connection = new SqlConnection(cString))
                    {
                        SqlCommand command = new SqlCommand(preReqTableInsert4, connection);
                        connection.Open();
                        try
                        {
                            command.ExecuteNonQuery();
                            String cr2Msg = "You have successfully added a prereq";
                            returnMessage = returnMessage + "\\n" + cr2Msg;
                        }
                        catch
                        {
                            returnMessage = returnMessage + "\\n"+"ERROR: The Course Req Failed to Insert Correctly";
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
        public static List<Major> editMajorSelectorHelper()
        {
            List<Major> majors = new List<Major>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT major_id, [major_name] FROM [HarborViewUniversity].[dbo].[major]";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        majors.Add(new Major(reader.GetInt32(0).ToString(), reader.GetString(1)));
                    }
                }
                connection.Close();
            }

            return majors;
        }

        public static List<Course> editMajor(String major)
        {
            List<Course> courses = new List<Course>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = @" SELECT c.course_name, c.course_id FROM course c
                                    INNER JOIN department d ON d.department_id = c.department_id
                                    INNER JOIN major m ON m.department_id = d.department_id
                                    WHERE m.major_id = 1";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course(reader.GetString(0), reader.GetInt32(1).ToString()));
                    }
                }
                connection.Close();
            }

            return courses;
        }

        public static String editMajorResults(String courseID, String courseAttr)
        {
            List<Course> courses = new List<Course>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = @" SELECT c.course_name, c.course_id FROM course c
                                    INNER JOIN department d ON d.department_id = c.department_id
                                    INNER JOIN major m ON m.department_id = d.department_id
                                    WHERE m.major_id = 1";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course(reader.GetString(0), reader.GetInt32(1).ToString()));
                    }
                }
                connection.Close();
            }

            return null;
        }

        public static AddSectionForm addSectionForm()
        {
            String roomAndBuilding = @"SELECT b.building_id,b.building_full_name,r.room_id,room_number FROM building b INNER JOIN room r ON r.building_id = b.building_id";
            String courseString = "select course_name,course_id from course order by course_name";
            String buildingString = "SELECT DISTINCT building_id,building_full_name FROM building";
            List<Location> buildings = new List<Location>();
            List<Location> locations = new List<Location>();
            List<Course> courses = new List<Course>();
            AddSectionForm helper = new AddSectionForm(buildings,locations, courses);
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand c1 = new SqlCommand(roomAndBuilding, connection);
                connection.Open();
                using (var reader = c1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        locations.Add(new Location(reader.GetInt32(0).ToString(),reader.GetString(1),reader.GetInt32(2).ToString(),reader.GetInt32(3).ToString()));
                    }
                }
                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand c2 = new SqlCommand(courseString, connection);
                connection.Open();
                using (var reader = c2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course(reader.GetString(0),reader.GetInt32(1).ToString()));
                    }
                }
                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand c3 = new SqlCommand(buildingString, connection);
                connection.Open();
                using (var reader = c3.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        buildings.Add(new Location(reader.GetInt32(0).ToString(), reader.GetString(1)));
                    }
                }
                connection.Close();
            }

            return helper;
        }
    }
    
}