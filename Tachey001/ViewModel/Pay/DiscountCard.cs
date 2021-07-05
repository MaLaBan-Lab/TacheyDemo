using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tachey001.ViewModel.Pay
{
    public class DiscountCard
    {
       
        public string TicketID { get; set; }

     
        public string TicketName { get; set; }

       
        public string TiketStatus { get; set; }

        public decimal? Discount { get; set; }

        public DateTime? Ticketdate { get; set; }

        
        public string PayMethod { get; set; }

        
        public string PoductType { get; set; }

        public string UseTime { get; set; }
    }
}