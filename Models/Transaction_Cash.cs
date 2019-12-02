namespace BlueberrySwap
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transaction_Cash
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int buyer_id { get; set; }

        public int seller_id { get; set; }

        [Required]
        [StringLength(50)]
        public string seller_item_name { get; set; }

        public int seller_item_id1 { get; set; }

        [Required]
        [StringLength(10)]
        public string seller_item_qty { get; set; }

        public int seller_item_unit { get; set; }

        public int buyer_cash_value { get; set; }

        public DateTime created_at { get; set; }

        public virtual Unit Unit { get; set; }

    }
}
