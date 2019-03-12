using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OpenCatalog()
        {
            return View("~/Views/UserViews/Catalog.cshtml");
        }

        public String GetCourses()
        {
            return Models.UserDbConnectionClass.displayCatalogCourses();
        }
    }
}