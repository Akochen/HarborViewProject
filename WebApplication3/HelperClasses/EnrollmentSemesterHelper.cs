using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class EnrollmentSemesterHelper
    {
        public List<String> semesters;

        public EnrollmentSemesterHelper(List<string> semesters)
        {
            this.semesters = semesters;
        }
    }
}