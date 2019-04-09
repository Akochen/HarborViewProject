using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class Advisee
    {
        public String studentID;
        public String firstName;
        public String lastName;
        public String phone;
        public String dob;
        public String street;
        public String city;
        public String state;
        public String zip;

        public Advisee(string studentID, string firstName, string lastName, string phone, string dob, string street, string city, string state, string zip)
        {
            this.studentID = studentID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phone = phone;
            this.dob = dob;
            this.street = street;
            this.city = city;
            this.state = state;
            this.zip = zip;
        }
    }
}