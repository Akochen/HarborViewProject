using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class Course
    {
        public string id;
        public string prereq;
        public string prereqName;
        public string courseName;

        public Course(string id, string prereq, string prereqName)
        {
            this.id = id;
            this.prereq = prereq;
            this.prereqName = prereqName;
        }

        public Course(string courseName)
        {
            this.courseName = courseName;
        }
    }
}