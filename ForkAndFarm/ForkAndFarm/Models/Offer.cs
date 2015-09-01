using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForkAndFarm.Models
{
    public abstract class Offer
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string Product { get; set; }

        [MaxLength(10)]
        public string Unit { get; set; }

        public double Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double ExtPrice { get; set; }

        public DateTime Delivery { get; set; }

        [MaxLength(10)]
        public string PaymentTerms { get; set; }

        public DateTime CreatedOn { get; set; }
        
        [MaxLength(50)]
        public string Memo { get; set; }
    }
    public class PurchaseOffer : Offer
    {
        public string PurchaseOrder { get; set; }

    }
    public class SupplyOffer : Offer
    {
        public string Invoice { get; set; }
    }
}