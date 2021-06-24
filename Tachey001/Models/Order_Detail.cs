namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_Detail
    {
        [Key]
        [Column(Order = 0)]
        public string OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string CourseID { get; set; }

        public decimal? UnitPrice { get; set; }

        [StringLength(128)]
        public string CourseName { get; set; }

        [StringLength(128)]
        public string BuyMethod { get; set; }
    }
}
