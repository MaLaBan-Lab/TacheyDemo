using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;

namespace Tachey001.Repository
{

    public class CheckoutRepository
    {
        //初始化資料庫
        private TacheyContext _tacheyContext;
        public CheckoutRepository()
        {
            _tacheyContext = new TacheyContext();
        }
        
        public IQueryable<Invoice> GetInvoice()
        {
            var result = _tacheyContext.Invoice;
            return result;
        }


    }

}