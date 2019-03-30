using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class SemesterDataHelper
    {
        public static String getSemesterSeason()
        {
            DateTime now = DateTime.Now;
            if (now.Month < 5)
            {
                return "Spring";
            }
            else if (now.Month < 9)
            {
                return "Summer";
            }
            else
            {
                return "Fall";
            }
        }

        public static String getSemesterYear()
        {
            return DateTime.Now.Year.ToString();
        }

        public static String getNextSemesterSeason()
        {
            if (getSemesterSeason().Equals("Fall"))
            {
                return "Spring";
            } else
            {
                return "Fall";
            }
        }

        public static String getNextSemesterYear()
        {
            if (getNextSemesterSeason().Equals("Fall"))
            {
                return getSemesterYear() + 1;
            }
            else
            {
                return getSemesterYear();
            }
        }
    }
}