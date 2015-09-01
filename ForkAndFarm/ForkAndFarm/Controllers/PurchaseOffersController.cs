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
    public class PurchaseOffersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PurchaseOffers
        public ActionResult Index()
        {
            return View(db.PurchaseOffers.ToList());
        }

        // GET: PurchaseOffers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOffer purchaseOffer = db.PurchaseOffers.Find(id);
            if (purchaseOffer == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOffer);
        }

        // GET: PurchaseOffers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PurchaseOffers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PurchaseOffer purchaseOffer)
        {
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            purchaseOffer.ProposedBy = currentUser.profile;
            purchaseOffer.CreatedOn = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.PurchaseOffers.Add(purchaseOffer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(purchaseOffer);
        }

        // GET: PurchaseOffers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOffer purchaseOffer = db.PurchaseOffers.Find(id);
            if (purchaseOffer == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOffer);
        }

        // POST: PurchaseOffers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PurchaseOrder,PurchaseOffer_Id,Memo,Product,Unit,Quantity,UnitPrice,ExtPrice,Delivery,PaymentTerms,CreatedOn")] PurchaseOffer purchaseOffer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseOffer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchaseOffer);
        }

        // GET: PurchaseOffers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOffer purchaseOffer = db.PurchaseOffers.Find(id);
            if (purchaseOffer == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOffer);
        }

        // POST: PurchaseOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseOffer purchaseOffer = db.PurchaseOffers.Find(id);
            db.PurchaseOffers.Remove(purchaseOffer);
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
