using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tachey001.ViewModel
{
    public class Cart_GroupViewModel
    {//大籃子裝三個小包
        public List<CartPartialCardViewModel> cartpartialViewModels { get; set; }
        public List<consoleViewModel> courseCardViewModels { get; set; }

    }
}