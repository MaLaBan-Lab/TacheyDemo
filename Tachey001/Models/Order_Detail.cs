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
        public string OrderID { get; set; }

        [Required]
        [StringLength(128)]
        public string CourseID { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [Required]
        [StringLength(200)]
        public string CourseName { get; set; }

        [Required]
        [StringLength(50)]
        public string BuyMethod { get; set; }
    }
}
