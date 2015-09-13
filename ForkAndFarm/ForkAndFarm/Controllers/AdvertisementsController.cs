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

        // GET: Advertisements
        public ActionResult Index(string id)
        {
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (currentuser != null)
            {
                ViewBag.User = currentuser.UserName;
                ViewBag.Role = currentuser.UserRole.ToString();
            }
            
            if (id == null || id == "")
            {
                return View(db.Advertisements.ToList());
            }
            return View(db.Advertisements.Where(x => x.Product.Contains(id)).OrderByDescending(x=>x.CreatedOn).ToList());
           
        }

        public ActionResult SupplyAds(string id)
        {
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            ViewBag.User = currentuser.UserName;
            ViewBag.Role = currentuser.UserRole.ToString();
            List<Advertisement> list = new List<Advertisement>();
            if(id == null || id == "")
            {
                list = db.Advertisements.Where(x => x.AdType == AdType.SupplyOffer).
                    OrderByDescending(x => x.CreatedOn).
                    ToList();
            }
            else
            {
                list = db.Advertisements.Where(x => x.AdType == AdType.SupplyOffer).
                    Where(x => x.Product.Contains(id)).
                    OrderByDescending(x => x.CreatedOn).
                    ToList();
            }
            return View(list);
        }
        public ActionResult PurchaseAds(string id)
        {
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            ViewBag.User = currentuser.UserName;
            ViewBag.Role = currentuser.UserRole.ToString();
            List<Advertisement> list = new List<Advertisement>();
            if (id == null || id == "")
            {
                list = db.Advertisements.Where(x => x.AdType == AdType.PurchaseOffer).
                    OrderByDescending(x => x.CreatedOn).
                    ToList();
            }
            else
            {
                list = db.Advertisements.Where(x => x.AdType == AdType.PurchaseOffer).
                    Where(x => x.Product.Contains(id)).
                    OrderByDescending(x => x.CreatedOn).
                    ToList();
            }
            return View(list);
        }

        // GET: Advertisements/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            ViewBag.UserRole = currentuser.UserRole.ToString();
            ViewBag.User = currentuser.UserName;
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // GET: Advertisements/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Advertisements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Invoice,AdType,ProposedBy,Product,Unit,Quantity,UnitPrice,ExtPrice,Delivery,PaymentTerms,CreatedOn,Memo,ProposedByOrganization,ProposedByPhone")] Advertisement advertisement)
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
            if (ModelState.IsValid)
            {
                currentuser.MyAdvertisements.Add(advertisement);
                db.Advertisements.Add(advertisement);
                db.SaveChanges();
                switch (currentuser.UserRole)
                {
                    case ForkAndFarmUser.Portal.Purchaser:
                        return RedirectToAction("PurchaseAds");

                    case ForkAndFarmUser.Portal.Supplier:
                        return RedirectToAction("SupplyAds");

                    default:
                        return RedirectToAction("Index");
                }
                
            }

            return View(advertisement);
        }

        // GET: Advertisements/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if(advertisement.ProposedBy == currentuser.UserName)
            {
                return View(advertisement);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Advertisements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Invoice,AdType,ProposedBy,Product,Unit,Quantity,UnitPrice,ExtPrice,Delivery,PaymentTerms,CreatedOn,Memo,ProposedByOrganization,ProposedByPhone")] Advertisement advertisement)
        {
            advertisement.ExtPrice = advertisement.Quantity * advertisement.UnitPrice;
            advertisement.Memo = "edited on " + DateTime.Now.ToString() + " " + advertisement.Memo;

            if (ModelState.IsValid)
            {
                db.Entry(advertisement).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("MyAds", "PortalVM");
            }
            return View(advertisement);
        }

        // GET: Advertisements/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }

            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (advertisement.ProposedBy == currentuser.UserName)
            {
                return View(advertisement);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advertisement advertisement = db.Advertisements.Find(id);
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            currentuser.MyAdvertisements.Remove(advertisement);

            db.Advertisements.Remove(advertisement);
            db.SaveChanges();
           
            return RedirectToAction("MyAds", "PortalVM");
        }
        public ActionResult AdList()
        {
            return View();
        }

        public ActionResult AllAds(string id)
        {
            if (id == null || id == "")
            {
                return Json(db.Advertisements.OrderByDescending(x => x.CreatedOn).ToList(), JsonRequestBehavior.AllowGet);
            }
            return Json(db.Advertisements.Where(x => x.Product.Contains(id)).OrderByDescending(x => x.CreatedOn).ToList(), JsonRequestBehavior.AllowGet);

            
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
            var list = db.Advertisements.OrderBy(x => x.Product).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delivery()
        {
            var list = db.Advertisements.OrderBy(x => x.Delivery).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult SearchOrg(string id)
        {
            if (id == null || id == "")
            {
                return Json(db.Advertisements.OrderBy(x => x.ProposedByOrganization).ToList(), JsonRequestBehavior.AllowGet);
            }
            return Json(db.Advertisements.Where(x => x.ProposedByOrganization.Contains(id)).OrderBy(x => x.CreatedOn).ToList(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult MakeOffer(int? id)
        {
            if (id == null)
            {
                return Content("id not received");
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

                return Content("Offer was successfully recorded"+ deal.Id);
            }


            return Content("Offer was not recorded");
        }

        public ActionResult GetUserInfo()
        {
            ForkAndFarmUser currentuser = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            {
                if (currentuser == null)
                    return new EmptyResult();
            }
            return Json(new { UserName = currentuser.UserName, UserRole = currentuser.UserRole.ToString() }, JsonRequestBehavior.AllowGet);
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
                ModelState.AddModelError(String.Empty, err.Message);
            }
           

            return Content("advertisement deleted successfully");
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
