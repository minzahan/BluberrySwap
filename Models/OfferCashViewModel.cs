using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueberrySwap.Models
{
    public class OfferCashViewModel
    {
        public int OfferCashId { get; set; }
        public string ItemDescription { get; set; }
        public int ItemId { get; set; }

        public double CashValue { get; set; }
        
        public double Quantity { get; set; }

        public int UnitID { get; set; }
    }
}