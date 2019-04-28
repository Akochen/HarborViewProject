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

        public AddSectionForm(List<Location> buildings,List<Location> locations, List<Course> courses)
        {
            this.buildings = buildings;
            this.locations = locations;
            this.courses = courses;
        }
    }
}