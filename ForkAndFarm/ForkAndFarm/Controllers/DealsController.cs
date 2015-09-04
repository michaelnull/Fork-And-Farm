﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ForkAndFarm.Models;

namespace ForkAndFarm.Controllers
{
    public class DealsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Deals/Create
        [Authorize]
        public ActionResult ProposePurchase(int? id)
        {
            SupplyOffer supplyoffer = db.SupplyOffers.FirstOrDefault(x => x.Id == id);
            Deal deal = new Deal();
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            deal.ProposedBy = currentuser.UserName;
            deal.Delivery = supplyoffer.Delivery;
            deal.PaymentTerms = supplyoffer.PaymentTerms;
            deal.Product = supplyoffer.Product;
            deal.Quantity = supplyoffer.Quantity;
            deal.Unit = supplyoffer.Unit;
            deal.UnitPrice = supplyoffer.UnitPrice;
            deal.OfferedTo = supplyoffer.ProposedBy;
            deal.OfferId = supplyoffer.Id;
            return View(deal);
        }

        // POST: Deals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProposePurchase(Deal deal)
        {
            var supplyoffer = db.SupplyOffers.FirstOrDefault(x => x.Id == deal.OfferId);
            if (supplyoffer == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var offeree = db.Users.FirstOrDefault(x=>x.UserName == supplyoffer.ProposedBy);

            var currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            deal.Product = supplyoffer.Product;
            deal.Unit = supplyoffer.Unit;
            deal.CreatedOn = DateTime.Now;
            deal.ExtPrice = deal.Quantity * deal.UnitPrice;
            deal.Complete = false;
            deal.OfferedTo = supplyoffer.ProposedBy;
            deal.ProposedBy = currentuser.UserName;
            deal.PaymentTerms = supplyoffer.PaymentTerms;
            if (ModelState.IsValid)
            {
                db.Deals.Add(deal);

                currentuser.DealsFromMe.Add(deal);
                offeree.DealsToMe.Add(deal);
                db.SaveChanges();
                
                return RedirectToAction("ShowProposedDeals", new { id = deal.OfferId });
            }
           

            return View(deal);
        }

        // GET: Deals
        public ActionResult ShowProposedDeals(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var list = db.Deals.Where(x => x.OfferId == id).OrderByDescending(d => d.CreatedOn).ToList();

            return View(list);
        }

        // GET: Deals
        public ActionResult Index()
        {
            return View(db.Deals.ToList());
        }

        // GET: Deals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            return View(deal);
        }

        // GET: Deals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Deals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AcceptedOn,AcceptanceComments,Complete,Deal_Id,ProposedBy_Id,AcceptedBy_Id,Product,Unit,Quantity,UnitPrice,ExtPrice,Delivery,PaymentTerms,CreatedOn,Memo")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                db.Deals.Add(deal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deal);
        }

        // GET: Deals/Edit/5
        [Authorize]
    
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            if (deal.ProposedBy == User.Identity.Name)
            {
                return View(deal);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Deals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AcceptedOn,AcceptanceComments,Complete,Deal_Id,ProposedBy_Id,AcceptedBy_Id,Product,Unit,Quantity,UnitPrice,ExtPrice,Delivery,PaymentTerms,CreatedOn,Memo")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deal);
        }

        // GET: Deals/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            if (deal.ProposedBy == User.Identity.Name)
            {
                return View(deal);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Deals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deal deal = db.Deals.Find(id);
            db.Deals.Remove(deal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
