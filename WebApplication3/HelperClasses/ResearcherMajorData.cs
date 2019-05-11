using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class ResearcherMajorData
    {
        public String majorName;
        public String studentCount;

        public ResearcherMajorData(string majorName, string studentCount)
        {
            this.majorName = majorName;
            this.studentCount = studentCount;
        }
    }
}