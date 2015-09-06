using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForkAndFarm.Models
{
    public class PortalVM
    {
        public int DealFromMeCount { get; set; }
        public int DealToMeCount { get; set; }
        public List<Deal> DealsFromMe { get; set; }
        public List<Deal> DealsToMe { get; set; }
        public List<PurchaseOffer> MyPurchaseOffers { get; set; }
        public List<SupplyOffer> MySupplyOffers { get; set; }
        public ForkAndFarmUser.Portal UserRole { get; set; }
        public string Organization { get; set; }
    }
}