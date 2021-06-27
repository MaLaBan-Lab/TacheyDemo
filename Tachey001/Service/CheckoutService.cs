using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Repository;
using Tachey001.ViewModel;

namespace Tachey001.Service
{
    public class CheckoutService
    {
        private CheckoutRepository _CheckoutRepository;
 public CheckoutService()
    {
        _CheckoutRepository = new CheckoutRepository();
    }
    public CheckoutViewModel GetCheckoutViewModels()
    {
            var invoice = _CheckoutRepository.GetInvoice();
            var result = from i in invoice

                         select new CheckoutViewModel
            {
                InvoiceEmail=i.InvoiceEmail,InvoiceType=i.InvoiceType,InvoiceName=i.InvoiceName,OrderID=i.OrderID
            };
            //要改成IQ型別才不用ToList
            var a = result.FirstOrDefault((x) => x.OrderID == "591db1fc00f58c070078c802");
            return a;
    }
    }
   
}