using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class StudentTranscriptHelper
    {
        public List<Section> sections;
        public StudentInfo studentInfo; 

        public StudentTranscriptHelper(List<Section> sections, StudentInfo studentInfo)
        {
            this.sections = sections;
            this.studentInfo = studentInfo;
        }
    }
}