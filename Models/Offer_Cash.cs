using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueberrySwap.Models
{


    public partial class Offer_Cash
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(name: "offer_cash_id")]
        public int OfferCashId { get; set; }

        [Column(name: "cash_value")]
        [DisplayName("Cash Offer")]
        public double CashValue { get; set; }

        [Column(name: "created_at")]
        public DateTime CreatedAt { get; set; }

        [Column(name: "updated_at")]
        [DisplayName("Offered At")]
        public DateTime UpdatedAt { get; set; }
        [DisplayName("Unit")]
        public int? Unit_id { get; set; }

        [NotMapped]
        [DisplayName("Unit")]
        public string UnitName { get; set; }

        [Column("offer_id")]
        public int OfferId { get; set; }

        [Required]
        public virtual Offer Offer { get; set; }
    }
}
