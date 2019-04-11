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
        public int room;
        public String type;
        public int seatCapacity;
        public int seatRemaining;
        public String day1;
        public String day2;
        public String day3;
        public String grade;

        public Section(int sectionID, string courseID, string courseName, string professorName, string days, string startTime, string endTime, string semester, string year, string building, int room, string type, int seatCapacity, int seatRemaining)
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

        /// <summary>
        /// Constructor for the Faculty Section List
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="days"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="semester"></param>
        /// <param name="year"></param>
        /// <param name="building"></param>
        /// <param name="room"></param>
        public Section(string courseName, string days, string startTime, string endTime, string semester, string year, string building, int room, int sectionID)
        {
            this.courseName = courseName;
            this.days = days;
            this.startTime = startTime;
            this.endTime = endTime;
            this.semester = semester;
            this.year = year;
            this.building = building;
            this.room = room;
            this.sectionID = sectionID;
        }

        public Section(string startTime, string day1, string day2, string day3)
        {
            this.startTime = startTime;
            this.day1 = day1;
            this.day2 = day2;
            this.day3 = day3;
        }

        /// <summary>
        /// Constructor for searching for a section to remove from a student's enrollment list.
        /// This method is only for displaying the options to remove and is not intended to be for
        /// the removal of the section enrollment itself.
        /// </summary>
        /// <param name="sectionID"></param>
        /// <param name="courseID"></param>
        /// <param name="courseName"></param>
        /// <param name="professorName"></param>
        /// <param name="days"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="semester"></param>
        /// <param name="year"></param>
        
        public Section(int sectionID, string courseID, string courseName, string professorName, string days, string startTime, string endTime, string semester, string year)
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
        }

        /// <summary>
        /// Constructor for the assign final grade use case for a faculty member.
        /// </summary>
        /// <param name="sectionID"></param>
        /// <param name="courseID"></param>
        /// <param name="courseName"></param>
        /// <param name="days"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="semester"></param>
        /// <param name="year"></param>
        public Section(int sectionID, string courseID, string courseName, string days, string startTime, string endTime, string semester, string year)
        {
            this.sectionID = sectionID;
            this.courseID = courseID;
            this.courseName = courseName;
            this.days = days;
            this.startTime = startTime;
            this.endTime = endTime;
            this.semester = semester;
            this.year = year;
        }

        public Section(int sectionID, string courseID, string courseName, string professorName, string days, string startTime, string endTime, string semester, string year, string type, string building, int room, string grade)
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
            this.type = type;
            this.building = building;
            this.room = room;
            this.grade = grade;
        }
    }
}