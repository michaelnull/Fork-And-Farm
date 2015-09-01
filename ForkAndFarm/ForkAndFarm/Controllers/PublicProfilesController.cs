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
    public class PublicProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PublicProfiles
        public ActionResult Index()
        {
            return View(db.Traders.ToList());
        }

        // GET: PublicProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trader publicProfile = db.Traders.Find(id);
            if (publicProfile == null)
            {
                return HttpNotFound();
            }
            return View(publicProfile);
        }

        // GET: PublicProfiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PublicProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Organization,Address,Address2,Zip")] Trader publicProfile)
        {
            if (ModelState.IsValid)
            {
                db.Traders.Add(publicProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(publicProfile);
        }

        // GET: PublicProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trader publicProfile = db.Traders.Find(id);
            if (publicProfile == null)
            {
                return HttpNotFound();
            }
            return View(publicProfile);
        }

        // POST: PublicProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Organization,Address,Address2,Zip")] Trader publicProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publicProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publicProfile);
        }

        // GET: PublicProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trader publicProfile = db.Traders.Find(id);
            if (publicProfile == null)
            {
                return HttpNotFound();
            }
            return View(publicProfile);
        }

        // POST: PublicProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trader publicProfile = db.Traders.Find(id);
            db.Traders.Remove(publicProfile);
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
