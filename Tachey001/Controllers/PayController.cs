using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tachey001.Service;

namespace Tachey001.Controllers
{
    public class PayController : Controller
    {
        private CheckoutService _checkoutService;

        public PayController()
        {
            //初始化
            
            _checkoutService = new CheckoutService();
        }
        // GET: Pay
        public ActionResult check()
        {
            var getcheckoutviewmodels = _checkoutService.GetCheckoutViewModels();
            return View(getcheckoutviewmodels);
        }
    }
}