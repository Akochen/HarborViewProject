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
                            ,reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetInt32(10).ToString()));
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

        public static List<Advisee> viewAdviseeList(String userID)
        {
            List<Advisee> adviseeList = new List<Advisee>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT first_name,last_name,phone_number,dob,street_name,city,state,zip,student_id FROM [HarborViewUniversity].[dbo].[advisor_view] av inner join [user] u on u.user_id = av.student_id where av.faculty_id = " + userID +"";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        adviseeList.Add(new Advisee(reader.GetInt32(8).ToString(),reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3).ToShortDateString(), reader.GetString(4),
                            reader.GetString(5), reader.GetString(6),reader.GetInt32(7).ToString()));
                    }
                }
                connection.Close();
            }

            return adviseeList;
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

        public static EnrollmentSemesterHelper createViewFacultySemesterHelper()
        {
            List<String> semesters = new List<string>();
            semesters.Add(SemesterDataHelper.getSemesterSeason() + " " + SemesterDataHelper.getSemesterYear());
            semesters.Add(SemesterDataHelper.getNextSemesterSeason() + " " + SemesterDataHelper.getNextSemesterYear());

            return new EnrollmentSemesterHelper(semesters);
        }

        public static ViewScheduleHelper createFacultySemesterHistoryScheduleViewHelper()
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String yearString = "SELECT DISTINCT [year] FROM [HarborViewUniversity].[dbo].[faculty_semester_history]";
            String semesterString = "SELECT DISTINCT [semster] FROM [HarborViewUniversity].[dbo].[faculty_semester_history]";
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

        public static List<FacultySemester> viewFacultySemesterHistory(String userID, String year, String semester)
        {
            List<FacultySemester> semesterData = new List<FacultySemester>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = @"SELECT DISTINCT
	   [faculty_schedule].faculty_id
	  ,[section].section_id
	  ,CONCAT(d.department_short_name,course.course_num) as 'course_id'
 	  ,[course].course_name
	  ,[section].semster
	  ,section.[year]
	  ,[time_slot].start_time
	  ,[time_slot].end_time
	  ,CONCAT([time_slot].[day_1], [time_slot].[day_2], [time_slot].[day_3]) AS 'days'
  FROM [HarborViewUniversity].[dbo].[faculty_schedule]
  JOIN [section] ON [faculty_schedule].[section_id] = section.[section_id]
  JOIN [course] ON [course].course_id = section.course_id
  JOIN [time_slot] ON [section].time_slot_id = [time_slot].period
  JOIN department d on d.department_id = course.department_id
  JOIN student_semester_history sh on sh.section_id = [section].section_id
  JOIN [user] u on u.user_id = sh.student_id" +
                 " WHERE section.[faculty_id] = " + userID + " AND section.[semster] = '" + semester + "' AND section.[year] = '" + year + "'";
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        semesterData.Add(new FacultySemester(reader.GetInt32(0).ToString(), reader.GetInt32(1).ToString(), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6)
                            , reader.GetString(7), reader.GetString(8)));
                    }
                }
                connection.Close();
            }

            return semesterData;
        }

        public static List<FacultySemester> viewFacultySemesterEnrolleeList(String sectionID)
        {
            List<FacultySemester> studentData = new List<FacultySemester>();
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String queryString = "SELECT u.first_name,u.last_name,u.email,u.phone_number,u.dob,sh.grade,sh.credits,sh.student_id FROM [HarborViewUniversity].[dbo].[faculty_schedule]JOIN [section] ON [faculty_schedule].[section_id] = [section].[section_id]JOIN [course] ON [course].course_id = section.course_id JOIN [time_slot] ON [section].time_slot_id = [time_slot].period JOIN department d on d.department_id = course.department_id JOIN student_semester_history sh on sh.section_id = [section].section_id JOIN [user] u on u.user_id = sh.student_id WHERE section.[section_id] = " + sectionID ;
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        studentData.Add(new FacultySemester(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4).ToShortDateString(), reader.GetString(5), reader.GetByte(6).ToString(), reader.GetInt32(7).ToString()));
                    }
                    connection.Close();
                }
                return studentData;
            }
        }

        public static EnrollmentSemesterHelper createViewScheduleHelper()
        {
            String cString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
            String deptString = "SELECT [department_full_name] FROM [HarborViewUniversity].[dbo].[department] WHERE department_id != 11";
            String dayString = "SELECT DISTINCT [days] FROM [HarborViewUniversity].[dbo].[section_view]";
            String timeString = "SELECT DISTINCT FORMAT(CAST([start_time] AS datetime), 'h:mm tt') AS start_time, CAST([start_time] AS datetime) AS order_time FROM [HarborViewUniversity].[dbo].[time_slot] ORDER BY order_time";
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

            return new DegreeAuditData(majorReqs, majorElectives, outOfMajorReqs);
        }
    }
}
