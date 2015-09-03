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

        // GET: Deals
        public ActionResult Index()
        {
           ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            var deals = from d in db.Deals.Include(d => d.ProposedBy)
                        select new DealListVM
                        {
                            Id = d.Id,
                            AcceptanceComments = d.AcceptanceComments,
                            AcceptedBy = d.OfferedTo.UserName + " @ " + d.OfferedTo.Organization,
                            AcceptedOn = d.AcceptedOn,
                            CreatedOn = d.CreatedOn,
                            Delivery = d.Delivery,
                            ExtPrice = d.ExtPrice,
                            IsComplete = d.Complete,
                            Memo = d.Memo,
                            PaymentTerms = d.PaymentTerms,
                            Product = d.Product,
                            ProposedBy = d.ProposedBy.UserName + " @ " + d.ProposedBy.Organization,
                            Quantity = d.Quantity,
                            Unit = d.Unit,
                            UnitPrice = d.UnitPrice
                        };
            return View(deals.ToList());
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
        public ActionResult ProposePurchase(int? id)
        {
            SupplyOffer supplyoffer = db.SupplyOffers.FirstOrDefault(x => x.Id == id);
            Deal deal = new Deal();
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            deal.ProposedBy = currentuser;
            deal.Complete = false;
            deal.CreatedOn = DateTime.Today;
            deal.Delivery = supplyoffer.Delivery;
            deal.ExtPrice = supplyoffer.ExtPrice;
            deal.PaymentTerms = supplyoffer.PaymentTerms;
            deal.Product = supplyoffer.Product;
            deal.Quantity = supplyoffer.Quantity;
            deal.Unit = supplyoffer.Unit;
            deal.UnitPrice = supplyoffer.UnitPrice;
            deal.OfferedTo = supplyoffer.ProposedBy;
            return View(deal);
        }

        // POST: Deals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProposePurchase([Bind(Include = "Id,AcceptedOn,AcceptanceComments,Complete,Deal_Id,ProposedBy_Id,AcceptedBy_Id,Product,Unit,Quantity,UnitPrice,ExtPrice,Delivery,PaymentTerms,CreatedOn,Memo")] Deal deal)
        {
            var currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            deal.CreatedOn = DateTime.Now;
            deal.AcceptedOn = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Deals.Add(deal);
               
                currentuser.DealsFromMe.Add(deal);
                deal.OfferedTo.DealsToMe.Add(deal);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deal);
        }

        // GET: Deals/Edit/5
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
            return View(deal);
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
            return View(deal);
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
