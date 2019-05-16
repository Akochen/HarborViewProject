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

        public String LogIn()
        {
            String userType = WebApplication3.Models.UserDbConnectionClass.login("select case [type_id] When 1 then 'Student' When 2 then 'Faculty' When 3 then 'Admin' When 4 then 'Researcher' End from[user] where[email] = '" + Request["email"] + "' and [password]= '" + Request["password"] + "'");

            return null;
        }

        public ActionResult LogOut()
        {
            Session.RemoveAll();
            return RedirectToAction("Index");
        }

       
    }
}