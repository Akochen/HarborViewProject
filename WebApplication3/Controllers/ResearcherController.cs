using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class ResearcherController : Controller
    {
        // GET: Researcher
        public ActionResult ResearcherHome()
        {
            return View();
        }
        //Display average grade for all courses (on home)
        //Display average grade by course
        public ActionResult DisplayGradeByCourse(String courseID, String majorID, String year, String semester)
        {
            Session["GpaByCourse"] = WebApplication3.Models.ResearcherDbConnectionClass.getAverageGradeByCourse(courseID, year, semester);
            TempData["courseSelect"] = courseID;
            TempData["majorSelect"] = majorID;
            TempData["yearSelect"] = year;
            TempData["semsterSelect"] = semester;
            return RedirectToAction("ResearcherHome");
        }

        public PartialViewResult DisplayGradeByCourseSelect()
        {
            return PartialView(WebApplication3.Models.ResearcherDbConnectionClass.ListAllCourses());
        }
        //Display average grade by Major
        public ActionResult DisplayGradeByMajor(String courseID, String majorID, String year, String semester)
        {
            Session["GpaByMajor"] = WebApplication3.Models.ResearcherDbConnectionClass.getAverageGradeByMajor(majorID, year, semester);
            TempData["courseSelect"] = courseID;
            TempData["majorSelect"] = majorID;
            TempData["yearSelect"] = year;
            TempData["semsterSelect"] = semester;
            return RedirectToAction("ResearcherHome");
        }

        public PartialViewResult DisplayGradeByMajorSelect()
        {
            return PartialView(WebApplication3.Models.ResearcherDbConnectionClass.listAllMajors());
        }

        //Create selects for semesters and years
        public PartialViewResult ShowSemesterSelects()
        {
            return PartialView(WebApplication3.Models.ResearcherDbConnectionClass.createSemesterList());
        }

        //Num of students by major
        //Create search by semester view
        public PartialViewResult StudentsByDegreeSelector()
        {
            return PartialView(WebApplication3.Models.ResearcherDbConnectionClass.createSemesterList());
        }
        public PartialViewResult StudentsByDegree(String semester, String year)
        {
            TempData["majorYear"] = year;
            TempData["majorSemester"] = semester;
            return PartialView(WebApplication3.Models.ResearcherDbConnectionClass.getStudentsByMajorCount(year, semester));
        }

        //Display average grade by professor
        //public ActionResult DisplayGradeByProfessor(String courseID, String professorID, String year, String semester)
        //{
        //    TempData["GpaByProfessor"] = WebApplication3.Models.ResearcherDbConnectionClass.getAverageGradeByProfessor(professorID, year, semester);
        //    return RedirectToAction("ResearcherHome");
        //}

        //public PartialViewResult DisplayGradeByProfessorSelect()
        //{
        //    return PartialView(WebApplication3.Models.ResearcherDbConnectionClass.listAllProfessors());
        //}

        //Display average grade by year

        //Display average grade by semster :^)



    }
}