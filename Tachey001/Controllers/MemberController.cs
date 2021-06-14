using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Tachey001.Models;

namespace Tachey001.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private TacheyContext tacheyDb = new TacheyContext();
        private TacheyContext _context;
        public MemberController()
        {
            _context = new TacheyContext();
        }
        // GET: Member
        public ActionResult Console()
        {
            var currentId = User.Identity.GetUserId();

            ViewBag.UserPhoto = tacheyDb.Member.Find(currentId).Photo;

            var courseList = tacheyDb.Course.Where(x => x.MemberID == currentId).Select(x => x).ToList();

            return View(courseList);
        }

        public ActionResult Point()
        {
            var UserId = User.Identity.GetUserId();

            using (TacheyContext context = new TacheyContext())
            {
                var result = context.Member.Find(User.Identity.GetUserId());

                if (result.Point == null)
                {
                    ViewBag.totalPoint = 0;
                }
                else
                {
                    ViewBag.totalPoint = result.Point;
                }
            }

            ViewBag.pointHistoGet = from p in _context.Point where p.MemberID == UserId && p.Status == false select p; // 已獲得
            ViewBag.pointHistoUsed = from p in _context.Point where p.MemberID == UserId && p.Status == true select p; // 已使用

            return View();
        }

        public ActionResult Setting()
        {
            var UserId = User.Identity.GetUserId();

            using (TacheyContext context = new TacheyContext())
            {
                var result = context.Member.Find(User.Identity.GetUserId());

                if (result.Photo != null)
                {
                    ViewBag.photoUrl = result.Photo;
                }
                else
                {
                    ViewBag.photoUrl = "/Assets/img/photo.png";
                }
                if (result.Name != null)
                {
                    ViewBag.name = result.Name;
                }
                else
                {
                    ViewBag.name = "無名氏";
                }

                List<string> linek_lists = new List<string>();

                if (result.Sex == null || result.Birthday == null) 
                {
                    linek_lists.Add("性別生日");
                }
                if (result.Profession == null)
                {
                    linek_lists.Add("職業行業");
                }
                //if (result.EmailStatus == null)
                //{
                //    linek_lists.Add("信箱驗證");
                //}
                if (result.Interest == null)
                {
                    linek_lists.Add("個人興趣");
                }
                if (result.Like == null)
                {
                    linek_lists.Add("個人喜好");
                }
                if (result.CountryRegion == null)
                {
                    linek_lists.Add("所在地區");
                }
                ViewBag.link = linek_lists;

                ViewBag.account = result.Account;
                ViewBag.fb = result.Facebook;
                ViewBag.google = result.Google;
                ViewBag.Line = result.Line;

                ViewBag.email = result.Email;
                ViewBag.emailStatus = result.EmailStatus;
                ViewBag.sex = result.Sex;

                if (result.Birthday != null)
                {
                    DateTime birthday = DateTime.ParseExact(result.Birthday.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    ViewBag.birthday = birthday;
                }
                else
                {
                    ViewBag.birthday = null;
                }
                

                //context.SaveChanges();
            }

            return View();
        }

        [HttpPost]
        public ActionResult Email(string Title, string Description, string TitlePageImageURL, string MarketingImageURL, string CourseId)
        {
            var result = tacheyDb.Course.Find(CourseId);

            result.Title = Title;
            result.Description = Description;
            result.TitlePageImageURL = TitlePageImageURL;
            result.MarketingImageURL = MarketingImageURL;

            tacheyDb.SaveChanges();

            return RedirectToAction("Step", "Courses", new { id = 2, CourseId = CourseId });
        }

        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult Profile()
        {
            var UserId = User.Identity.GetUserId();

            using (TacheyContext context = new TacheyContext())
            {
                var result = context.Member.Find(User.Identity.GetUserId());

                if (result.Photo != null)
                {
                    ViewBag.photoUrl = result.Photo;
                }
                else
                {
                    ViewBag.photoUrl = "/Assets/img/photo.png";
                }
                if (result.Theme != null)
                {
                    ViewBag.theme = result.Theme;
                }
                else
                {
                    ViewBag.theme = "/Assets/img/cover-default.png";
                }
                if (result.Name != null)
                {
                    ViewBag.name = result.Name;
                }
                else
                {
                    ViewBag.name = "無名氏";
                }

                if (result.About != null)
                {
                    ViewBag.about = result.About;
                }
                else
                {
                    ViewBag.about = "簡單介紹一下自己吧！";
                }
                if (result.Expertise != null)
                {
                    ViewBag.expertise = result.Expertise;
                }
                else
                {
                    ViewBag.expertise = "有什麼擅長的事情嗎？";
                }
                if (result.InterestContent != null)
                {
                    ViewBag.interestContent = result.InterestContent;
                }
                else
                {
                    ViewBag.interestContent = "平常喜歡做什麼呢？";
                }
                if (result.Introduction != null)
                {
                    ViewBag.introduction = result.Introduction;
                }
                else
                {
                    ViewBag.introduction = "編輯個人頁面，和大家分享更多精彩故事";
                }
                
            }

            ViewBag.giveCourseCount = from p in _context.Course where p.MemberID == UserId select p; // 已開設
            ViewBag.giveCourseCount = Enumerable.Count(ViewBag.giveCourseCount);
            ViewBag.takeCourseCount = from p in _context.CourseBuyed where p.MemberID == UserId select p; // 已參加
            ViewBag.takeCourseCount = Enumerable.Count(ViewBag.takeCourseCount);

            ViewBag.fbConnection = from p in _context.PersonalUrl where p.MemberID == UserId select p.FbUrl;
            if (Enumerable.Count(ViewBag.fbConnection) == 0)
            {
                ViewBag.fbConnection = "";
            } 
            ViewBag.googleConnection = from p in _context.PersonalUrl where p.MemberID == UserId select p.GoogleUrl;
            if (Enumerable.Count(ViewBag.googleConnection) == 0)
            {
                ViewBag.googleConnection = "";
            }
            ViewBag.ytConnection = from p in _context.PersonalUrl where p.MemberID == UserId select p.YouTubeUrl;
            if (Enumerable.Count(ViewBag.ytConnection) == 0)
            {
                ViewBag.ytConnection = "";
            }
            ViewBag.behanceConnection = from p in _context.PersonalUrl where p.MemberID == UserId select p.BehanceUrl;
            if (Enumerable.Count(ViewBag.behanceConnection) == 0)
            {
                ViewBag.behanceConnection = "";
            }
            ViewBag.pinterestConnection = from p in _context.PersonalUrl where p.MemberID == UserId select p.PinterestUrl;
            if (Enumerable.Count(ViewBag.pinterestConnection) == 0)
            {
                ViewBag.pinterestConnection = "";
            }
            ViewBag.blogConnection = from p in _context.PersonalUrl where p.MemberID == UserId select p.BlogUrl;
            if (Enumerable.Count(ViewBag.blogConnection) == 0)
            {
                ViewBag.blogConnection = "";
            }

            // 課程
            ViewBag.courseGive = from p in _context.Course where p.MemberID == UserId select p; // 開課
            ViewBag.courseTake = from p in _context.CourseBuyed where p.MemberID == UserId select p; // 修課
            ViewBag.courseFavorites = from p in _context.Owner where p.MemberID == UserId select p; // 收藏
            ViewBag.courseWork = from p in _context.Homework where p.MemberID == UserId select p; // 作品


            //var currentId = User.Identity.GetUserId();

            //ViewBag.UserPhoto = tacheyDb.Member.Find(currentId).Photo;

            //var courseList = tacheyDb.Course.Where(x => x.MemberID == currentId).Select(x => x).ToList();

            //return View(courseList);

            return View();
        }

        public ActionResult Coupons()
        {
            return View();
        }

        public ActionResult Invite()
        {
            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }
    }
}