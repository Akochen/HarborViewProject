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

        public Major(String name, String requirements, String degreeLevel)
        {
            this.name = name;
            this.requirements = requirements;
            this.degreeLevel = degreeLevel;
        }
    }
}