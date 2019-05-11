using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class SemesterList
    {
        public List<String> season;
        public List<String> year;

        public SemesterList(List<string> season, List<string> year)
        {
            this.season = season;
            this.year = year;
        }
    }
}