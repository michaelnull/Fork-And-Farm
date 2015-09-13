using ForkAndFarm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForkAndFarm.Controllers
{
    public class PortalVMController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Portal()
        {
            var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            PortalVM portalview = new PortalVM();
            portalview.UserName = user.UserName;
            portalview.UserRole = user.UserRole.ToString();
            //portalview.MySupplyOffers = user.SupplyOffers;
            portalview.Organization = user.Organization;
            portalview.Phone = user.Phone;
            if (user.DealsToMe != null)
            {
                portalview.DealToMeCount = user.DealsToMe.Count();
            }
            else
            {
                portalview.DealToMeCount = 0;
            }
            
            if (user.DealsFromMe != null)
            {
                portalview.DealFromMeCount = user.DealsFromMe.Count();
            }
            else
            {
                portalview.DealFromMeCount = 0;
            }

            if (user.MyAdvertisements != null)
            {
                portalview.AdCount = user.MyAdvertisements.Count();
            }
            else
            {
                portalview.AdCount = 0;
            }

           
           
            return View(portalview);
        }
        // GET: PortalVM
        [Authorize]
        public ActionResult OffersToMe()
        {
            var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var list = user.DealsToMe.OrderByDescending(x => x.CreatedOn);
            return View(list);
        }
        [Authorize]
        public ActionResult MyAds()
        {
            var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var list = user.MyAdvertisements.OrderByDescending(x => x.CreatedOn);
            return View(list);
        }

        [Authorize]
        public ActionResult MySupplyOffers()
        {
            var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var list = user.MyAdvertisements.OrderByDescending(x=>x.CreatedOn);
            return View(list);
        }

        [Authorize]
        public ActionResult MyPurchaseOffers()
        {
            var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var list = user.MyAdvertisements.OrderByDescending(x => x.CreatedOn);
            return View(list);
        }
       
        [Authorize]
        public ActionResult GetUserInfo()
        {
            var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            PortalVM portalview = new PortalVM();
            portalview.UserName = user.UserName;
            portalview.UserRole = user.UserRole.ToString();
            portalview.Organization = user.Organization;
            portalview.Phone = user.Phone;
         
            if (user.DealsToMe != null)
            {
                portalview.DealToMeCount = user.DealsToMe.Count();
            }
            else
            {
                portalview.DealToMeCount = 0;
            }

            if (user.DealsFromMe != null)
            {
                portalview.DealFromMeCount = user.DealsFromMe.Count();
            }
            else
            {
                portalview.DealFromMeCount = 0;
            }

            if (user.MyAdvertisements != null)
            {
                portalview.AdCount = user.MyAdvertisements.Count();
            }
            else
            {
                portalview.AdCount = 0;
            }
            return Json(portalview, JsonRequestBehavior.AllowGet);
        }
        
    }
    }

