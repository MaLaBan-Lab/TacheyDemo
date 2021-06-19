namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public string OrderID { get; set; }

        [Required]
        [StringLength(128)]
        public string CourseID { get; set; }

        public int TicketID { get; set; }

        public int InvoiceID { get; set; }

        [Required]
        [StringLength(128)]
        public string MemberID { get; set; }

        [Required]
        [StringLength(128)]
        public string OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(20)]
        public string PayMethod { get; set; }

        public DateTime? PayDate { get; set; }
    }
}
