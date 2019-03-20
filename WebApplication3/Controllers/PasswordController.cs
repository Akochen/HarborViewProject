using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class PasswordController : Controller
    {
        public ActionResult ChangePassword()
        {
            return View();
        }

        public String ActivateChangePassword(String email, String id, String password)
        {
            String result = WebApplication3.Models.UserDbConnectionClass.changePasswordCheck(email, id, password);
            return result;
        }
    }
}