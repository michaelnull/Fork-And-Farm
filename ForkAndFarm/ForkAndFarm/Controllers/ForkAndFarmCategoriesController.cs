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
    public class ForkAndFarmCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ForkAndFarmCategories
        public ActionResult Index()
        {
            return View(db.ForkAndFarmCategories.ToList());
        }

        // GET: ForkAndFarmCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForkAndFarmCategory forkAndFarmCategory = db.ForkAndFarmCategories.Find(id);
            if (forkAndFarmCategory == null)
            {
                return HttpNotFound();
            }
            return View(forkAndFarmCategory);
        }

        // GET: ForkAndFarmCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ForkAndFarmCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ListName")] ForkAndFarmCategory forkAndFarmCategory)
        {
            if (ModelState.IsValid)
            {
                db.ForkAndFarmCategories.Add(forkAndFarmCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(forkAndFarmCategory);
        }

        // GET: ForkAndFarmCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForkAndFarmCategory forkAndFarmCategory = db.ForkAndFarmCategories.Find(id);
            if (forkAndFarmCategory == null)
            {
                return HttpNotFound();
            }
            return View(forkAndFarmCategory);
        }

        // POST: ForkAndFarmCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ListName")] ForkAndFarmCategory forkAndFarmCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forkAndFarmCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(forkAndFarmCategory);
        }

        // GET: ForkAndFarmCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForkAndFarmCategory forkAndFarmCategory = db.ForkAndFarmCategories.Find(id);
            if (forkAndFarmCategory == null)
            {
                return HttpNotFound();
            }
            return View(forkAndFarmCategory);
        }

        // POST: ForkAndFarmCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ForkAndFarmCategory forkAndFarmCategory = db.ForkAndFarmCategories.Find(id);
            db.ForkAndFarmCategories.Remove(forkAndFarmCategory);
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
