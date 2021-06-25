using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.ViewModel;

namespace Tachey001.ViewModel
{
    public class CartPartialCardViewModel
    {
        public string Title { get; set; }
        public string TitlePageImageURL { get; set; }

        public bool? CreateVerify { get; set; }
        public decimal? OriginalPrice { get; set; }
        public string CourseID { get; set; }
    }
}