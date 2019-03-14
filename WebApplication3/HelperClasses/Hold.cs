using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class Hold
    {
        public String holdType;
        public String semester;
        public String year;

        public Hold(string holdType, string semester, string year)
        {
            this.holdType = holdType;
            this.semester = semester;
            this.year = year;
        }
    }
}