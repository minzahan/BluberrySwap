using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DisplayName("Offered At")]
        public DateTime UpdatedAt { get; set; }

        [Column(name: "exchange_item_id")]
        [DisplayName("Item")]
        public int ExchangeItemId { get; set; }

        [Column(name: "exchange_item_qty")]
        [DisplayName("Quantity")]

        public double ExchangeItemQty { get; set; }

        [Column(name: "exchange_item_unit_id")]
        [DisplayName("Unit")]
        public int ExchangeItemUnitId { get; set; }

        [NotMapped]
        [DisplayName("Unit")]
        public string ExchangeItemUnitName { get; set; }


        [Column("offer_id")]
        public int OfferId { get; set; }

        [Required]
        public virtual Offer Offer { get; set; }
    }
}
