using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class Course
    {
        public String shortName { get; set; }
        public String fullName { get; set; }
        public String prereqs { get; set; }
        public String description { get; set; }
        public int credits { get; set; }
        public String courseNumber { get; set; }

        public Course(String shortName, String fullName, String prereqs, String description, int credits)
        {
            this.shortName = shortName;
            this.fullName = fullName;
            this.prereqs = prereqs;
            this.description = description;
            this.credits = credits;
        }

        public Course(string courseNumber)
        {
            this.courseNumber = courseNumber;
        }
    }
}