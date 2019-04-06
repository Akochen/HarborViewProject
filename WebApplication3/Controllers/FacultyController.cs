using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class FacultyController : Controller
    {
        // GET: Faculty
        public ActionResult FacultyHome()
        {
            return View();
        }

        public ActionResult FacultySchedule(String UserID, String semester, String year)
        {
            return View(WebApplication3.Models.FacultyDbConnectionClass.viewSchedule(UserID, semester, year));
        }

        public ActionResult ViewEnrollees(String sectionID)
        {
            return View(WebApplication3.Models.FacultyDbConnectionClass.viewEnrolleeList(sectionID));
        }

        public ActionResult AssignGradeEnrollees(String sectionID, String semester, String year,String firstName, String lastName, String studentID, String courseName, String credits)
        {
            return View(new WebApplication3.HelperClasses.StudentEnrollment(firstName, lastName, sectionID, courseName, year, semester, studentID,credits));
        }

        public ActionResult InsertGrade(String studentID, String sectionID, String courseName, String semester, String year, String grade, String credits)
        {
            return View((object)WebApplication3.Models.FacultyDbConnectionClass.insertGrade(studentID, sectionID, courseName, semester, year, grade,credits));
        }

        public ActionResult ViewStudentHoldSelector()
        {
            return View(WebApplication3.Models.FacultyDbConnectionClass.viewStudentHoldSemesterHelper());
        }
    }
}