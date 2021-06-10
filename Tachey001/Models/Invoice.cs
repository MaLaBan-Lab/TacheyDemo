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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InvoiceID { get; set; }

        [Required]
        [StringLength(20)]
        public string InvoiceType { get; set; }

        [Required]
        [StringLength(20)]
        public string InvoiceName { get; set; }

        [Required]
        [StringLength(20)]
        public string InvoiceEmail { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public int? InvoiceNum { get; set; }

        public int? InvoiceRandomNum { get; set; }
    }
}
