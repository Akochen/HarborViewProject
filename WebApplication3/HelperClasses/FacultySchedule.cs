using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class FacultySchedule
    {
        public String courseName;
        public String semester;
        public String year;
        public String startTime;
        public String endTime;
        public String days;
        public String building;
        public String room;


        public FacultySchedule()
        {
        }

        public FacultySchedule(string courseName, string semester, string year, string startTime, string endTime, string day, string building, string room)
        {
            this.courseName = courseName;
            this.semester = semester;
            this.year = year;
            this.startTime = startTime;
            this.endTime = endTime;
            this.days = day;
            this.building = building;
            this.room = room;
        }


    }
}
