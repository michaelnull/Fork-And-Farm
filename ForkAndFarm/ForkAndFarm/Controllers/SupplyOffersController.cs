using System;
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
    public class SupplyOffersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SupplyOffers
        public ActionResult Index()
        {
            return View(db.SupplyOffers.ToList());
        }

        // GET: SupplyOffers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplyOffer supplyOffer = db.SupplyOffers.Find(id);
            if (supplyOffer == null)
            {
                return HttpNotFound();
            }
            return View(supplyOffer);
        }

        // GET: SupplyOffers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplyOffers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Invoice,SupplyOffer_Id,Product,Unit,Quantity,UnitPrice,ExtPrice,Delivery,PaymentTerms,CreatedOn,Memo")] SupplyOffer supplyOffer)
        {
            if (ModelState.IsValid)
            {
                db.SupplyOffers.Add(supplyOffer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplyOffer);
        }

        // GET: SupplyOffers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplyOffer supplyOffer = db.SupplyOffers.Find(id);
            if (supplyOffer == null)
            {
                return HttpNotFound();
            }
            return View(supplyOffer);
        }

        // POST: SupplyOffers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Invoice,SupplyOffer_Id,Product,Unit,Quantity,UnitPrice,ExtPrice,Delivery,PaymentTerms,CreatedOn,Memo")] SupplyOffer supplyOffer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplyOffer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplyOffer);
        }

        // GET: SupplyOffers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplyOffer supplyOffer = db.SupplyOffers.Find(id);
            if (supplyOffer == null)
            {
                return HttpNotFound();
            }
            return View(supplyOffer);
        }

        // POST: SupplyOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupplyOffer supplyOffer = db.SupplyOffers.Find(id);
            db.SupplyOffers.Remove(supplyOffer);
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
