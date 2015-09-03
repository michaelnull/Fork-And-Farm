using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForkAndFarm.Models
{
    public class DealListVM
    {
        public int Id { get; set; }

        [DisplayName("Date Offer Created")]
        public DateTime CreatedOn { get; set; }

        public string ProposedBy { get; set; }

        public double Quantity { get; set; }

        public string Unit { get; set; }

        public string Product { get; set; }

        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }

        [DisplayName("Extended Price")]
        public double ExtPrice { get; set; }

        [DisplayName("Delivery Date")]
        [DataType(DataType.Date)]
        public DateTime Delivery { get; set; }

        [MaxLength(10)]
        [DisplayName("Payment Terms")]
        public string PaymentTerms { get; set; }

        [MaxLength(50)]
        public string Memo { get; set; }

        [DisplayName("Offer Accepted")]
        public bool IsComplete { get; set; }

        [DisplayName("Accepted By")]
        public string AcceptedBy { get; set; }

        [DisplayName("Date Accepted")]
        public DateTime AcceptedOn { get; set; }

        [DisplayName("Acceptance Comments")]
        [MaxLength(50)]
        public string AcceptanceComments { get; set; }
    }
}