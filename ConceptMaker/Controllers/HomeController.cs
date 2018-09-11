using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConceptMaker.DAL;
using ConceptMaker.Models;

namespace ConceptMaker.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "O mnie";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Dane kontaktowe";

            return View();
        }
    }
}