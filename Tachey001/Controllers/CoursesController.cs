using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Tachey001.Models;
using Tachey001.Service;
using Tachey001.Service.Course;
using Tachey001.Service.Member;
using Tachey001.ViewModel.Course;
using PagedList;
using Tachey001.ViewModel;
using Tachey001.ViewModel.Member;

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

        private int pageSize = 20;
        [AllowAnonymous]
        public ActionResult All( int page = 1 )
        {
            var result = _consoleService.GetConsoleData();

            int currentPage = page < 1 ? 1 : page;
            var oresult = result.OrderBy(x => x.CourseID);
            var rresult = oresult.ToPagedList(currentPage, pageSize);

            var newresult = new consoleallViewModel
            {
                consoleViews = result,
                pageConsole = rresult
            };

            return View(newresult);
        }

        [AllowAnonymous]
        public ActionResult Group(int? categoryid, int? detailid)
        {

            if (categoryid != null)
            {
                var cname = tacheyDb.CourseCategory.FirstOrDefault(x => x.CategoryID == categoryid);
                ViewBag.categoryname = cname.CategoryName;
                ViewBag.detailname = "所有" + cname.CategoryName;

            }

            if (detailid != null)
            {
                var dname = tacheyDb.CategoryDetail.FirstOrDefault(x => x.DetailID == detailid);
                ViewBag.detailname = dname.DetailName;
                ViewBag.categoryname = tacheyDb.CourseCategory.FirstOrDefault(x => x.CategoryID == dname.CategoryID).CategoryName;
            }

            var result = _consoleService.GetGroupData(categoryid,detailid);
            return View(result);
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
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
            var isown = _courseService.GetOwner(MemberId);

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
                PostCourseScore = new CourseScore(),
                PostCourseQuestion = new Question(),
                GetOwner = isown
            };

            return View(result);
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
            //取得當前登入會員ID
            ViewBag.UserId = UserId;
            //取得當前開課步驟
            ViewBag.Id = id;
            //取得當前開課的課程ID
            ViewBag.CourseId = CourseId;

            var CourseCateDet = _courseService.courseCateDet(CourseId);
            var getmemberviewmodels = _memberService.GetAllMemberData(UserId);
            var getcourseviewmodels = _memberService.GetCourseData();

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
            _courseService.UpdateStep(id, group, formCollection, CourseId);

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
        [HttpPost]
        public ActionResult Step8(string CourseId)
        {
            return RedirectToAction("Step", "Courses", new { id = 8, CourseId = CourseId });
        }
        [HttpPost]
        public ActionResult Step9(string CourseId)
        {
            return RedirectToAction("Step", "Courses", new { id = 9, CourseId = CourseId });
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
    }
}