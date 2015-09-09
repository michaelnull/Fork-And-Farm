﻿using ForkAndFarm.Models;
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
            portalview.UserRole = user.UserRole;
            portalview.MyAdvertisements = user.MyAdvertisements;
            //portalview.MySupplyOffers = user.SupplyOffers;
            portalview.Organization = user.Organization;
            portalview.Phone = user.Phone;
           
            portalview.DealsToMe = user.DealsToMe;
            portalview.DealsFromMe = user.DealsFromMe;
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
        // GET: PortalVM/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PortalVM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PortalVM/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PortalVM/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PortalVM/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PortalVM/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PortalVM/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
