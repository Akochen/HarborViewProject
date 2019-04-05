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

        public ActionResult AssignGradeEnrollees(String UserID, String semester, String year)
        {
            return View(WebApplication3.Models.FacultyDbConnectionClass.viewEnrolleeList());
        }
    }
}