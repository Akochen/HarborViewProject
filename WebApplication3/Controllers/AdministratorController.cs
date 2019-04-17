using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.HelperClasses;

namespace WebApplication3.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        public ActionResult AdminHome()
        {
            return View();
        }

        public ActionResult AddCourseOptions()
        {
            return View(WebApplication3.Models.StudentDbConnectionClass.createSectionSearchHelper());
        }

        public ActionResult SearchMasterScheduleSelector()
        {
            return View(Models.FacultyDbConnectionClass.createViewScheduleHelper());
        }

        public ActionResult SearchScheduleResults()
        {
            return PartialView(new List<Section>());
        }
        [HttpPost]
        public ActionResult SearchScheduleResults(String searchYear, String searchSemester, String instructor, String days, String time, String courseID, String courseName, String department)
        {
            return PartialView(WebApplication3.Models.FacultyDbConnectionClass.searchSections(searchYear, searchSemester, instructor, days, time, courseID, courseName, department));
        }

        //****************************************************************//
        public ActionResult ViewFacultySchedule(String facultyID)
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.viewFacultySchedule(facultyID));
        }

        public ActionResult ViewFacultyScheduleSelector()
        {
            return View();
        }
    }
}