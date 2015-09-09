//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using ForkAndFarm.Models;

//namespace ForkAndFarm.Controllers
//{
//    public class SupplyOffersController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        // GET: SupplyOffers
        
//        public ActionResult Index(string id)
//        {
//            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
//            ViewBag.User = currentuser.UserName;
//            ViewBag.Role = currentuser.UserRole.ToString();
//            List<SupplyOffer> list = new List<SupplyOffer>();
//            if (id == null || id == "")

//            {
//                list = db.SupplyOffers.OrderByDescending(x => x.CreatedOn).ToList();
//            }
//            else
//            {
//                list = db.SupplyOffers.Where(x => x.Product.Contains(id)).OrderByDescending(x => x.CreatedOn).ToList();
//            }
//            return View(list);
//        }

//        // GET: SupplyOffers/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            SupplyOffer supplyOffer = db.SupplyOffers.Find(id);
//            if (supplyOffer == null)
//            {
//                return HttpNotFound();
//            }
//            return View(supplyOffer);
//        }

//        // GET: SupplyOffers/Create
//        public ActionResult Create()
//        {
           
          
//            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
//            if(currentuser.UserRole == ForkAndFarmUser.Portal.Supplier)
//            {
//                return View();
//            }
//            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

//        }

//        // POST: SupplyOffers/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [Authorize]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Id,Invoice,SupplyOffer_Id,Product,Unit,Quantity,UnitPrice,ExtPrice,Delivery,PaymentTerms,CreatedOn,Memo,ProposedByOrganization")] SupplyOffer supplyOffer)
//        {
//            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
//            supplyOffer.ProposedBy = currentuser.UserName;
//            supplyOffer.ProposedByOrganization = currentuser.Organization;
//            supplyOffer.ProposedByPhone = currentuser.Phone;
//            supplyOffer.CreatedOn = DateTime.Now;
//            supplyOffer.ExtPrice = supplyOffer.Quantity * supplyOffer.UnitPrice;

//            if (ModelState.IsValid)
//            {
               
//                currentuser.SupplyOffers.Add(supplyOffer);
//                db.SupplyOffers.Add(supplyOffer);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(supplyOffer);
//        }

//        // GET: SupplyOffers/Edit/5
//        [Authorize]
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
           

//            SupplyOffer supplyOffer = db.SupplyOffers.Find(id);
//            if (supplyOffer == null)
//            {
//                return HttpNotFound();
//            }
//            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
//            if (supplyOffer.ProposedBy == currentuser.UserName)
//            {
//                return View(supplyOffer);
//            }
//            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

//        }

//        // POST: SupplyOffers/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,Invoice,SupplyOffer_Id,Product,Unit,Quantity,UnitPrice,ExtPrice,Delivery,PaymentTerms,CreatedOn,Memo,ProposedBy")] SupplyOffer supplyOffer)
//        {
//            supplyOffer.Memo = "edited on " + DateTime.Now + " " + supplyOffer.Memo;
//            supplyOffer.ExtPrice = supplyOffer.UnitPrice * supplyOffer.Quantity;
//            if (ModelState.IsValid)
//            {
//                db.Entry(supplyOffer).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(supplyOffer);
//        }

//        // GET: SupplyOffers/Delete/5
//        [Authorize]
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            SupplyOffer supplyOffer = db.SupplyOffers.Find(id);
//            if (supplyOffer == null)
//            {
//                return HttpNotFound();
//            }

//            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
//            if (supplyOffer.ProposedBy == currentuser.UserName)
//            {
//                return View(supplyOffer);
//            }
//            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//        }

//        // POST: SupplyOffers/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            SupplyOffer supplyOffer = db.SupplyOffers.Find(id);
//            db.SupplyOffers.Remove(supplyOffer);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
