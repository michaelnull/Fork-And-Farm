using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForkAndFarm.Models
{
    public class ForkAndFarmItem
    {
        public int Id { get; set; }
        [DisplayName("Item Name")]
        [MaxLength(25)]
        public string ItemName { get; set; }
    }
    public class ForkAndFarmCategory
    {
        public int Id { get; set; }
        public string ListName { get; set; }
        public List<ForkAndFarmItem> ItemList { get; set; }
    }
}