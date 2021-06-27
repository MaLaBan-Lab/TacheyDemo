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


        //Delete刪除指定課程資料
        public void DeleteCurrentIdRowCart(string outsideCourseId, string outsideMemberId)
        {
            var search = _tacheyContext.ShoppingCart.FirstOrDefault(x => x.MemberID == outsideMemberId && x.CourseID == outsideCourseId);

            _tacheyContext.ShoppingCart.Remove(search);
            _tacheyContext.SaveChanges();
            _tacheyContext.Dispose();

        }

    }

}