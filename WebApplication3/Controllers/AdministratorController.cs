using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public ActionResult AddSectionForm()
        {
            return View(Models.AdminDbConnectionClass.addSectionForm());
        }
        //[HttpPost]
        public ActionResult AddCourseOptions()
        {
            return View(Models.AdminDbConnectionClass.addCourseHelper2());
        }

        public ActionResult AddCourse(AddCourse form)
        {
            return View((object)WebApplication3.Models.AdminDbConnectionClass.addCourse(form));
        }

        public ActionResult SearchMasterScheduleSelector()
        {
            return View(Models.AdminDbConnectionClass.createViewScheduleHelper());
        }

        public ActionResult SearchScheduleResults()
        {
            return PartialView(new List<Section>());
        }
        [HttpPost]
        public ActionResult SearchScheduleResults(String searchYear, String searchSemester, String instructor, String days, String time, String courseID, String courseName, String department)
        {
            return PartialView(WebApplication3.Models.AdminDbConnectionClass.searchSections(searchYear, searchSemester, instructor, days, time, courseID, courseName, department));
        }

        public ActionResult ViewStudentScheduleSelector()
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.createViewStudentScheduleHelper());
        }

        public ActionResult ViewStudentSchedule(String studentID, String semester, String year)
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.viewStudentSchedule(studentID, year, semester));
        }

        public ActionResult ViewTranscriptSelector()
        {
            return View();
        }

        public ActionResult ViewTranscript(int studentID)
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.viewTranscript(studentID));
        }

        public ActionResult ViewAnAdvisorAviseeListSelector()
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.createAdvisorSelectorHelper());
        }

        public ActionResult ViewAnAdvisorAviseeList(String userID)
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.viewAdvisorAdviseeList(userID));
        }

        public ActionResult ViewFacultySchedule(String facultyID)
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.viewFacultySchedule(facultyID));
        }

        public ActionResult ViewFacultyScheduleSelector()
        {
            return View();
        }

        public ActionResult UpdateStudentGradeStudentSelector()
        {
            return View();
        }

        public ActionResult UpdateStudentGradeClassSelector(String studentID)
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.createUpdateGradeList(studentID));
        }

        public ActionResult UpdateStudentGradeGradeSelector(string courseID, string name, string sectionID, string grade, string studentID)
        {
            StudentEnrollment s = new StudentEnrollment(courseID, name, sectionID, grade, studentID);
            return View(s);
        }

        public String UpdateGrade(string courseID, string name, string sectionID, string grade, string studentID)
        {
            StudentEnrollment s = new StudentEnrollment(courseID, name, sectionID, grade, studentID);
            return WebApplication3.Models.AdminDbConnectionClass.updateGrade(s);
        }

        public ActionResult ViewStudentHoldSelector()
        {
            return View();
        }

        public ActionResult ViewStudentHold(String studentID)
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.viewHolds(studentID));
        }

        public ActionResult removeHold(String holdType, String studentID, String year, String semester)
        {
            return View((object)WebApplication3.Models.AdminDbConnectionClass.removeHold(holdType, studentID, year, semester));
        }

        public ActionResult EditMajorSelector()
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.editMajorSelectorHelper());
        }

        public ActionResult EditMajor(String majorID)
        {
            return View(Models.AdminDbConnectionClass.editMajor(majorID));
        }

        public ActionResult EditMajorResult(String courseID, String courseAttr)
        {
            String result = "<script> alert(\"" + WebApplication3.Models.AdminDbConnectionClass.editMajorResults(courseID, courseAttr) + "\"); </script>";
            return View((object)result);
        }

        public ActionResult DegreeAuditStudentSelector()
        {
            return View();
        }

        public ActionResult ViewDegreeAuditSelector(String studentID)
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.createDegreeAuditSelector(studentID));
        }

        [HttpPost]
        public ActionResult ViewDegreeAudit(String studentID, String majorID)
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.degreeAudit(studentID, majorID));
        }

        public ActionResult EditCatalogSelector()
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.CreateEditCatalogSelector());
        }

        public ActionResult EditCatalogDisplayDetails(string courseID)
        {
            return View(Models.AdminDbConnectionClass.editCatalogDisplayCourseDetails(courseID));
        }

        public ActionResult EditCatalogPrereqSelector()
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.CreateEditCatalogSelector());
        }

        public ActionResult EditCatalogRemovePrereq(string courseID, string prereqID)
        {
            TempData["msg"] = WebApplication3.Models.AdminDbConnectionClass.editCatalogRemovePrereq(courseID, prereqID);
            return RedirectToAction("EditCatalogDisplayDetails", new { courseID = courseID });
        }
    }
}