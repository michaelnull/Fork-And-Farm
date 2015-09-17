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

        [Required]
        public string ProposedBy { get; set; }

        [Required]
        [MaxLength(20)]
        public string Product { get; set; }

        [Required]
        [MaxLength(20)]
        public string Unit { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
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
    public class Advertisement : Offer
    {
        [MaxLength(20)]
        [DisplayName("Invoice or Purchase Order")]
        public string Invoice{ get; set; }

        public AdType AdType { get; set; }

        public virtual ICollection<Deal>ResponseToAdvertisement { get; set; }

    }
  
    public class Deal : Offer
    {
       

        [DisplayName("Offered To")]
        public string OfferedTo { get; set; }

        public int OfferId { get; set; }

        public bool IsNew { get; set; }
        
    }
    public enum AdType
    {
        PurchaseOffer,
        SupplyOffer
    }
}