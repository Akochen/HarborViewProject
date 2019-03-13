using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class Catalog
    {
        public List<Course> courses { get; set; }
        public List<Major> majors { get; set; }
        public List<Minor> minors { get; set; }
        public Catalog(List<Course> courses, List<Major> majors, List<Minor> minors)
        {
            this.courses = courses;
            this.majors = majors;
            this.minors = minors;
        }
    }
}