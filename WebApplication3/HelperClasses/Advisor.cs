using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class Advisor
    {
        public String name;
        public String department;
        public String email;
        public String id;

        public Advisor(string name, string department, string email)
        {
            this.name = name;
            this.department = department;
            this.email = email;
        }

        public Advisor(string name, string id)
        {
            this.name = name;
            this.id = id;
        }
    }
}