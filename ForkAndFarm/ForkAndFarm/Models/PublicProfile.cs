using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForkAndFarm.Models
{
    public class Trader
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Organization { get; set; }

        [MaxLength(20)]
        public string Address { get; set; }

        [MaxLength(20)]
        public string Address2 { get; set; }

        [RegularExpression("^\\d{5}(-\\d{4})?$")]
        public string Zip { get; set; }

        [Required]
        public Roles Role { get; set; }

        public enum Roles
        {
            Purchaser,
            Supplier
        
        }
        public List<PurchaseOffer> MyPurchaseOffers { get; set; }
        public List<SupplyOffer> MySupplyOffers { get; set; }
        public List<Deal> ProposedByMeAndOpen { get; set; }
        public List<Deal> ProposedByMeAndComplete { get; set; }
        public List<Deal> AcceptedByMe { get; set; }
    }
}