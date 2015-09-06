using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForkAndFarm.Models
{
    public abstract class Offer
    {
        private double total;

        public int Id { get; set; }

        public string ProposedBy { get; set; }

        [MaxLength(20)]
        public string Product { get; set; }

        [MaxLength(10)]
        public string Unit { get; set; }

        public double Quantity { get; set; }

        [DisplayName("Unit Price")]
        [DisplayFormat(DataFormatString ="{0:C}")]
        public double UnitPrice { get; set; }

        [DisplayName("Extended Price")]
        [DisplayFormat(DataFormatString ="{0:C}")]
        public double ExtPrice
        {
            get; set;
        }

        [DisplayName("Delivery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required]
        public DateTime? Delivery { get; set; }

        [MaxLength(10)]
        [DisplayName("Payment Terms")]
        public string PaymentTerms { get; set; }

        [DisplayName("Date Offer Created")]
        public DateTime? CreatedOn { get; set; }

        [MaxLength(150)]
        public string Memo { get; set; }
        
        [DisplayName("Proposer's Organization")]
        public string ProposedByOrganization { get; set; }

        [DisplayName("Proposer's Contact Number")]
        public string ProposedByPhone { get; set; }

    }
    public class PurchaseOffer : Offer
    {
        [DisplayName("Purchaser Purchase Order")]
        public string PurchaseOrder { get; set; }

        public virtual ICollection<Deal>ResponsesToPurchaseOffer { get; set; }


    }
    public class SupplyOffer : Offer
    {
        [DisplayName("Seller Invoice")]
        public string Invoice { get; set; }
        
       public virtual ICollection<Deal>ResponsesToSupplyOffer { get; set; }



    }
    public class Deal : Offer
    {
       

        [DisplayName("Offered To")]
        public string OfferedTo { get; set; }

      
     

        public int OfferId { get; set; }

        
    }
}