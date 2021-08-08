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
        //最新排序(預設)
        [AllowAnonymous]
        public ActionResult All(int page = 1)
        {
            var MemberId = User.Identity.GetUserId();
            ViewBag.UserId = MemberId;
            var all = _consoleService.GetCardsPageList(page);
            var owner = _consoleService.GetOwners(MemberId);

            var result = new consoleallViewModel()
            {
                pageConsole = all,
                GetOwners = owner
            };

            return View(result);
        }
        //最新排序(預設)
        [AllowAnonymous]
        [HttpPost]
        public ActionResult All(string Search, int page = 1)
        {
            var MemberId = User.Identity.GetUserId();
            ViewBag.UserId = MemberId;
            var all = _consoleService.Search(Search, page);
            var owner = _consoleService.GetOwners(MemberId);

            var result = new consoleallViewModel()
            {
                pageConsole = all,
                GetOwners = owner
            };

            return View(result);
        }
        [AllowAnonymous]
        public ActionResult FindCategory(string category)
        {
            int categoryid = _consoleService.ReturnCategoryId(category);
            int detailid = _consoleService.ReturnDetailId(category);
            if (categoryid == 0 && detailid == 0)
            {
                return RedirectToAction("All");
            }
            else
            {
                if(categoryid != 0)
                {
                    var cname = tacheyDb.CourseCategory.FirstOrDefault(x => x.CategoryID == categoryid);
                    ViewBag.categoryname = cname.CategoryName;
                    ViewBag.categoryEname = cname.CategoryEngName;
                    ViewBag.CategoryId = cname.CategoryID;
                }
                if (detailid != 0)
                {
                    var dname = tacheyDb.CategoryDetail.FirstOrDefault(x => x.DetailID == detailid);
                    ViewBag.detailname = dname.DetailName;
                    ViewBag.categoryname = tacheyDb.CourseCategory.FirstOrDefault(x => x.CategoryID == dname.CategoryID).CategoryName;
                    ViewBag.categoryEname = tacheyDb.CourseCategory.FirstOrDefault(x => x.CategoryID == dname.CategoryID).CategoryEngName;
                    ViewBag.CategoryId = dname.CategoryID;
                }

                var MemberId = User.Identity.GetUserId();
                ViewBag.UserId = MemberId;
                var all = _consoleService.GetGroupData(categoryid, detailid);
                var owner = _consoleService.GetOwners(MemberId);
                var result = new consoleallViewModel()
                {
                    consoleViews = all,
                    GetOwners = owner
                };

                return View(result);
            }
        }
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
            return RedirectToAction("Main", "Courses", new { id= id });
        }
        //課程影片頁面
        [AllowAnonymous]
        public ActionResult Main(string id, string type)
        {
            var CourseId = _courseService.GetCourseId(id);

            if(id == "Index")
            {
                return RedirectToAction("Index", "Home");
            }
            if (CourseId == "Index")
            {
                if (_courseService.CheckCourseId(id))
                {
                    CourseId = id;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            var MemberId = User.Identity.GetUserId();
            var YnN = _courseService.Scored(MemberId, CourseId);
            var video = _courseService.GetCourseVideoData(CourseId);
            var allScore = _courseService.GetAllScore(CourseId);
            var allQuestion = _courseService.GetAllQuestions(MemberId, CourseId);
            var isown = _courseService.GetOwner(MemberId, CourseId);
            var isCarted = _courseService.GetCarted(MemberId, CourseId);
            var CourseScore = new CourseScore();
            var Question = new Question();

            //從主頁進入，點擊率+1
            _courseService.AddMainClick(CourseId);

            ViewBag.type = type;
            ViewBag.YnN = YnN;
            ViewBag.MemberId = MemberId;
            ViewBag.UserId = MemberId;
            ViewBag.CourseId = CourseId;

            var result = new MainGroup()
            {
                Main_Video = video,
                GetCourseScore = allScore,
                GetQuestions = allQuestion,
                PostCourseScore = CourseScore,
                PostCourseQuestion = Question,
                GetOwner = isown,
                GetCarted = isCarted
            };

            return View(result);
        }
        [AllowAnonymous]
        public ActionResult LockPage()
        {
            return View();
        }
        //開課10步驟 GET
        public ActionResult Step(string CourseId)
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
        public JsonResult Step(int? id, StepGroup group, FormCollection formCollection)
        {
            try
            {
                _courseService.UpdateStep(id, group, formCollection, group.course.CourseID);
                var result = new ApiResult(ApiStatus.Success, group.course.CourseID, null);
                return Json(result);
            }
            catch (Exception ex)
            {
                var result = new ApiResult(ApiStatus.Fail, "儲存失敗", null);
                return Json(result);
            }
        }
        [HttpGet]
        public ActionResult StepCheck(string CourseId)
        {
            var result = _courseService.GetStepGroup(CourseId);

            return PartialView("_StepCheck", result);
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
            return RedirectToAction("Step", "Courses", new {CourseId = returnCourseId });
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

            var Score = _courseService.GetAllScore(CourseId).First(x=>x.MemberID==MemberID);

            return PartialView("CourseScoreCard", Score);
        }
        [HttpPost]
        //課程發問 POST
        public ActionResult CreateQuestion(MainGroup mainGroup, string CourseId)
        {
            var MemberID = User.Identity.GetUserId();

            _courseService.CreateQuestion(mainGroup.PostCourseQuestion, CourseId, MemberID);

            var Question = _courseService.GetAllQuestions(MemberID, CourseId).Last(x=>x.MemberID == MemberID);

            return PartialView("_QuestionPartial", Question);
        }
        [HttpPost]
        //課程發問 回答 POST
        public ActionResult CreateAnswer(QuestionCard questionCard, string CourseId, int QuestionId)
        {
            var MemberID = User.Identity.GetUserId();

            _courseService.CreateAnswer(questionCard, CourseId, QuestionId, MemberID);

            var Ans = _courseService.GetAllQuestions(MemberID, CourseId)
                .First(x => x.CourseID == CourseId && x.QuestionID == QuestionId).GetAnswerCards
                .Last(x=>x.MemberID == MemberID);

            return PartialView("_AnswerPartial", Ans);

            //return RedirectToAction("Main", "Courses", new { id = 3, CourseId = CourseId });
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
        //Post上傳課程影片並返回成功訊息
        [HttpPost]
        public JsonResult MainCourseVideoUpload(string CourseID, string UnitId)
        {
            try
            {
                var ReturnUrl = _courseService.PostCourseVideoStorage(CourseID, UnitId, Request.Files[0]);
                var result = new ApiResult(ApiStatus.Success, ReturnUrl, null);
                return Json(result);
            }
            catch (Exception ex)
            {
                var result = new ApiResult(ApiStatus.Fail, ex.Message, null);
                return Json(result);
            }
        }
        //猜你想學
        public ActionResult GuessYouLike(int page = 1)
        {
            var currentId = User.Identity.GetUserId();
            ViewBag.UserId = currentId;

            var result = _consoleService.GuessYouLike(currentId, page);

            return PartialView("PageListCardTemplate", result);
        }
        //熱門排序
        [AllowAnonymous]
        public ActionResult AllHot(int page = 1)
        {
            var result = _consoleService.AllHot(page);

            return PartialView("PageListCardTemplate", result);
        }
        //搜尋
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Search(string search, int page = 1)
        {
            var result = _consoleService.Search(search, page);

            return PartialView("PageListCardTemplate", result);
        }
        //最新排序
        [AllowAnonymous]
        public ActionResult AllNew(int page = 1)
        {
            var result = _consoleService.GetCardsPageList(page);

            return PartialView("PageListCardTemplate", result);
        }
        //最多人數排序
        [AllowAnonymous]
        public ActionResult Orderbypn(int page = 1)
        {
            var result = _consoleService.GetCardsHotPageList(page);

            return PartialView("PageListCardTemplate", result);
        }
        //最常課時
        [AllowAnonymous]
        public ActionResult Orderbyct(int page = 1)
        {
            var result = _consoleService.OrderByTotalTimeOfCourse(page);

            return PartialView("PageListCardTemplate", result);
        }
        //最高評價
        [AllowAnonymous]
        public ActionResult Orderbycs(int page = 1)
        {
            var result = _consoleService.OrderByCourseScore(page);

            return PartialView("PageListCardTemplate", result);
        }
        /// <summary>
        /// 新增課程章節後，更新課程影片
        /// </summary>
        /// <param name="CourseID"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Update_Step_Video(string CourseID)
        {
            ViewBag.CourseId = CourseID;
            var result = _courseService.GetStepGroup(CourseID);
            return PartialView("StepVideo", result);
        }
    }
}