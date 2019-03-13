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

    }
}