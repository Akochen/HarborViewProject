﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class Minor
    {
        public String name { get; set; }
        public String requirements { get; set; }
        public int minorID { get; set; }

        public Minor(String name, String requirements)
        {
            this.name = name;
            this.requirements = requirements;
        }

        public Minor(String name)
        {
            this.name = name;
        }

        public Minor(int minorID, String name)
        {
            this.minorID = minorID;
            this.name = name;
        }
    }
}