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
        public ActionResult ProposeDeal(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index","Advertisements");
            }
            Advertisement offer = db.Advertisements.FirstOrDefault(x => x.Id == id);
            Deal deal = new Deal();
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if ((currentuser.UserRole == ForkAndFarmUser.Portal.Purchaser && offer.AdType == AdType.SupplyOffer) || 
                (currentuser.UserRole == ForkAndFarmUser.Portal.Supplier && offer.AdType == AdType.PurchaseOffer))
            {
                deal.ProposedBy = currentuser.UserName;
                deal.ProposedByOrganization = currentuser.Organization;
                deal.ProposedByPhone = currentuser.Phone;
                deal.Delivery = offer.Delivery;
                deal.PaymentTerms = offer.PaymentTerms;
                deal.Product = offer.Product;
                deal.Quantity = offer.Quantity;
                deal.Unit = offer.Unit;
                deal.UnitPrice = offer.UnitPrice;
                deal.OfferedTo = offer.ProposedBy;
                deal.OfferId = offer.Id;
                deal.ExtPrice = offer.UnitPrice * offer.Quantity;
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
        public ActionResult ProposeDeal(Deal deal)
        {

            var offeree = db.Users.FirstOrDefault(x => x.UserName == deal.OfferedTo);
            var offer = db.Advertisements.FirstOrDefault(x => x.Id == deal.OfferId);
            var currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            
            deal.CreatedOn = DateTime.Now;
            deal.ExtPrice = deal.Quantity * deal.UnitPrice;
            
            if (ModelState.IsValid)
            {
                db.Deals.Add(deal);

                currentuser.DealsFromMe.Add(deal);
                offeree.DealsToMe.Add(deal);
                offer.ResponseToAdvertisement.Add(deal);
                db.SaveChanges();
                
                return RedirectToAction("ShowProposedDeals", new { id = deal.OfferId });
            }
           

            return View(deal);
        }

        [Authorize]
        public ActionResult MakeOffer(int? id)
        {
            if (id == null)
            {
                return new EmptyResult();
            }
            Advertisement offer = db.Advertisements.FirstOrDefault(x => x.Id == id);
            Deal deal = new Deal();
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if ((currentuser.UserRole == ForkAndFarmUser.Portal.Purchaser && offer.AdType == AdType.SupplyOffer) ||
                (currentuser.UserRole == ForkAndFarmUser.Portal.Supplier && offer.AdType == AdType.PurchaseOffer))
            {
                deal.ProposedBy = currentuser.UserName;
                deal.ProposedByOrganization = currentuser.Organization;
                deal.ProposedByPhone = currentuser.Phone;
                deal.Delivery = offer.Delivery;
                deal.PaymentTerms = offer.PaymentTerms;
                deal.Product = offer.Product;
                deal.Quantity = offer.Quantity;
                deal.Unit = offer.Unit;
                deal.UnitPrice = offer.UnitPrice;
                deal.OfferedTo = offer.ProposedBy;
                deal.OfferId = offer.Id;
                deal.ExtPrice = offer.UnitPrice * offer.Quantity;
                return View(deal);
            }
            else
            {
                return new EmptyResult();
            }

        }

        // POST: Deals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeOffer(Deal deal)
        {

            var offeree = db.Users.FirstOrDefault(x => x.UserName == deal.OfferedTo);
            var offer = db.Advertisements.FirstOrDefault(x => x.Id == deal.OfferId);
            var currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            deal.CreatedOn = DateTime.Now;
            deal.ExtPrice = deal.Quantity * deal.UnitPrice;

            if (ModelState.IsValid)
            {
                db.Deals.Add(deal);

                currentuser.DealsFromMe.Add(deal);
                offeree.DealsToMe.Add(deal);
                offer.ResponseToAdvertisement.Add(deal);
                db.SaveChanges();

                return RedirectToAction("ShowProposedDeals", new { id = deal.OfferId });
            }


            return new EmptyResult();
        }

        // GET: Deals
        public ActionResult ShowProposedDeals(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.User = User.Identity.Name;
           
            var offer = db.Advertisements.FirstOrDefault(x => x.Id == id);
            var list = offer.ResponseToAdvertisement.OrderByDescending(x => x.CreatedOn);
            if (list.Count() > 0)
            {
                return View(list);
            }
            else
            {
                return RedirectToAction("Details", "Advertisements", new { id = offer.Id });
            }
           
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
            int offerid = deal.OfferId;
            db.Deals.Remove(deal);
            db.SaveChanges();
            return RedirectToAction("ShowProposedDeals", new { id = offerid });
        }

        // POST: Deals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        public ActionResult SubmitOffer(Deal deal)
        {
            int id = deal.OfferId;
            
            Advertisement offer = db.Advertisements.FirstOrDefault(x => x.Id == id);
            if (offer == null)
            {
                return Content("could not match to an advertisement");
            }
            var offeree = db.Users.FirstOrDefault(x => x.UserName == offer.ProposedBy);
            var currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if ((currentuser.UserRole == ForkAndFarmUser.Portal.Purchaser && offer.AdType == AdType.SupplyOffer) |
               (currentuser.UserRole == ForkAndFarmUser.Portal.Supplier && offer.AdType == AdType.PurchaseOffer))
            {
                deal.ProposedBy = currentuser.UserName;
                deal.ProposedByOrganization = currentuser.Organization;
                deal.ProposedByPhone = currentuser.Phone;
                deal.Product = offer.Product;
                deal.Unit = offer.Unit;
                deal.OfferedTo = offer.ProposedBy;
                deal.OfferId = offer.Id;
                deal.ExtPrice = deal.UnitPrice * deal.Quantity;
                deal.CreatedOn = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Deals.Add(deal);
                    currentuser.DealsFromMe.Add(deal);
                    offeree.DealsToMe.Add(deal);
                    offer.ResponseToAdvertisement.Add(deal);
                    db.SaveChanges();

                    return Content(String.Format("offer to {0} for {1} {2} of {3} for {4:C}", deal.OfferedTo, deal.Quantity, deal.Unit, deal.Product, deal.ExtPrice));
                }
                return Content("data missing");
            }
            return Content("transaction not allowed");
        }

        public ActionResult GetOffers(int? id)
        {
            if (id == null)
            {
                return Json(new List<Deal> { new Deal { ProposedByOrganization = "error, the advertisement ID was not found!!" } }, JsonRequestBehavior.AllowGet);
            }
            var offer = db.Advertisements.FirstOrDefault(x => x.Id == id);
            if (offer == null)
            {
                return Json(new List<Deal> { new Deal { ProposedByOrganization = "error, the advertisement was not found in the list" } }, JsonRequestBehavior.AllowGet);
            }
            var list = offer.ResponseToAdvertisement.OrderByDescending(x => x.CreatedOn);
            if (list.Count() <= 0)
            {
                return Json(new List<Deal> { new Deal { ProposedByOrganization = "there are no responses to this ad yet" } }, JsonRequestBehavior.AllowGet);
            }
            return Json(list, JsonRequestBehavior.AllowGet);  
        }
        [Authorize]
        public ActionResult DeleteResponse(int? id)
        {
            if (id == null)
            {
                return Content("error, response ID missing");
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return Content("error, response not found in database");
            }
            var currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            return Json(deal, JsonRequestBehavior.AllowGet);  
        }
        [Authorize]
        [HttpPost, ActionName("DeleteResponse")]
        public ActionResult DeleteResponseConfirmed(int? id)
        {

            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return Content("error, response not found in database");
            }
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            ForkAndFarmUser offeree = db.Users.FirstOrDefault(x => x.UserName == deal.OfferedTo);
            if (currentuser == null || currentuser.UserName != deal.ProposedBy)
            {
                return Content("error, user not found in database");
            }
            offeree.DealsToMe.Remove(deal);
            currentuser.DealsToMe.Remove(deal);
            db.Deals.Remove(deal);
            db.SaveChanges();
            return Content("response successfully deleted");

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
