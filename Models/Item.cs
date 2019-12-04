using System.ComponentModel;

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
        public string Name { get; set; }

        public double Price { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public DateTime created_at { get; set; }

        [DisplayName("Last Updated")]
        public DateTime updated_at { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        [DisplayName("Unit")]
        public int UnitID { get; set; }
        public virtual Unit Unit { get; set; }

        
        public virtual ApplicationUser Author { get; set; }

    }
}
