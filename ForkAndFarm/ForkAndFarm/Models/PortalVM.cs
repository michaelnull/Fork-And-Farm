using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForkAndFarm.Models
{
    public class PortalVM
    {
        public string UserName { get; set; }
        public int DealFromMeCount { get; set; }
        public int DealToMeCount { get; set; }
        public int AdCount { get; set; }
        public string UserRole { get; set; }
        public string Organization { get; set; }
        public string Phone { get; set; }
        public int CountNewResponses { get; set; }
    }
}