using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class AddCourse
    {
        public List<Department> departments { set; get; }
        public List<Major> majors { set; get; }
        public List<Minor> minors { set; get; }

        public AddCourse(List<Department> departments, List<Major> majors, List<Minor> minors)
        {
            this.departments = departments;
            this.majors = majors;
            this.minors = minors;
        }

        public int isElective(String input)
        {
            var result = 0;
            if (input == "Yes")
            {
                result = 1;
            }
            else result = 0;
            return result;
        }
        public int isGradCourse(String input)
        {
            var result = 0;
            if (input == "Yes")
            {
                result = 1;
            }
            else result = 0;
            return result;
        }
    }
}