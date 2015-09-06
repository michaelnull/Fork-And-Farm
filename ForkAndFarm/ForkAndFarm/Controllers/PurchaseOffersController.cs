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
        [Authorize]
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
            User currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            purchaseOffer.ProposedBy = currentuser.UserName;
            purchaseOffer.CreatedOn = DateTime.Now;
            purchaseOffer.ExtPrice = purchaseOffer.Quantity * purchaseOffer.UnitPrice;
            if (ModelState.IsValid)
            {
                
                currentuser.PurchaseOffers.Add(purchaseOffer);
                db.PurchaseOffers.Add(purchaseOffer);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(purchaseOffer);
        }

        // GET: PurchaseOffers/Edit/5
        [Authorize]
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
            User currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if(purchaseOffer.ProposedBy == User.Identity.Name)
            {
                return View(purchaseOffer);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: PurchaseOffers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PurchaseOffer purchaseOffer)
        {
            User currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            purchaseOffer.ExtPrice = purchaseOffer.Quantity * purchaseOffer.UnitPrice;
            if (ModelState.IsValid)
            {
                db.Entry(purchaseOffer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchaseOffer);
        }

        // GET: PurchaseOffers/Delete/5
        [Authorize]
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
            User currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (purchaseOffer.ProposedBy == User.Identity.Name)
            {
                return View(purchaseOffer);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
