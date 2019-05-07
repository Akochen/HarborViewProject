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
            TempData["result"] = "Section successfully added!";
            return RedirectToAction("AdminHome");
        }

        public ActionResult UpdateSection(String sectionId)
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.updateSection(sectionId));
        }

        public ActionResult UpdateSectionForm(String courseId)
        {
            return View(WebApplication3.Models.AdminDbConnectionClass.createUpdateSectionForm(courseId));
        }
        
        public ActionResult UpdateSectionSubmit(string credits, string courseName, string building, string room, string semester, string year, string type, string seatCapacity, string professor, string d1, string d2, string d3, string time, string sectionId)
        {
            int check = WebApplication3.Models.AdminDbConnectionClass.updateSectionCheck(credits, courseName, building, room, semester, year, type, seatCapacity, professor, d1, d2, d3, time, sectionId);
            if (check == 0)
            {
                //success
                TempData["result"] = "Section successfuly updated.";
                return RedirectToAction("SearchMasterScheduleSelector");
            }
            else if (check == 1)
            {
                //room in use
                TempData["result"] = "Error: Room is in use at that time.";
                return RedirectToAction("SearchMasterScheduleSelector");
            }
            else if (check == 2)
            {
                //period does not exist (success)
                TempData["result"] = "Section successfuly updated.";
                return RedirectToAction("SearchMasterScheduleSelector");
            }
            else if (check == 3)
            {
                //professor has class at that time
                TempData["result"] = "Error: " + professor +" has class at that time.";
                return RedirectToAction("SearchMasterScheduleSelector");
            }
            else if(check == 4)
            {
                TempData["result"] = "Something went wrong with time selection. Please try again in a bit.";
                return RedirectToAction("SearchMasterScheduleSelector");
            }
            else
            {
                TempData["result"] = "Something went wrong. Please check your connection and then call technical support.";
                return RedirectToAction("SearchMasterScheduleSelector");
            }
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

        public ActionResult UpdateGrade(string courseID, string name, string sectionID, string grade, string studentID)
        {
            StudentEnrollment s = new StudentEnrollment(courseID, name, sectionID, grade, studentID);
            TempData["result"] = WebApplication3.Models.AdminDbConnectionClass.updateGrade(s);
            return RedirectToAction("AdminHome");
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
        public ActionResult UpdateStudentInformation(string streetName, string city, string state, string zip, string studentID)
        {
            StudentInfo s = new StudentInfo(streetName, city, state, zip);
            TempData["result"] = WebApplication3.Models.AdminDbConnectionClass.UpdateStudentInformation(s, studentID);
            return RedirectToAction("AdminHome");
        }

        public ActionResult UpdateStudentInformationPage(string streetName, string city, string state, string zip)
        {
            StudentInfo s = new StudentInfo(streetName, city, state, zip);
            return View(s);
        }
        public ActionResult ViewStudentInformation(String userID)
        {
            return View((object)WebApplication3.Models.AdminDbConnectionClass.ViewStudentInformation(userID));
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