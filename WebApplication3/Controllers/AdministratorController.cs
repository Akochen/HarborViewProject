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

        public ActionResult AddSection(String courseId, String buildingId, String roomId, String semester, String year, String type, String capacity)
        {
            String msg = WebApplication3.Models.AdminDbConnectionClass.addSection(courseId, roomId, buildingId, semester, year, type, capacity);
            return RedirectToAction("UpdateSection");
        }

        public ActionResult UpdateSection(String courseId)
        {
            return View();
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

        public ActionResult UpdateStudentInformationSelector()
        {
            return View();
        }
        public String UpdateStudentInformation(string streetName, string city, string state, string zip, string studentID)
        {
            StudentInfo s = new StudentInfo(streetName, city, state, zip);
            return WebApplication3.Models.AdminDbConnectionClass.UpdateStudentInformation(s, studentID);
        }

        public ActionResult UpdateStudentInformationPage(string streetName, string city, string state, string zip)
        {
            StudentInfo s = new StudentInfo(streetName, city, state, zip);
            return View(s);
        }
        public ActionResult ViewStudentInformation(String streetName, String city, String state, String zip, String userID)
        {
            return View((object)WebApplication3.Models.AdminDbConnectionClass.ViewStudentInformation(streetName, city, state, zip, userID));
        }

        public ActionResult EditMajorSelector()
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.editMajorSelectorHelper());
        }
    
        public ActionResult EditMajor(String majorID)
        {
            return View(Models.AdminDbConnectionClass.editMajor(majorID));
        }

        public ActionResult EditMajorResult(String courseID, String courseAttr,String majorID)
        {

           //return RedirectToAction("EditMajor", new { courseID = courseID });
            String result = "<script> alert(\"" + WebApplication3.Models.AdminDbConnectionClass.editMajorResults(courseID, courseAttr,majorID) + "\"); </script>";
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

        public ActionResult EditCatalogAddPrereq(string courseID, string prereqID)
        {
            TempData["msg"] = WebApplication3.Models.AdminDbConnectionClass.editCatalogAddPrereq(courseID, prereqID);
            return RedirectToAction("EditCatalogDisplayDetails", new { courseID = courseID });
        }

        public ActionResult EditCatalogEditDescription(string courseID, string description)
        {
            TempData["msg"] = WebApplication3.Models.AdminDbConnectionClass.editCatalogEditDescriptions(courseID, description);
            return RedirectToAction("EditCatalogDisplayDetails", new { courseID = courseID });
        }
    }
}