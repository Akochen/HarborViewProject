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
        public String electives { get; set; }
        public String degreeLevel { get; set; }
        public String majorID { get; set; }

        public Major(string name, string requirements, string electives, string degreeLevel)
        {
            this.name = name;
            this.requirements = requirements;
            this.electives = electives;
            this.degreeLevel = degreeLevel;
        }

        public Major(String majorID, String name)
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