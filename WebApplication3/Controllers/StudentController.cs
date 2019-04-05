using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.HelperClasses;

namespace WebApplication3.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult StudentHome()
        {
            return View();
        }
        public ActionResult SearchSection()
        {
            return View(WebApplication3.Models.StudentDbConnectionClass.createSectionSearchHelper());
        }

        public ActionResult SearchSectionResults()
        {
            return PartialView(new List<Section>());
        }
        [HttpPost]
        public ActionResult SearchSectionResults(String searchYear, String searchSemester, String instructor, String days, String time, String courseID, String courseName, String department)
        {
            return PartialView(WebApplication3.Models.StudentDbConnectionClass.searchSections(searchYear, searchSemester, instructor, days, time, courseID, courseName, department));
        }

        public ActionResult ViewHolds(String userID)
        {
            return View(WebApplication3.Models.StudentDbConnectionClass.viewHolds(userID));
        }

        public ActionResult ViewAdvisor(String userID)
        {
            return View(WebApplication3.Models.StudentDbConnectionClass.viewAdvisor(userID));
        }

        public ActionResult ViewSchedule()
        {
            return View(WebApplication3.Models.StudentDbConnectionClass.createScheduleViewHelper());
        }
        
        public ActionResult ViewScheduleResults()
        {
            return View(new List<Enrollment>());
        }
        [HttpPost]
        public ActionResult ViewScheduleResults(String userID, String year, String semester)
        {
            return View(WebApplication3.Models.StudentDbConnectionClass.viewSchedule(userID, year, semester));
        }

        public ActionResult ViewCurrentSchedule(String userID, String year, String semester)
        {
            return View(WebApplication3.Models.StudentDbConnectionClass.viewSchedule(userID, year, semester));
        }

        public ActionResult RegisterForClassesSelector()
        {
            return View(WebApplication3.Models.StudentDbConnectionClass.createAddEnrollmentViewScheduleHelper());
        }

        public ActionResult RegisterForClasses(String searchParameter, String searchType, String searchYear, String searchSemester)
        {
            return View(WebApplication3.Models.StudentDbConnectionClass.displayEnrollables(searchType, searchParameter, searchYear, searchSemester));
        }

        public ActionResult RegisteredForClass(int studentID, int sectionID, String year, String season)
        {
            String result = "<script> alert(\"" + WebApplication3.Models.StudentDbConnectionClass.register(sectionID, studentID, year, season) + "\"); </script>";
            return View((object) result);
        }

        public ActionResult RemoveEnrollmentSelector()
        {
            return View(WebApplication3.Models.StudentDbConnectionClass.createRemoveEnrollmentViewScheduleHelper());
        }

        public ActionResult RemoveEnrollmentOptions()
        {
            return View(WebApplication3.Models.StudentDbConnectionClass.createScheduleViewHelper());
        }
        
        public ActionResult RemoveEnrollmentResults(String userID, String year, String semester)
        {

            return View(WebApplication3.Models.StudentDbConnectionClass.submitDropClass(userID, year, semester));
        }

        public ActionResult RemoveEnrollment(int studentID, int sectionID, String semester, String year)
        {
            String result = "<script> alert(\"" + WebApplication3.Models.StudentDbConnectionClass.removeEnrollment(studentID, sectionID, semester, year) + "\"); </script>";
            return View((object)result);
        }
    }
}



