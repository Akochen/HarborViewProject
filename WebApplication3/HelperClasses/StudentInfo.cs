using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class StudentInfo
    {
        public String studentID { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String email { get; set; }
        public String dob { get; set; }
        public String phoneNumber { get; set; }
        public String street { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public String zip { get; set; }
        public String major { get; set; }
        public String minor { get; set; }

        public StudentInfo()
        {
        }

        public StudentInfo(string studentID, string firstName, string lastName, string email, string dob, string phoneNumber, string street, string city, string state, string zip, string major, string minor)
        {
            this.studentID = studentID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.dob = dob;
            this.phoneNumber = phoneNumber;
            this.street = street;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.major = major;
            this.minor = minor;
        }
    }
}