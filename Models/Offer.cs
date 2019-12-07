using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueberrySwap.Models
{


    [Table("Offer")]
    public partial class Offer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Offer()
        {
            Transactions = new HashSet<Transaction>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(name: "offer_id")]
        public int OfferId { get; set; }

        [Column(name: "item_id")]
        public int ItemId { get; set; }

        [Required]
        [StringLength(128)]
        [Column(name: "offeredby_author_id")]
        [DisplayName("Offered By")]
        public string OfferedByAuthorId { get; set; }

        [NotMapped]
        [DisplayName("Offered By")]
        public string OfferedByName { get; set; }

        [Column(name: "qty")]
        public double Qty { get; set; }

        [Column(name: "created_at")]
        public DateTime CreatedAt { get; set; }

        [Column(name: "updated_at")]
        public DateTime UpdatedAt { get; set; }

      
        public virtual Offer_Cash Offer_Cash { get; set; }

        public virtual Offer_Exchange Offer_Exchange { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
