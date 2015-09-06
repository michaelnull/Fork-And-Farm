using ForkAndFarm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ForkAndFarm.Controllers
{
    public class HomeController : Controller
    {
        
       

        public ActionResult About()
        {
            ViewBag.Message = "A little about Fork & Farm";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Fork & Farm contact info";

            return View();
        }
    }
}