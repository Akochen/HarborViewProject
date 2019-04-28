using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class Course
    {
        public string id { get; set; }
        public string prereq { get; set; }
        public string prereqName { get; set; }
        public string courseName { get; set; }
        public string major { get; set; }

        public Course(string id, string prereq, string prereqName)
        {
            this.id = id;
            this.prereq = prereq;
            this.prereqName = prereqName;
        }

        public Course(string courseName,string id)
        {
            this.courseName = courseName;
            this.id = id;
        }

    }
}