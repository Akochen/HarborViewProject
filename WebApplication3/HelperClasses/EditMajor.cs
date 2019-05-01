using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class EditMajor
    {
        public List<Course> courses { set; get; }
        public String major { get; set; }
        public String courseID { get; set; }

        public EditMajor(List<Course> courses, string major)
        {
            this.courses = courses;
            this.major = major;
        }

        public EditMajor(string major, string courseID)
        {
            this.major = major;
            this.courseID = courseID;
        }
    }
}