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
        public int majorID { get; set; }
        public string majorName { get; set; }

        public Course(string id, string prereq, string prereqName)
        {
            this.id = id;
            this.prereq = prereq;
            this.prereqName = prereqName;
        }

        public Course(string courseName, string id)
        {
            this.courseName = courseName;
            this.id = id;
        }

        public Course(string id,string courseName, int majorID,string majorName)
        {
            this.courseName = courseName;
            this.id = id;
            this.majorID = majorID;
            this.majorName = majorName;
        }
        public Course(string courseName, string id, int majorID)
        {
            this.courseName = courseName;
            this.id = id;
            this.majorID = majorID;
            this.majorName = majorName;
        }

    }
}