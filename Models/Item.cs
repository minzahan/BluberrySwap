using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace BlueberrySwap.Models
{

    [Table("Item")]
    public partial class Item
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("name")]
        public string Name { get; set; }
        [Column(name: "price")]
        public double Price { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Column(name:"created_at")]
        public DateTime CreatedAt { get; set; }
        [Column(name: "updated_at")]
        public DateTime UpdatedAt { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public int UnitID { get; set; }

        [StringLength(128)]
        public string Author_Id { get; set; }
        public virtual ApplicationUser Author { get; set; }

        public virtual Unit Unit { get; set; }
    }
}
