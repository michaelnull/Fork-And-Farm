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
    public class ForkAndFarmUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult PurchaserPortal()
        {
            User currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            return View(currentuser);
        }
        
        [Authorize]
        public ActionResult SupplierPortal()
        {
            User currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            return View(currentuser);
        }

        // GET: ForkAndFarmUsers
        [Authorize]
        public ActionResult Index()
        {
            User currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            if (currentuser.UserRole == Models.User.Portal.Purchaser)
            {
               return RedirectToAction("PurchaserPortal");
            }

            return RedirectToAction("SupplierPortal");
          

        }

        // GET: ForkAndFarmUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User forkAndFarmUser = db.ForkAndFarmUsers.Find(id);
            if (forkAndFarmUser == null)
            {
                return HttpNotFound();
            }
            return View(forkAndFarmUser);
        }

        // GET: ForkAndFarmUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ForkAndFarmUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Organization,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User forkAndFarmUser)
        {
            if (ModelState.IsValid)
            {
                db.ForkAndFarmUsers.Add(forkAndFarmUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(forkAndFarmUser);
        }

        // GET: ForkAndFarmUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User forkAndFarmUser = db.ForkAndFarmUsers.Find(id);
            if (forkAndFarmUser == null)
            {
                return HttpNotFound();
            }
            return View(forkAndFarmUser);
        }

        // POST: ForkAndFarmUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Organization,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User forkAndFarmUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forkAndFarmUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(forkAndFarmUser);
        }

        // GET: ForkAndFarmUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User forkAndFarmUser = db.ForkAndFarmUsers.Find(id);
            if (forkAndFarmUser == null)
            {
                return HttpNotFound();
            }
            return View(forkAndFarmUser);
        }

        // POST: ForkAndFarmUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User forkAndFarmUser = db.ForkAndFarmUsers.Find(id);
            db.ForkAndFarmUsers.Remove(forkAndFarmUser);
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
