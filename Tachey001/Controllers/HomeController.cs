using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Tachey001.Models;
using Tachey001.Service.Home;
using Tachey001.Service.Member;
using Tachey001.ViewModel;

namespace Tachey001.Controllers
{
    public class HomeController : Controller
    {
        //要叫Service
        //初始化=隨時要用他都叫得到
        private HomeService _homeService;
        private MemberService _memberService;
        public HomeController()
        {
            _homeService = new HomeService();
            _memberService = new MemberService();
        }
        public ActionResult Index()
        {
            var MemberId = User.Identity.GetUserId();
            ViewBag.UserId = MemberId;
            //var用碗去接我要的東西
            var getcommentviewmodel = _homeService.GetCommentViewModel();
            var getcoursecardviewmodels = _homeService.GetCourseCardViewModels(MemberId);
            var gethighlightcourseviewmodel = _homeService.GetHighlightCourseViewModels();
            var getcartpartialcardviewmodel = _memberService.GetCartPartialViewModel(MemberId);
            //再創一個group viewmodel包裝傳回view  <-規則
            //var result 最大包的
            //要找人的時候都要先初始化他 <-雞蛋糕 ˇ藍圖
            //使用大籃子裝三個碗
            var result = new Card_highlight_Group
            {
                //跟他說要放甚麼 like select new
                //也可以小括號用.的
                highlightViewModels = gethighlightcourseviewmodel,
                commentViewModels = getcommentviewmodel,
                courseCardViewModels = getcoursecardviewmodels,
                cartPartialCardViewModels= getcartpartialcardviewmodel
            };
            //丟入view
            return View(result);
        }
        public ActionResult About()
        {
            ViewBag.Title = "About";
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult CourseCard()
        {
            return View();
        }
    }
}