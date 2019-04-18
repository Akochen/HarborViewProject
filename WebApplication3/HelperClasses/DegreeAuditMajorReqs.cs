using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class DegreeAuditMajorReqs
    {
        public String courseID;
        public String courseName;
        public String prereqsToTake;
        public String isComplete;

        public DegreeAuditMajorReqs(string courseID, string courseName, string prereqsToTake, string isComplete)
        {
            this.courseID = courseID;
            this.courseName = courseName;
            this.prereqsToTake = prereqsToTake;
            this.isComplete = isComplete;
        }
    }
}