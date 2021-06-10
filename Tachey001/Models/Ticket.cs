namespace Tachey001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ticket")]
    public partial class Ticket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TicketID { get; set; }

        [Required]
        [StringLength(80)]
        public string TicketName { get; set; }

        [Required]
        [StringLength(5)]
        public string TiketStatus { get; set; }

        public decimal Discount { get; set; }

        public DateTime Ticketdate { get; set; }

        [StringLength(10)]
        public string PayMethod { get; set; }

        [StringLength(20)]
        public string PoductType { get; set; }

        public int? UseTime { get; set; }
    }
}
