using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class Major
    {
        public String name { get; set; }
        public String requirements { get; set; }
        public String degreeLevel { get; set; }
        public String majorID;

        public Major(String name, String requirements, String degreeLevel)
        {
            this.requirements = requirements;
            this.name = name;
            this.degreeLevel = degreeLevel;
        }

        public Major(string name, string majorID)
        {
            this.name = name;
            this.majorID = majorID;
        }
        public Major(string name)
        {
            this.name = name;
        }
    }
}