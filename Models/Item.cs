namespace BlueberrySwap
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using BlueberrySwap.Models;

    [Table("Item")]
    public partial class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public double price { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }

        public DateTime created_at { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] updated_at { get; set; }

        public int CategoryID { get; set; }

        public Category Category { get; set; }

        public int UnitID { get; set; }
        public Unit Unit { get; set; }

        public int AuthorID { get; set; }
        public ApplicationUser Author { get; set; }

    }
}
