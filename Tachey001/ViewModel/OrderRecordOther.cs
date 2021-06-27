using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tachey001.ViewModel
{
    public class OrderRecordOther
    {
        public string OrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? PayDate { get; set; }
        public string PayMethod { get; set; }
        public decimal? UnitPrice { get; set; }
       
        public string BuyMethod { get; set; }
        public string TitlePageImageURL { get; set; }
        public string CourseName { get; set; }
    }
}