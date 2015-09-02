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

        public Profile ProposedBy { get; set; }

        [MaxLength(20)]
        public string Product { get; set; }

        [MaxLength(10)]
        public string Unit { get; set; }

        public double Quantity { get; set; }

        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }

        [DisplayName("Extended Price")]
        public double ExtPrice
        {
            get
            {
                return total;
            }
            set
            {
                total = Quantity * UnitPrice;
            }
        }

        [DisplayName("Delivery Date")]
        [DataType(DataType.Date)]
        public DateTime Delivery { get; set; }

        [MaxLength(10)]
        [DisplayName("Payment Terms")]
        public string PaymentTerms { get; set; }

        [DisplayName("Date Offer Created")]
        public DateTime CreatedOn { get; set; }

        [MaxLength(50)]
        public string Memo { get; set; }


    }
    public class PurchaseOffer : Offer
    {
        [DisplayName("Purchaser Purchase Order")]
        public string PurchaseOrder { get; set; }

        public int PurchaseOffer_Id { get; set; }


    }
    public class SupplyOffer : Offer
    {
        [DisplayName("Seller Invoice")]
        public string Invoice { get; set; }

        public int SupplyOffer_Id { get; set; }


    }
    public class Deal : Offer
    {
        private bool iscomplete;

        [DisplayName("Accepted By")]
        public Profile AcceptedBy { get; set; }

        [DisplayName("Date Accepted")]
        public DateTime AcceptedOn { get; set; }

        [DisplayName("Acceptance Comments")]
        [MaxLength(50)]
        public string AcceptanceComments { get; set; }

        public bool Complete
        {

            get
            {
                return iscomplete;
            }
            set
            {
                if (this.AcceptedBy != null)
                {
                    iscomplete = true;
                }
                else
                {
                    iscomplete = false;
                }
            }
        }
        public int Deal_Id { get; set; }

        public int ProposedBy_Id { get; set; }
        public int AcceptedBy_Id { get; set; }

    }
}