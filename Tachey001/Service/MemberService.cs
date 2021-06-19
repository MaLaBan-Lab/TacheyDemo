using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.ViewModel;
using Tachey001.Repository.Home;
using Tachey001.Repository;

namespace Tachey001.Service
{
    public class MemberService
    {
        //跟Repository拿資料
        private HomeRepository _HomeRepository;
        private MemberRepository _MemberRepository;
        //初始化資料庫邏輯
        public MemberService()
        {
            _HomeRepository = new HomeRepository();
            _MemberRepository = new MemberRepository();
        }
        public List<CartPartialCardViewModel> GetCartPartialViewModel(string memberId)
        {
            
            var course = _MemberRepository.GetCourses();
            var shoppingcart = _MemberRepository.GetShoppingCarts();
            var result = from c in course join s in shoppingcart on c.CourseID equals s.CourseID
                         //shoppingCartID=MemberController的Cart裡抓到的會員ID
                         where s.MemberID == memberId
                         select new CartPartialCardViewModel { Title = c.Title, TitlePageImageURL = c.TitlePageImageURL, CreateVerify = c.CreateVerify, OriginalPrice = c.OriginalPrice };
            return result.ToList();
        }

        public List<CourseCardViewModel> GetCourseCardViewModels()
        {
            var member = _HomeRepository.GetMembers();
            var course = _HomeRepository.GetCourses();
            var result = from c in course
                         join m in member on c.MemberID equals m.MemberID
                         select new CourseCardViewModel { Photo = m.Photo, Title = c.Title, TotalMinTime = c.TotalMinTime, OriginalPrice = c.OriginalPrice, TitlePageImageURL = c.TitlePageImageURL };
            return result.ToList();
        }

    }
}