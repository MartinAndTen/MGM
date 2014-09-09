using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProjectRestaurangMVC01.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Våran apps förstasida HEJHEJ";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Vår OM-Sida";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Kontakt";

            return View();
        }
    }
}
