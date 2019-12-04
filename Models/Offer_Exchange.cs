using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace BlueberrySwap.Models
{


    public partial class Offer_Exchange
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(name: "offer_exchange_id")]
        public int OfferExchangeId { get; set; }

        [Column(name: "created_at")]
        public DateTime CreatedAt { get; set; }

        [Column(name: "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column(name: "exchange_item_id")]
        public int ExchangeItemId { get; set; }

        [Column(name: "exchange_item_qty")]
        public double ExchangeItemQty { get; set; }

        [Column(name: "exchange_item_unit_id")]
        public int ExchangeItemUnitId { get; set; }


        [Column("offer_id")]
        public int OfferId { get; set; }

        [Required]
        public virtual Offer Offer { get; set; }
    }
}
