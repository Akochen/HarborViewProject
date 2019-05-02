using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class Professor
    {
        public string id;
        public string name;

        public Professor(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}