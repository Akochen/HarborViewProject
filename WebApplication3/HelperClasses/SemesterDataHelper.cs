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
            if (getSemesterSeason().Equals("Fall"))
            {
                return (int.Parse(getSemesterYear()) + 1).ToString();
            }
            else
            {
                return getSemesterYear();
            }
        }

        public static bool canRegisterForCurrentSemester()
        {
            //if during first week of current semester
            DateTime today = DateTime.Now;
            if (getSemesterSeason().Equals("Spring"))
            {
                if (today.Month == 1)
                {
                    if (today.Day < 7)
                    {
                        return true;
                    }
                }
            } else if (getSemesterSeason().Equals("Fall")){
                if (today.Month == 9)
                {
                    if (today.Day < 7)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool canDropForCurrentSemester()
        {
            //if during month of current semester
            DateTime today = DateTime.Now;
            if (getSemesterSeason().Equals("Spring"))
            {
                if (today.Month == 1)
                {
                    return true;
                
                }
            }
            else if (getSemesterSeason().Equals("Fall"))
            {
                if (today.Month == 9)
                {
                    return true;
                }
            }
            return false;
        }
    }
}