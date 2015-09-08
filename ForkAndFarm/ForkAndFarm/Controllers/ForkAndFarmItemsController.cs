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
    public class ForkAndFarmItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ForkAndFarmItems
        public ActionResult Index()
        {
            return View(db.ForkAndFarmItems.ToList());
        }

        // GET: ForkAndFarmItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForkAndFarmItem forkAndFarmItem = db.ForkAndFarmItems.Find(id);
            if (forkAndFarmItem == null)
            {
                return HttpNotFound();
            }
            return View(forkAndFarmItem);
        }

        // GET: ForkAndFarmItems/Create
        public ActionResult AddNewItem(string type, string item)
        {
            ForkAndFarmCategory category = db.ForkAndFarmCategories.FirstOrDefault(x => x.ListName == type);
            ForkAndFarmItem thing = new ForkAndFarmItem();
            thing.ItemName = item;
            category.ItemList.Add(thing);
            return Json(category, JsonRequestBehavior.AllowGet);
        }   

        // POST: ForkAndFarmItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewItem([Bind(Include = "Id,ItemName")] ForkAndFarmItem forkAndFarmItem)
        {
            if (ModelState.IsValid)
            {
                db.ForkAndFarmItems.Add(forkAndFarmItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(forkAndFarmItem);
        }

        // GET: ForkAndFarmItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForkAndFarmItem forkAndFarmItem = db.ForkAndFarmItems.Find(id);
            if (forkAndFarmItem == null)
            {
                return HttpNotFound();
            }
            return View(forkAndFarmItem);
        }

        // POST: ForkAndFarmItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemName")] ForkAndFarmItem forkAndFarmItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forkAndFarmItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(forkAndFarmItem);
        }

        // GET: ForkAndFarmItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForkAndFarmItem forkAndFarmItem = db.ForkAndFarmItems.Find(id);
            if (forkAndFarmItem == null)
            {
                return HttpNotFound();
            }
            return View(forkAndFarmItem);
        }

        // POST: ForkAndFarmItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ForkAndFarmItem forkAndFarmItem = db.ForkAndFarmItems.Find(id);
            db.ForkAndFarmItems.Remove(forkAndFarmItem);
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
