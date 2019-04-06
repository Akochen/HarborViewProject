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
    }
}