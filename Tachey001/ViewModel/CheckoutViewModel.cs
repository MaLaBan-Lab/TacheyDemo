using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tachey001.ViewModel
{
    public class CheckoutViewModel
    {
        public string InvoiceType { get; set; }
        public string InvoiceName { get; set; }

        public string InvoiceEmail { get; set; }
        public string OrderID { get; set; }
    }
}