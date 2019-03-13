using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class Section
    {
        public int sectionID;
        public String courseID;
        public String courseName;
        public String professorName;
        public String days;
        public String startTime;
        public String endTime;
        public String semester;
        public String year;
        public String building;
        public String room;
        public String type;
        public int seatCapacity;
        public int seatRemaining;

        public Section(int sectionID, string courseID, string courseName, string professorName, string days, string startTime, string endTime, string semester, string year, string building, string room, string type, int seatCapacity, int seatRemaining)
        {
            this.sectionID = sectionID;
            this.courseID = courseID;
            this.courseName = courseName;
            this.professorName = professorName;
            this.days = days;
            this.startTime = startTime;
            this.endTime = endTime;
            this.semester = semester;
            this.year = year;
            this.building = building;
            this.room = room;
            this.type = type;
            this.seatCapacity = seatCapacity;
            this.seatRemaining = seatRemaining;
        }
    }
}