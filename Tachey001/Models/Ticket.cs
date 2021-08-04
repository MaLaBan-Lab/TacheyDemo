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
        public string TicketID { get; set; }

        [StringLength(128)]
        public string TicketName { get; set; }

        [StringLength(128)]
        public string TicketStatus { get; set; }

        public decimal? Discount { get; set; }

        public DateTime? Ticketdate { get; set; }

        [StringLength(50)]
        public string PayMethod { get; set; }

        [StringLength(50)]
        public string ProductType { get; set; }

        [StringLength(50)]
        public string UseTime { get; set; }

        [StringLength(50)]
        public string Send { get; set; }

        public DateTime? SendDate { get; set; }
    }
}
