using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class AddCourse
    {
        public List<Department> departments { set; get; }
        public List<Course> courses { set; get; }
        public List<Major> majors { set; get; }
        public List<Minor> minors { set; get; }
        public String department { set; get; }
        public String major { set; get; }
        public String minor { set; get; }
        public String courseNumber { set; get; }
        public String courseName { set; get; }
        public String credits { set; get; }
        public String isElective { set; get; }
        public String isGrad { set; get; }
        public String description { set; get; }
        public String pr1 { set; get; }
        public String pr2 { set; get; }
        public String cr1 { set; get; }
        public String cr2 { set; get; }
        public String isMajorReq { set; get; }
        public String isMinorReq { set; get; }

        //public AddCourse(List<Department> departments, List<Major> majors, List<Minor> minors)
        //{
        //    this.departments = departments;
        //    this.majors = majors;
        //    this.minors = minors;
        //}

        public AddCourse(List<Department> departments, List<Course> courses)
        {
            this.departments = departments;
            this.courses = courses;
        }

        public AddCourse(string department, string courseNumber, string courseName, string credits, string isElective, string isGrad, string description, string pr1, string pr2, string cr1, string cr2)
        {
            this.department = department;
            this.courseNumber = courseNumber;
            this.courseName = courseName;
            this.credits = credits;
            this.isElective = isElective;
            this.isGrad = isGrad;
            this.description = description;
            this.pr1 = pr1;
            this.pr2 = pr2;
            this.cr1 = cr1;
            this.cr2 = cr2;
        }

    }
}