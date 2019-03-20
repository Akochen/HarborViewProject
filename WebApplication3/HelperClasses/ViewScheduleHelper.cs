using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class ViewScheduleHelper
    {
        public IEnumerable<String> years { get; set; }
        public IEnumerable<String> semesters { get; set; }

        public ViewScheduleHelper(IEnumerable<string> years, IEnumerable<string> semesters)
        {
            this.years = years;
            this.semesters = semesters;
        }
    }
}