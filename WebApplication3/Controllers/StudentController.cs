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
        private List<Section> sections;
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
        public ActionResult SearchSectionResults(String searchParameter, String searchType, String searchYear, String searchSemester)
        {
            return PartialView(WebApplication3.Models.StudentDbConnectionClass.searchSections(searchType, searchParameter, searchYear, searchSemester));
        }
    }
}