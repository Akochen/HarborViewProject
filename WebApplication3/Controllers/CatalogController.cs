using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.HelperClasses;

namespace WebApplication3.Controllers
{
    public class CatalogController : Controller
    {
        Catalog catalog;
        public CatalogController()
        {
            catalog = Models.UserDbConnectionClass.createCatalog();
        }
        // GET: Catalog
        public ActionResult Catalog()
        {
            return View(catalog);
        }

        public ActionResult SearchMasterScheduleSelector()
        {
            return View(Models.UserDbConnectionClass.createViewScheduleHelper());
        }

        public ActionResult SearchScheduleResults()
        {
            return PartialView(new List<Section>());
        }
        [HttpPost]
        public ActionResult SearchScheduleResults(String searchYear, String searchSemester, String instructor, String days, String time, String courseID, String courseName, String department)
        {
            return PartialView(WebApplication3.Models.UserDbConnectionClass.searchSections(searchYear, searchSemester, instructor, days, time, courseID, courseName, department));
        }
    }
}