using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class StudentEnrollment
    {

        public String firstName { get; set; }
        public String lastName { get; set; }
        public String email { get; set; }
        public String phoneNumber { get; set; }
        public String dob { get; set; }
        public String street { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public String zip { get; set; }
        public String sectionID { get; set; }
        public String courseName { get; set; }
        public String year { get; set; }
        public String semester { get; set; }
        public String studentID { get; set; }
        public String credits { get; set; }
        public String grade { get; set; }

        public StudentEnrollment(string studentID, string firstName, string lastName, string email,
            string phoneNumber, string dob, string sectionID, string courseName, string year, string semester, string credits)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.dob = dob;
            this.sectionID = sectionID;
            this.courseName = courseName;
            this.year = year;
            this.semester = semester;
            this.studentID = studentID;
            this.credits = credits;
        }

        public StudentEnrollment(string firstName, string lastName, string sectionID, string courseName, string year, string semester, string studentID,string credits)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.sectionID = sectionID;
            this.courseName = courseName;
            this.year = year;
            this.semester = semester;
            this.studentID = studentID;
            this.credits = credits;
        }

    }

}