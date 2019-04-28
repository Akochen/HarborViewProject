using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class CatalogCourse
    {
        public string id;
        public String shortName { get; set; }
        public String fullName { get; set; }
        public String prereqs { get; set; }
        public String description { get; set; }
        public int credits { get; set; }
        public String courseNumber { get; set; }
        public List<Course> prereqsList;

        public CatalogCourse(String shortName, String fullName, String prereqs, String description, int credits)
        {
            this.shortName = shortName;
            this.fullName = fullName;
            this.prereqs = prereqs;
            this.description = description;
            this.credits = credits;
        }
        
        public CatalogCourse(string id, string courseName)
        {
            this.id = id;
            this.fullName = courseName;
        }

        public CatalogCourse(string id, string fullName, string description, List<Course> prereqsList) : this(id, fullName)
        {
            this.description = description;
            this.prereqsList = prereqsList;
        }
    }
}