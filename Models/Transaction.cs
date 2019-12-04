using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace BlueberrySwap.Models
{


    [Table("Transaction")]
    public partial class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(name: "transaction_id")]
        public int TransactionId { get; set; }

        [Column(name: "offer_id")]
        public int OfferId { get; set; }

        [Column(name: "accepted")]
        public bool Accepted { get; set; }

        [Required]
        [Column(name: "rejection_reason")]
        public string RejectionReason { get; set; }

        [Column(name: "created_at")]
        public DateTime CreatedAt { get; set; }

        [Column(name: "updated_at")]
        public DateTime UpdatedAt { get; set; }

        public virtual Offer Offer { get; set; }
    }
}
