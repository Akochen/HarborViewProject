using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class FacultySemester
    {
        public String facultyID;
        public String sectionID;
        public String courseID;
        public String courseName;
        public String semester;
        public String year;
        public String startTime;
        public String endTime;
        public String days;
        public String grade;
        public String credits;

        public String firstName;
        public String lastName;
        public String email;
        public String phone_number;
        public String dob;
        public String studentID;


        public FacultySemester()
        {
        }

        public FacultySemester(string facultyID, string sectionID, string courseID, string courseName, string semester, string year, string startTime, string endTime, string days)
        {
            this.facultyID = facultyID;
            this.sectionID = sectionID;
            this.courseID = courseID;
            this.courseName = courseName;
            this.semester = semester;
            this.year = year;
            this.startTime = startTime;
            this.endTime = endTime;
            this.days = days;
        }

        public FacultySemester(string firstName, string lastName, string email, string phone_number, string dob, string grade, string credits,string studentID)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phone_number = phone_number;
            this.dob = dob;
            this.grade = grade;
            this.credits = credits;
            this.studentID = studentID;
        }
    }
}
