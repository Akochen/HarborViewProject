using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class SectionSearchHelper
    {
        public IEnumerable<String> departments { get; set; }
        public IEnumerable<String> times { get; set; }
        public IEnumerable<String> days { get; set; }
        public IEnumerable<String> years { get; set; }
        public IEnumerable<String> semesters { get; set; }

        public SectionSearchHelper(IEnumerable<string> departments, IEnumerable<string> times, IEnumerable<string> days, IEnumerable<string> years, IEnumerable<string> semesters)
        {
            this.departments = departments;
            this.times = times;
            this.days = days;
            this.years = years;
            this.semesters = semesters;
        }
    }
}