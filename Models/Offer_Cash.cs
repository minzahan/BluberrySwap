using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace BlueberrySwap.Models
{


    public partial class Offer_Cash
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(name: "offer_cash_id")]
        public int OfferCashId { get; set; }

        [Column(name: "cash_value")]
        public double CashValue { get; set; }

        [Column(name: "created_at")]
        public DateTime CreatedAt { get; set; }

        [Column(name: "updated_at")]
        public DateTime UpdatedAt { get; set; }

        public int? Unit_id { get; set; }

        [Column("offer_id")]
        public int OfferId { get; set; }

        [Required]
        public virtual Offer Offer { get; set; }
    }
}
