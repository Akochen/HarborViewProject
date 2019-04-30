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
        public ActionResult DisplayGradeByCourse()
        {
            return PartialView();
        }

        public ActionResult DisplayGradeByCourseSelect()
        {
            return PartialView();
        }
        //Display average grade by course

        //Display average grade by professor

        //Display average grade by year

        //Display average grade by semster :^)



    }
}