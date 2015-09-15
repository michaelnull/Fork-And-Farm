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
                deal.IsNew = true;
                offeree.CountNewResponses++;

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

        public ActionResult SetOld(int? id)
        {
            if (id == null)
            {
                return Content("error, id not sent");
            }
            Deal deal = db.Deals.FirstOrDefault(x => x.Id == id);
            if (deal == null)
            {
                return Content("error, response not found in database");
            }
            deal.IsNew = false;
            db.SaveChanges();
            return Content("Deal viewed");
        }

        [Authorize]
        public ActionResult ClearResponseCount()
        {
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            currentuser.CountNewResponses = 0;
            db.SaveChanges();
            return Content("new responses counter reset to zero");
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
            if (deal.IsNew)
            {
                offeree.CountNewResponses--;
            }
            currentuser.DealsToMe.Remove(deal);
            db.Deals.Remove(deal);
            db.SaveChanges();
            return Content("response successfully deleted");
        }

        [Authorize]
        public ActionResult DealsToMe()
        {
            var currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            return Json(currentuser.DealsToMe.OrderByDescending(x => x.CreatedOn), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult DealsFromMe()
        {
            var currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            return Json(currentuser.DealsFromMe.OrderByDescending(x => x.CreatedOn), JsonRequestBehavior.AllowGet);
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
