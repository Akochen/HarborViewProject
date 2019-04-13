using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}