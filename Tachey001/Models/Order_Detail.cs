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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        public int CourseID { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [Required]
        [StringLength(200)]
        public string CourseName { get; set; }
    }
}
