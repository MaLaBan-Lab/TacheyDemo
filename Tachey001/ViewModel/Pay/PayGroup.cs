using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tachey001.ViewModel.Pay
{
    public class PayGroup
    {
        public CheckoutViewModel checkoutViewModel { get; set; }
        public IEnumerable<DiscountCard> discountCards { get; set; }

    }
}