using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class AddSectionForm
    {
        public List<Location> buildings { set; get; }
        public List<Location> locations { set; get; }
        public List<Course> courses { set; get; }
        public List<String> startTimes;
        public List<String> semsters;
        public List<String> years;

        public AddSectionForm(List<Location> buildings, List<Location> locations, List<Course> courses, List<string> semsters, List<string> years)
        {
            this.buildings = buildings;
            this.locations = locations;
            this.courses = courses;
            this.semsters = semsters;
            this.years = years;
        }
    }
}