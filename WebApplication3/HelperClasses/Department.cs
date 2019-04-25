using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class Department
    {
        public String departmentID { get; set; }
        public String departmentShortName { get; set; }
        public String departmentFullName { get; set; }

        public Department(string departmentFullName,string departmentID)
        {
            this.departmentID = departmentID;
            this.departmentFullName = departmentFullName;
        }
        public Department(string departmentFullName)
        {
            this.departmentFullName = departmentFullName;
        }

        public Department()
        {
        }
    }
}