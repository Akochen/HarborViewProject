using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class EditMinor
    {
        public List<Course> courses { set; get; }
        public String minor { get; set; }
        public String courseID { get; set; }

        public EditMinor(List<Course> courses, string minor)
        {
            this.courses = courses;
            this.minor = minor;
        }

        public EditMinor(string minor, string courseID)
        {
            this.minor = minor;
            this.courseID = courseID;
        }
    }
}