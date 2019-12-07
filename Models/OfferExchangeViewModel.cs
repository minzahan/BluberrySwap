using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueberrySwap.Models
{
    public class OfferExchangeViewModel
    {
        public int ExchangeItemId { get; set; }

        public double ExchangeItemQty { get; set; }

        public int ExchangeItemUnitId { get; set; }

        public string OfferedItemDescription { get; set; }

        public int OfferedItemId { get; set; }
        
    }
}