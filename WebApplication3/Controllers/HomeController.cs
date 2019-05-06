using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using WebApplication3.HelperClasses;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public String getUserID(String email)
        {
            return (WebApplication3.Models.UserDbConnectionClass.getUserID(email));
        }

        public String isFacultyAdvisor(String email)
        {
            return (WebApplication3.Models.UserDbConnectionClass.isFacultyAdvisor(email));
        }

        public ActionResult LogOut()
        {
            Session.RemoveAll();
            return RedirectToAction("Index");
        }

       
    }
}