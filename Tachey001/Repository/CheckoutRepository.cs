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
        
        public IQueryable<Member> GetMember()
        {
            var result = _tacheyContext.Member;
            return result;
        }


    }

}