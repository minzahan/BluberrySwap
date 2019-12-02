namespace BlueberrySwap
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Item")]
    public partial class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public double price { get; set; }

        public int unit { get; set; }

        public int author_id { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }

        public DateTime created_at { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] updated_at { get; set; }

        public int category { get; set; }

        public virtual Category Category1 { get; set; }

        public virtual Unit Unit1 { get; set; }

    }
}
