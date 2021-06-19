using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;

namespace Tachey001.Repository
{
   
    public class MemberRepository
    { 
        //初始化資料庫
        private TacheyContext _tacheyContext;
        public MemberRepository()
        {
            _tacheyContext = new TacheyContext();
        }
        public IQueryable<Models.Course> GetCourses()
        {
            var result = _tacheyContext.Course;
            return result;
        }
        public IQueryable<ShoppingCart> GetShoppingCarts()
        {
            var result = _tacheyContext.ShoppingCart;
            return result;
        }








    }

}