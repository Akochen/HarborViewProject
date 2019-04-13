using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class Department
    {
        public String departmentID;
        public String departmentShortName;
        public String departmentFullName;

        public Department(string departmentID,string departmentFullName)
        {
            this.departmentID = departmentID;
            this.departmentFullName = departmentFullName;
        }
    }
}