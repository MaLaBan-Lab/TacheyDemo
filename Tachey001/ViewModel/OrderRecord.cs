using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tachey001.ViewModel
{
    public class OrderRecord
    {
        public DateTime OrderDate { get; set; }
        public DateTime? PayDate { get; set; }
        public string PayMethod { get; set; }
        public decimal UnitPrice { get; set; }
        public string InvoiceType { get; set; }
        public string InvoiceName { get; set; }
        public string InvoiceEmail { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int? InvoiceNum { get; set; }
        public int? InvoiceRandomNum { get; set; }
    }
}