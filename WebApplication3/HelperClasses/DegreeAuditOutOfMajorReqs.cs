using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class DegreeAuditOutOfMajorReqs
    {
        public String courseID;
        public String courseNum;
        public String courseName;
        public String prereqs { get; set; }
        public String courseStatus { get; set; }
        public int credits;
        public String grade;

        public DegreeAuditOutOfMajorReqs(string courseID, string courseNum, string courseName, string prereqs, int credits, string grade)
        {
            this.courseID = courseID;
            this.courseNum = courseNum;
            this.courseName = courseName;
            this.prereqs = prereqs;
            this.credits = credits;
            this.grade = grade;
            this.courseStatus = "";
        }
    }
}