using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Tachey001.Models;
using Tachey001.Service;
using Tachey001.ViewModel.Course;
using PagedList;
using Tachey001.ViewModel;
using Tachey001.ViewModel.Member;
using CloudinaryDotNet;
using Tachey001.AccountModels;
using CloudinaryDotNet.Actions;
using Tachey001.ViewModel.ApiViewModel;

namespace Tachey001.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private TacheyContext tacheyDb;
        //宣告CourseService
        private CourseService _courseService;
        private MemberService _memberService;
        private TacheyContext _context;
        private consoleService _consoleService;
        

        //初始化CourseService
        public CoursesController()
        {
            tacheyDb = new TacheyContext();
            _courseService = new CourseService();
            _consoleService = new consoleService();
            _memberService = new MemberService();
            _context = new TacheyContext();
        }

        [AllowAnonymous]
        //最新排序
        public ActionResult All(int page = 1)
        {
            var result = _consoleService.GetCardsPageList(page);
            
            return View(result);
        }

        //熱門排序
        public ActionResult AllHot(int page = 1)
        {
            var result = _consoleService.GetCardsHotPageList(page);

            return PartialView("PageListCardTemplate", result);
        }
        //最高評價
        public ActionResult Orderbycs(int page = 1)
        {
            var result = _consoleService.OrderByCourseScore( page);

            return View(result);
        }

        //搜尋

        [HttpGet]
        public ActionResult Search(int page = 1)
        {
            var result = _consoleService.GetCardsPageList(page);

            return View(result);
        }
        [HttpPost]
        public ActionResult Search(string search, int page = 1)
        {
            var result = _consoleService.Search(search, page);

            return View(result);
        }


        ////猜你想學
        //public ActionResult GuessYouLike(int page = 1)
        //{
        //    var currentId = User.Identity.GetUserId();
        //    ViewBag.UserId = currentId;

        //    var result = _consoleService.GuessYouLike(currentId, page);

        //    return View(result);
        //}
        //[HttpPost]
        //public ActionResult GuessYouLike(int page = 1)
        //{
        //    var currentId = User.Identity.GetUserId();
        //    ViewBag.UserId = currentId;

        //    var result = _consoleService.GuessYouLike(currentId,page);

        //    return View(result);
        //}


        [AllowAnonymous]
        public ActionResult Group(int? categoryid, int? detailid)
        {
            if (categoryid != null)
            {
                var cname = tacheyDb.CourseCategory.FirstOrDefault(x => x.CategoryID == categoryid);
                ViewBag.categoryname = cname.CategoryName;
                ViewBag.detailname = "所有" + cname.CategoryName;
                ViewBag.CategoryId = cname.CategoryID;
            }

            if (detailid != null)
            {
                var dname = tacheyDb.CategoryDetail.FirstOrDefault(x => x.DetailID == detailid);
                ViewBag.detailname = dname.DetailName;
                ViewBag.categoryname = tacheyDb.CourseCategory.FirstOrDefault(x => x.CategoryID == dname.CategoryID).CategoryName;
                ViewBag.CategoryId = dname.CategoryID;
            }

            var result = _consoleService.GetGroupData(categoryid,detailid);
            return View(result);
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }
        //課程影片自訂Url
        [AllowAnonymous]
        public ActionResult Custom(string id)
        {
            var getCourseId = _courseService.GetCourseId(id);
            
            if(id == "Index" || getCourseId == "Index")
            {
                return RedirectToAction("All", "Courses");
            }
            //從行銷網址進去，客戶點擊+1
            _courseService.AddCustomClick(getCourseId);
            return RedirectToAction("Main", "Courses", new { id=1, CourseId = getCourseId });
        }
        //課程影片頁面
        [AllowAnonymous]
        public ActionResult Main(int? id, string CourseId)
        {
            if (CourseId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var MemberId = User.Identity.GetUserId();
            var YnN = _courseService.Scored(MemberId, CourseId);
            var video = _courseService.GetCourseVideoData(CourseId);
            var allScore = _courseService.GetAllScore(CourseId);
            var allQuestion = _courseService.GetAllQuestions(MemberId, CourseId);
            var isown = _courseService.GetOwner(MemberId, CourseId);
            var CourseScore = new CourseScore();
            var Question = new Question();

            //從主頁進入，點擊率+1
            _courseService.AddMainClick(CourseId);

            if (id == null)
            {
                id = 1;
            }
            ViewBag.Id = id;
            ViewBag.YnN = YnN;
            ViewBag.MemberId = MemberId;

            var result = new MainGroup()
            {
                Main_Video = video,
                GetCourseScore = allScore,
                GetQuestions = allQuestion,
                PostCourseScore = CourseScore,
                PostCourseQuestion = Question,
                GetOwner = isown
            };

            return View(result);
        }
        [AllowAnonymous]
        public ActionResult LockPage()
        {
            return View();
        }
        //開課10步驟 GET
        public ActionResult Step(int? id, string CourseId)
        {
            var UserId = User.Identity.GetUserId();

            var courseCategory = _context.CourseCategory.Select(x => x);
            Dictionary<string, string> interestDic = new Dictionary<string, string>();
            foreach (var group in courseCategory)
            {
                interestDic.Add(group.CategoryID.ToString(), group.CategoryName);
            }
            // all 選項 - 子選項
            Dictionary<string, ArrayList> interestDicSub = new Dictionary<string, ArrayList>();
            ArrayList interestArr = new ArrayList();
            var groups = _context.CategoryDetail.GroupBy(x => x.CategoryID);
            foreach (var group in groups)
            {
                interestArr = new ArrayList();
                foreach (var detail in group)
                {
                    interestArr.Add(detail.DetailName);
                }
                interestDicSub.Add(interestDic[group.Key.ToString()], interestArr);
            }
            ViewBag.interestDetil = interestDicSub;

            var CourseCateDet = _courseService.courseCateDet(CourseId);
            var getmemberviewmodels = _memberService.GetAllMemberData(UserId);
            var getcourseviewmodels = _memberService.GetCourseData();

            //取得當前登入會員ID
            ViewBag.UserId = UserId;
            //取得當前開課步驟
            ViewBag.Id = id;
            //取得當前開課的課程ID
            ViewBag.CourseId = CourseId;
            //取得當前會員頭像
            ViewBag.MemberPhoto = getmemberviewmodels[0].Photo;

            var mem = new MemberGroup
            {
                courseCateDet = CourseCateDet,
                memberViewModels = getmemberviewmodels,
                courseViewModels = getcourseviewmodels,
            };

            var result = _courseService.GetStepGroup(CourseId);
            result.memberGroup = mem;

            return View(result);
        }
        //開課10步驟 POST
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Step(int? id, StepGroup group, FormCollection formCollection, string CourseId)
        {
            if(id != 6)
            {
                _courseService.UpdateStep(id, group, formCollection, CourseId);
            }
            return RedirectToAction("Step", "Courses", new { id = (id + 1), CourseID = CourseId });
        }
        //課程種類Post
        [HttpPost]
        public void CategoryStep(string clickedOption, int? id, string CourseId)
        {
            _courseService.ChangeCategory(clickedOption, CourseId);
        }
        //創新課程，加入課程ID
        public ActionResult NewCourseStep()
        {
            //取得當前會員ID
            var currentUserId = User.Identity.GetUserId();

            //創建課程，並回傳課程ID
            var returnCourseId = _courseService.NewCourseStep(currentUserId);

            //導向開課步驟，並傳入課程ID路由
            return RedirectToAction("Step", "Courses", new { id = 0, CourseId = returnCourseId });
        }
        //完成課程，送出審核
        public ActionResult StepFinish(string CourseId)
        {
            var result = tacheyDb.Course.Find(CourseId);

            result.CreateDate = DateTime.Now;
            result.CreateFinish = true;

            tacheyDb.SaveChanges();

            return RedirectToAction("Console", "Member");
        }
        //課程評價 POST
        [HttpPost]
        public ActionResult CreateScore(MainGroup courseScore, string CourseId)
        {
            var MemberID = User.Identity.GetUserId();

            _courseService.CreateScore(courseScore.PostCourseScore, CourseId, MemberID);

            return RedirectToAction("Main", "Courses", new { id = 2, CourseId = CourseId });
        }
        [HttpPost]
        //課程發問 POST
        public ActionResult CreateQuestion(MainGroup mainGroup, string CourseId)
        {
            var MemberID = User.Identity.GetUserId();

            _courseService.CreateQuestion(mainGroup.PostCourseQuestion, CourseId, MemberID);

            return RedirectToAction("Main", "Courses", new { id = 3, CourseId = CourseId });
        }
        [HttpPost]
        //課程發問 回答 POST
        public ActionResult CreateAnswer(QuestionCard questionCard, string CourseId, int QuestionId)
        {
            var MemberID = User.Identity.GetUserId();

            _courseService.CreateAnswer(questionCard, CourseId, QuestionId, MemberID);

            return RedirectToAction("Main", "Courses", new { id = 3, CourseId = CourseId });
        }
        //Post上傳圖片並返回成功訊息
        [HttpPost]
        public JsonResult CoursePhotoUpload(string CourseID)
        {
            try
            {
                var ReturnUrl = _courseService.PostFileStorage(CourseID, Request.Files[0]);
                var result = new ApiResult(ApiStatus.Success, ReturnUrl, null);
                return Json(result);
            }
            catch (Exception ex)
            {
                var result = new ApiResult(ApiStatus.Fail, ex.Message, null);
                return Json(result);
            }
        }
        //Post上傳影片並返回成功訊息
        [HttpPost]
        public JsonResult CourseVideoUpload(string CourseID)
        {
            try
            {
                var ReturnUrl = _courseService.PostVideoStorage(CourseID, Request.Files[0]);
                var result = new ApiResult(ApiStatus.Success, ReturnUrl, null);
                return Json(result);
            }
            catch (Exception ex)
            {
                var result = new ApiResult(ApiStatus.Fail, ex.Message, null);
                return Json(result);
            }
        }
    }
}