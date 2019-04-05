using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class GradeList
    {
        public List<String> grades { get; set; }

        public GradeList(List<string> grades)
        {
            this.grades = grades;
        }
    }
}