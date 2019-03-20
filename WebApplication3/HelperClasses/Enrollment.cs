using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class Enrollment
    {
        public String courseID;
        public String name;
        public String instructor;
        public String days;
        public String startTime;
        public String endTime;
        public String semester;
        public String year;
        public String type;
        public String building;
        public String room;

        public Enrollment(string courseID, string name, string instructor, string days, string startTime, string endTime, string semester, string year, string type, string building, string room)
        {
            this.courseID = courseID;
            this.name = name;
            this.instructor = instructor;
            this.days = days;
            this.startTime = startTime;
            this.endTime = endTime;
            this.semester = semester;
            this.year = year;
            this.type = type;
            this.building = building;
            this.room = room;
        }
    }
}