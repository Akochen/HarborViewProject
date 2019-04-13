using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class EnrollmentSemesterHelper
    {
        public List<String> departments { get; set; }
        public List<String> times { get; set; }
        public List<String> days { get; set; }
        public List<String> semesters;

        public EnrollmentSemesterHelper(List<string> semesters)
        {
            this.semesters = semesters;
        }

        public EnrollmentSemesterHelper(List<string> departments, List<string> times, List<string> days, List<string> semesters)
        {
            this.departments = departments;
            this.times = times;
            this.days = days;
            this.semesters = semesters;
        }
    }
}