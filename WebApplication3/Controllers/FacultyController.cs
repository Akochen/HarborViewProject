using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.HelperClasses;

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

        public ActionResult ViewStudentScheduleSelector()
        {
            return View(WebApplication3.Models.FacultyDbConnectionClass.createViewStudentScheduleHelper());
        }

        public ActionResult ViewStudentSchedule(String studentID, String semester, String year)
        {
            if (!studentID.Equals(""))
            {
                return View(WebApplication3.Models.FacultyDbConnectionClass.viewStudentSchedule(studentID, year, semester));
            } else
            {
                return View();
            }
        }

        public ActionResult ViewStudentHoldSelector()
        {
            return View();
        }

        public ActionResult ViewStudentHold(String studentID)
        {
            return View(WebApplication3.Models.FacultyDbConnectionClass.viewHolds(studentID));
        }

        public ActionResult ViewAdviseeList(String userID)
        {
            return View(WebApplication3.Models.FacultyDbConnectionClass.viewAdviseeList(userID));
        }

        public ActionResult ViewTranscriptSelector()
        {
            return View();
        }

        public ActionResult ViewTranscript(int studentID)
        {
            return View(WebApplication3.Models.FacultyDbConnectionClass.viewTranscript(studentID));
        }

        public ActionResult ViewSemesterHistorySelector()
        {
            return View(WebApplication3.Models.FacultyDbConnectionClass.createFacultySemesterHistoryScheduleViewHelper());
        }

        [HttpPost]
        public ActionResult ViewSemesterHistory(String userID, String year, String semester)
        {
            return PartialView(WebApplication3.Models.FacultyDbConnectionClass.viewFacultySemesterHistory(userID, year, semester));
        }

        public ActionResult ViewSemesterHistoryEnrollee(String sectionID)
        {
            return View(WebApplication3.Models.FacultyDbConnectionClass.viewFacultySemesterEnrolleeList(sectionID));
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

        public ActionResult DegreeAuditStudentSelector()
        {
            return View();
        }

        public ActionResult ViewDegreeAuditSelector(String studentID)
        {
            return View(WebApplication3.Models.FacultyDbConnectionClass.createDegreeAuditSelector(studentID));
        }

        [HttpPost]
        public ActionResult ViewDegreeAudit(String studentID, String majorID)
        {
            return View(WebApplication3.Models.FacultyDbConnectionClass.degreeAudit(studentID, majorID));
        }
    }
}