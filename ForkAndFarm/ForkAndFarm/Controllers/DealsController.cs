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
    public class DealsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Deals/Create
        [Authorize]
        public ActionResult ProposePurchase(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index","SupplyOffers");
            }
            Advertisement supplyoffer = db.Advertisements.FirstOrDefault(x => x.Id == id);
            Deal deal = new Deal();
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (currentuser.UserRole == ForkAndFarmUser.Portal.Purchaser)
            {
                deal.ProposedBy = currentuser.UserName;
                deal.ProposedByOrganization = currentuser.Organization;
                deal.ProposedByPhone = currentuser.Phone;
                deal.Delivery = supplyoffer.Delivery;
                deal.PaymentTerms = supplyoffer.PaymentTerms;
                deal.Product = supplyoffer.Product;
                deal.Quantity = supplyoffer.Quantity;
                deal.Unit = supplyoffer.Unit;
                deal.UnitPrice = supplyoffer.UnitPrice;
                deal.OfferedTo = supplyoffer.ProposedBy;
                deal.OfferId = supplyoffer.Id;
                deal.ExtPrice = supplyoffer.UnitPrice * supplyoffer.Quantity;
                return View(deal);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
        }

        // POST: Deals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProposePurchase(Deal deal)
        {
            var supplyoffer = db.Advertisements.FirstOrDefault(x => x.Id == deal.OfferId);
            if (supplyoffer == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var offeree = db.Users.FirstOrDefault(x=>x.UserName == supplyoffer.ProposedBy);

            var currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            
            deal.CreatedOn = DateTime.Now;
            deal.ExtPrice = deal.Quantity * deal.UnitPrice;
            
            if (ModelState.IsValid)
            {
                db.Deals.Add(deal);

                currentuser.DealsFromMe.Add(deal);
                offeree.DealsToMe.Add(deal);
                supplyoffer.ResponseToAdvertisement.Add(deal);
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
            ViewBag.User = User.Identity.Name;
            if (ViewBag.Title == "ShowProposedSaleDeals")
            {
                var offer = db.Advertisements.FirstOrDefault(x => x.Id == id);
                return View(offer.ResponseToAdvertisement.OrderByDescending(x => x.CreatedOn));
            }
            else
            {
                var offer = db.Advertisements.FirstOrDefault(x => x.Id == id);
                return View(offer.ResponseToAdvertisement.OrderByDescending(x => x.CreatedOn));
            }
           

           
            
        }
        // GET: Deals/Create
        [Authorize]
        public ActionResult ProposeSale(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "PurchaseOffers");
            }
            Advertisement purchaseoffer = db.Advertisements.FirstOrDefault(x => x.Id == id);
            Deal deal = new Deal();
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if(currentuser.UserRole == ForkAndFarmUser.Portal.Supplier)
            {
                deal.ProposedBy = currentuser.UserName;
                deal.ProposedByOrganization = currentuser.Organization;
                deal.ProposedByPhone = currentuser.Phone;
                deal.Delivery = purchaseoffer.Delivery;
                deal.PaymentTerms = purchaseoffer.PaymentTerms;
                deal.Product = purchaseoffer.Product;
                deal.Quantity = purchaseoffer.Quantity;
                deal.Unit = purchaseoffer.Unit;
                deal.UnitPrice = purchaseoffer.UnitPrice;
                deal.OfferedTo = purchaseoffer.ProposedBy;
                deal.OfferId = purchaseoffer.Id;
                deal.ExtPrice = purchaseoffer.Quantity * purchaseoffer.UnitPrice;
                return View(deal);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: Deals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProposeSale(Deal deal)
        {
            var purchaseoffer = db.Advertisements.FirstOrDefault(x => x.Id == deal.OfferId);
            if (purchaseoffer == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var offeree = db.Users.FirstOrDefault(x => x.UserName == purchaseoffer.ProposedBy);

            var currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            
            deal.CreatedOn = DateTime.Now;
            deal.ExtPrice = deal.Quantity * deal.UnitPrice;
            if (ModelState.IsValid)
            {
                db.Deals.Add(deal);

                currentuser.DealsFromMe.Add(deal);
                offeree.DealsToMe.Add(deal);
                purchaseoffer.ResponseToAdvertisement.Add(deal);
                db.SaveChanges();

                return RedirectToAction("ShowProposedSaleDeals", new { id = deal.OfferId });
            }


            return View(deal);
        }

        // GET: Deals
        public ActionResult ShowProposedSaleDeals(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var offer = db.Advertisements.FirstOrDefault(x => x.Id == id);

            ViewBag.User = User.Identity.Name;
            return View(offer.ResponseToAdvertisement.OrderByDescending(x => x.CreatedOn));
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
        // GET: Deals/Details/5
        public ActionResult PurchaseDetails(int? id)
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
            var currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (deal.ProposedBy == currentuser.UserName)
            {
                return View(deal);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
        }

        // POST: Deals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Deal deal)
        {
            deal.ExtPrice = deal.UnitPrice * deal.Quantity;
            deal.Memo = "edited on " + DateTime.Now + " " + deal.Memo;
            var offerId = deal.OfferId;
            if (ModelState.IsValid)
            {
                db.Entry(deal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ShowProposedDeals", new { id = offerId });
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
            var currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (deal.ProposedBy == currentuser.UserName)
            {
                return View(deal);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: Deals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deal deal = db.Deals.Find(id);
            db.Deals.Remove(deal);
            db.SaveChanges();
            return RedirectToAction("ShowProposedDeals", new { id = deal.OfferId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Deals/Edit/5
        [Authorize]

        public ActionResult PurchaseEdit(int? id)
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
        public ActionResult PurchaseEdit(Deal deal)
        {
            deal.ExtPrice = deal.UnitPrice * deal.Quantity;
            deal.Memo = "edited on " + DateTime.Now + " " + deal.Memo;
            var offerId = deal.OfferId;
            if (ModelState.IsValid)
            {
                db.Entry(deal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ShowProposedSaleDeals", new { id = offerId });
            }
            return View(deal);
        }

        // GET: Deals/Delete/5
        [Authorize]
        public ActionResult PurchaseDelete(int? id)
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
        [HttpPost, ActionName("PurchaseDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult PurchaseDeleteConfirmed(int id)
        {
            Deal deal = db.Deals.Find(id);
            db.Deals.Remove(deal);
            db.SaveChanges();
            return RedirectToAction("ShowProposedSaleDeals", new { id = deal.OfferId });
        }

       
    }
}
