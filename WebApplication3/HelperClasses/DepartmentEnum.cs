using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public enum DepartmentEnum
    {
        Default,
        [Description("Computer Science")] ComputerScience
    }
}