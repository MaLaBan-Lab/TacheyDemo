using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Tachey001.AccountModels;
using Tachey001.Service;
using Tachey001.Util;
using Tachey001.ViewModel;

namespace Tachey001.Controllers
{
    public class HomeController : Controller
    {
        //要叫Service
        //初始化=隨時要用他都叫得到
        private HomeService _homeService;
        private MemberService _memberService;
        private consoleService _consoleService;
        public HomeController()
        {
            _homeService = new HomeService();
            _memberService = new MemberService();
            _consoleService = new consoleService();
        }
        public ActionResult Index()
        {
            var MemberId = User.Identity.GetUserId();
            ViewBag.UserId = MemberId;
            //var用碗去接我要的東西
            var getcommentviewmodel = _homeService.GetCommentViewModel();
            //var getcoursecardviewmodels = _homeService.GetCourseCardViewModels(MemberId);
            var getcoursecardviewmodels = _consoleService.GetAllCourses();
            var owner = _consoleService.GetOwners(MemberId);
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
                cartPartialCardViewModels = getcartpartialcardviewmodel,
                consoleViewModels = getcoursecardviewmodels,
                GetOwners = owner
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
            var _cloudinary = Credientials.Init();

            SearchResult result = _cloudinary.Search()
                .Expression("folder=Course/7QmfVoaSH68w/Video")
                .Execute();

            var url = result.Resources.Find(x => x.FileName == "5._電腦如何計算_zlaned").SecureUrl;

            ViewBag.List = result.Resources;
            return View();
        }
        public JsonResult LongRunningProcess()
        {
            //THIS COULD BE SOME LIST OF DATA
            int itemsCount = 50;

            for (int i = 0; i <= itemsCount; i++)
            {
                //SIMULATING SOME TASK
                Thread.Sleep(50);

                //CALLING A FUNCTION THAT CALCULATES PERCENTAGE AND SENDS THE DATA TO THE CLIENT
                //itemsCount 總容量
                //i現在上傳量
                Functions.SendProgress("Process in progress...", i, itemsCount);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetHomePageCard(int start = 0)
        {
            ViewBag.UserId = User.Identity.GetUserId();
            var getcoursecardviewmodels = _consoleService.GetAllCourses();
            var top4 = getcoursecardviewmodels.Skip(start).Take(1);
            consoleViewModel result = null ;
            foreach (var item in top4)
            {
                result = item;
            }
            if (result != null)
                return PartialView("_CourseCardPartial", result);
            else
                return null;
        }
      
    }
}