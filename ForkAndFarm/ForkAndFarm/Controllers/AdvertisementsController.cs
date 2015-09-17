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
    public class AdvertisementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
     
        public ActionResult AdList()
        {
            var Bob = db.Users.FirstOrDefault(x => x.UserName == "bob@buyer.com");
            var Fred = db.Users.FirstOrDefault(x => x.UserName == "fred@farmer.com");
            Bob.CountNewResponses = 5;
            Fred.CountNewResponses = 6;

            foreach (Deal deal in Bob.DealsToMe)
            {
                deal.IsNew = true;
            }
            foreach (Deal deal in Fred.DealsToMe)
            {
                deal.IsNew = true;
            }
            db.SaveChanges();

            return View();
        }

        public ActionResult AllAds(string id)
        {
            if (id == null || id == "")
            {
                var list = from item in db.Advertisements
                           orderby item.CreatedOn descending
                           select new
                           {
                               Id = item.Id,
                               Product = item.Product,
                               Quantity = item.Quantity,
                               Unit = item.Unit,
                               UnitPrice = item.UnitPrice,
                               ExtPrice = item.ExtPrice,
                               Delivery = item.Delivery,
                               ProposedByOrganization = item.ProposedByOrganization,
                               Memo = item.Memo,
                               ProposedByPhone = item.ProposedByPhone,
                               Invoice = item.Invoice,
                               ResponseToAdvertisement = item.ResponseToAdvertisement,
                               ResponseCount = item.ResponseToAdvertisement.Count(),
                               AdType = item.AdType,
                               CreatedOn = item.CreatedOn
                           };

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            var listb = from item in db.Advertisements.Where(x => x.Product.Contains(id))
                        orderby item.CreatedOn descending
                       select new
                       {
                           Id = item.Id,
                           Product = item.Product,
                           Quantity = item.Quantity,
                           Unit = item.Unit,
                           UnitPrice = item.UnitPrice,
                           ExtPrice = item.ExtPrice,
                           Delivery = item.Delivery,
                           ProposedByOrganization = item.ProposedByOrganization,
                           Memo = item.Memo,
                           ProposedByPhone = item.ProposedByPhone,
                           Invoice = item.Invoice,
                           ResponseToAdvertisement = item.ResponseToAdvertisement,
                           ResponseCount = item.ResponseToAdvertisement.Count(),
                           AdType = item.AdType.ToString(),
                           CreatedOn = item.CreatedOn
                       };
            return Json(listb, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SupplyList()
        {
            var list = db.Advertisements.Where(x => x.AdType == AdType.SupplyOffer).OrderByDescending(x => x.CreatedOn).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PurchaseList()
        {
            var list = db.Advertisements.Where(x => x.AdType == AdType.PurchaseOffer).OrderByDescending(x => x.CreatedOn).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

       public ActionResult Products()
        {
            var list = db.Advertisements.OrderBy(x => x.Product).OrderBy(x => x.CreatedOn).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delivery()
        {
            var list = db.Advertisements.OrderBy(x => x.Delivery).OrderBy(x=>x.CreatedOn).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult SearchOrg(string id)
        {
            if (id == null || id == "")
            {
                return Json(db.Advertisements.OrderBy(x => x.ProposedByOrganization).OrderBy(x=>x.CreatedOn).ToList(), JsonRequestBehavior.AllowGet);
            }
            return Json(db.Advertisements.Where(x => x.ProposedByOrganization.Contains(id)).OrderBy(x => x.CreatedOn).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SubmitAd([Bind(Include ="Product, Quantity, Unit, UnitPrice, PaymentTerms, Invoice, Memo, Delivery")]Advertisement advertisement)
        {
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            advertisement.ProposedBy = currentuser.UserName;
            advertisement.ProposedByOrganization = currentuser.Organization;
            advertisement.ProposedByPhone = currentuser.Phone;
            advertisement.CreatedOn = DateTime.Now;
            advertisement.ExtPrice = advertisement.Quantity * advertisement.UnitPrice;

            switch (currentuser.UserRole)
            {
                case ForkAndFarmUser.Portal.Purchaser:
                    advertisement.AdType = AdType.PurchaseOffer;
                    break;

                case ForkAndFarmUser.Portal.Supplier:
                    advertisement.AdType = AdType.SupplyOffer;
                    break;
            }
            try
            {
                if (ModelState.IsValid)
                {
                    currentuser.MyAdvertisements.Add(advertisement);
                    db.Advertisements.Add(advertisement);
                    db.SaveChanges();
                }
            }
            catch (DataException dex)
            {
                return Content(dex.Message);
            }
           

            return Content("advertisement successfully created");
        }

        [Authorize]
        public ActionResult MyAds()
        {
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            return Json(currentuser.MyAdvertisements.OrderByDescending(x => x.CreatedOn), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        //get acton
        public ActionResult DeleteAd(int? id)
        {
            if(id == null)
            {
                return Content("error, id not sent");
            }
           
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return Content("error, advertisement not found in database");
            }
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
           
           
            return Json(advertisement, JsonRequestBehavior.AllowGet);
        }
        //post action
        [HttpPost, ActionName("DeleteAd")]
        [Authorize]
        //due to the many to many relationship, the subordinate deals must be removed from all lists, then removed from the db, then the ad must be removed from the user's list and then removed from the db
        public ActionResult DeleteAdConfirmed(int? id)
        {
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return Content("error, advertisement not found in database");
            }
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (currentuser == null)
            {
                return Content("error, user not found in database");
            }
            if (currentuser.UserName != advertisement.ProposedBy)
            {
                return Content("error, user name does not match user that originally posted the ad");
            }
            var list = advertisement.ResponseToAdvertisement;
            if (list != null)
            { 
                foreach (Deal deal in list)
                {
                    Deal trade = db.Deals.FirstOrDefault(x => x.Id == deal.Id);
                    //get poster of response and remove response from user's list
                    ForkAndFarmUser poster = db.Users.FirstOrDefault(x => x.UserName == trade.ProposedBy);
                    poster.DealsFromMe.Remove(trade);
                    //remove response from list of ad owner's responses
                    currentuser.DealsToMe.Remove(trade);
                  
                }
                //remove trades from database
                db.Deals.RemoveRange(list);
                //remove response from list of responses to ad
                advertisement.ResponseToAdvertisement.Clear();
            }
            //remove ad from list of ads to user
            currentuser.MyAdvertisements.Remove(advertisement);

            db.Advertisements.Remove(advertisement);
            try
            {
                db.SaveChanges();
            }
            catch (Exception err)
            {
                return Content(err.Message);
            }
            return Content("advertisement deleted successfully");
        }

        public ActionResult GetOneAd(int? id)
        {
            List<Advertisement> list = new List<Advertisement>();
            if (id == null)
            {
                list.Add(new Advertisement { Product = "error, Id not received" });
            }
            Advertisement advertisement = db.Advertisements.FirstOrDefault(x => x.Id == id);
            if (advertisement == null)
            {
                list.Add(new Advertisement { Product = "Error, Ad not found" });
            }
            list.Add(advertisement);
            return Json(list, JsonRequestBehavior.AllowGet);
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
