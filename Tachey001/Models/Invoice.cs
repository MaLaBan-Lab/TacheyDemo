namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Invoice")]
    public partial class Invoice
    {
        [Key]
        public string OrderID { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceType { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceName { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceEmail { get; set; }

        public DateTime? InvoiceDate { get; set; }

        [StringLength(50)]
        public string InvoiceNum { get; set; }

        public int? InvoiceRandomNum { get; set; }
    }
}
