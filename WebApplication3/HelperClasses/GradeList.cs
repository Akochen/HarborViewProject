using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class GradeList
    {
        // public List<String> grades { get; set; }

        //public GradeList(List<string> grades)
        //{
        //    this.grades = { ''}
        //}

        public static List<String> getGradeList()
        {
            List<String> gradeList = new List<string>();

            gradeList.Add("A");
            gradeList.Add("A-");
            gradeList.Add("B+");
            gradeList.Add("B");
            gradeList.Add("B-");
            gradeList.Add("C+");
            gradeList.Add("C");
            gradeList.Add("C-");
            gradeList.Add("D+");
            gradeList.Add("D");
            gradeList.Add("D-");
            gradeList.Add("F");
            return gradeList;
        }

        public static bool isPassing(String grade)
        {
            string g = grade.ToLower().Replace(" ", string.Empty);
            if (g == "c" || g == "c+" || g == "b-" || g == "b" || g == "b+" || g == "a-" || g == "a")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static double convertGradeToPoints(String grade)
        {
            String gpaGrade = grade.ToLower();
            if (gpaGrade.Equals("a"))
            {
                return 4;
            }
            else if (gpaGrade.Equals("a-"))
            {
                return 3.7;
            }
            else if (gpaGrade.Equals("b+"))
            {
                return 3.3;
            }
            else if (gpaGrade.Equals("b"))
            {
                return 3;
            }
            else if (gpaGrade.Equals("b-"))
            {
                return 2.7;
            }
            else if (gpaGrade.Equals("c+"))
            {
                return 2.3;
            }
            else if (gpaGrade.Equals("c"))
            {
                return 2;
            }
            else if (gpaGrade.Equals("c-"))
            {
                return 1.7;
            }
            else if (gpaGrade.Equals("d+"))
            {
                return 1.3;
            }
            else if (gpaGrade.Equals("d"))
            {
                return 1;
            }
            else if (gpaGrade.Equals("d-"))
            {
                return .7;
            }
            else
            {
                return 0;
            }
        }

        public static string convertPointsToGrade(double points)
        {
            if(points < 0)
            {
                return "N/A";
            }
            else if(points < .5)
            {
                return "F";
            }
            else if (points < .85)
            {
                return "D-";
            }
            else if (points < 1.15)
            {
                return "D";
            }
            else if (points < 1.5)
            {
                return "C-";
            }
            else if (points < 1.85)
            {
                return "C";
            }
            else if (points < 2.15)
            {
                return "C+";
            }
            else if (points < 2.5)
            {
                return "B-";
            }
            else if (points < 2.85)
            {
                return "B";
            }
            else if (points < 3.15)
            {
                return "B+";
            }
            else if (points < 3.5)
            {
                return "A-";
            }
            else
            {
                return "A";
            }
        }
    }
}