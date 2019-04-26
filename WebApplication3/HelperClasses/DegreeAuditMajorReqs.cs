using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class DegreeAuditMajorReqs
    {
        public String courseID;
        public String courseNum;
        public String courseName;
        public String prereqsToTake { get; set; }
        public String prereqsTaken { get; set; }
        public String courseStatus { get; set; }
        public int credits;
        public String grade;

        public DegreeAuditMajorReqs(string courseID, string courseNum, string courseName, int credits)
        {
            courseStatus = "&#x2612";
            this.courseID = courseID;
            this.courseNum = courseNum;
            this.courseName = courseName;
            this.credits = credits;
            prereqsTaken = "";
            prereqsToTake = "";
        }
    }
}