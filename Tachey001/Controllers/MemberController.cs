using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Tachey001.Models;
using Tachey001.ViewModel;

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

                var selectList = new List<SelectListItem>()
                {
                    new SelectListItem {Text="text-1", Value="value-1" },
                    new SelectListItem {Text="text-2", Value="value-2" },
                    new SelectListItem {Text="text-3", Value="value-3" },
                };

                //預設選擇哪一筆
                selectList.Where(q => q.Value == "value-2").First().Selected = true;

                ViewBag.SelectList = selectList;

                //context.SaveChanges();
            }

            return View();
        }

        [HttpPost]
        public ActionResult SettingIndex()
        {
            //int personId = person.PersonId;
            //string name = person.Name;
            //string gender = person.Gender;
            //string city = person.City;

            var UserId = User.Identity.GetUserId();

            var result = _context.Member.Find(UserId);

            //result.Title = Title;
            //result.Description = Description;
            //result.TitlePageImageURL = TitlePageImageURL;
            result.Sex = ViewBag.SelectList;

            _context.SaveChanges();

            return View();
        }

        [HttpPost]
        public ActionResult ProfileExpertise(string Expertise, string submitButton)
        {
            if (submitButton == "Send")
            {
                //int personId = person.PersonId;
                //string name = person.Name;
                //string gender = person.Gender;
                //string city = person.City;

                var UserId = User.Identity.GetUserId();

                var result = _context.Member.Find(UserId);

                //result.Title = Title;
                //result.Description = Description;
                //result.TitlePageImageURL = TitlePageImageURL;
                result.Expertise = Expertise;

                _context.SaveChanges();

            }

            return RedirectToAction("Profile", "Member");
        }

        [HttpPost]
        public ActionResult ProfileAbout(string About, string submitButton)
        {
            if (submitButton == "Send")
            {
                //int personId = person.PersonId;
                //string name = person.Name;
                //string gender = person.Gender;
                //string city = person.City;

                var UserId = User.Identity.GetUserId();

                var result = _context.Member.Find(UserId);

                //result.Title = Title;
                //result.Description = Description;
                //result.TitlePageImageURL = TitlePageImageURL;
                result.About = About;

                _context.SaveChanges();

            }

            return RedirectToAction("Profile", "Member");
        }

        [HttpPost]
        public ActionResult ProfileInterest(string Interest, string submitButton)
        {
            if (submitButton == "Send")
            {
                //int personId = person.PersonId;
                //string name = person.Name;
                //string gender = person.Gender;
                //string city = person.City;

                var UserId = User.Identity.GetUserId();

                var result = _context.Member.Find(UserId);

                //result.Title = Title;
                //result.Description = Description;
                //result.TitlePageImageURL = TitlePageImageURL;
                result.InterestContent = Interest;

                _context.SaveChanges();

            }

            return RedirectToAction("Profile", "Member");
        }

        [HttpPost]
        public ActionResult ProfileName(string Name, string submitButton)
        {
            if (submitButton == "Send")
            {
                //int personId = person.PersonId;
                //string name = person.Name;
                //string gender = person.Gender;
                //string city = person.City;

                var UserId = User.Identity.GetUserId();

                var result = _context.Member.Find(UserId);

                //result.Title = Title;
                //result.Description = Description;
                //result.TitlePageImageURL = TitlePageImageURL;
                result.Name = Name;

                _context.SaveChanges();

            }

            return RedirectToAction("Profile", "Member");
        }

        [HttpPost]
        public ActionResult ProfileIntro(string Introduction, string submitButton)
        {
            if (submitButton == "Send")
            {
                //int personId = person.PersonId;
                //string name = person.Name;
                //string gender = person.Gender;
                //string city = person.City;

                var UserId = User.Identity.GetUserId();

                var result = _context.Member.Find(UserId);

                //result.Title = Title;
                //result.Description = Description;
                //result.TitlePageImageURL = TitlePageImageURL;
                result.Introduction = Introduction;

                _context.SaveChanges();

            }

            return RedirectToAction("Profile", "Member");
        }

        [HttpPost]
        public ActionResult ProfileConnection(string Facebook, string Google, string YouTube, string Behance, string Pinterest, string Blog, string submitButton)
        {
            if (submitButton == "Send")
            {
                //int personId = person.PersonId;
                //string name = person.Name;
                //string gender = person.Gender;
                //string city = person.City;

                var UserId = User.Identity.GetUserId();

                var result = _context.Member.Find(UserId); // not member

                //result.Title = Title;
                //result.Description = Description;
                //result.TitlePageImageURL = TitlePageImageURL;
                result.Introduction = Introduction;
                result.Introduction = Introduction;
                result.Introduction = Introduction;
                result.Introduction = Introduction;
                result.Introduction = Introduction;
                result.Introduction = Introduction;

                _context.SaveChanges();

            }

            return RedirectToAction("Profile", "Member");
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
            var currentId = User.Identity.GetUserId();
            var OrderRecord = from O in tacheyDb.Order
                             join OD in tacheyDb.Order_Detail on O.OrderID equals OD.OrderID
                             join invoice in tacheyDb.Invoice on O.InvoiceID equals invoice.InvoiceID
                             where O.MemberID == currentId
                             select new OrderRecord
                             {
                                 OrderDate =O.OrderDate,
                                 PayDate=O.PayDate,
                                 PayMethod=O.PayMethod,
                                 UnitPrice=OD.UnitPrice,
                                 InvoiceType=invoice.InvoiceType,
                                 InvoiceName=invoice.InvoiceName,
                                 InvoiceEmail=invoice.InvoiceEmail,
                                 InvoiceDate=invoice.InvoiceDate,
                                 InvoiceNum=invoice.InvoiceNum,
                                 InvoiceRandomNum=invoice.InvoiceRandomNum
                             };

            
            return View(OrderRecord);
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

            ViewData["PersonalUrls"] = _context.PersonalUrl;
            ViewData["Members"] = _context.Member;


            //var currentId = User.Identity.GetUserId();

            //ViewBag.UserPhoto = tacheyDb.Member.Find(currentId).Photo;

            //var courseList = tacheyDb.Course.Where(x => x.MemberID == currentId).Select(x => x).ToList();
            //var courseList1 = tacheyDb.Course.Where(x => x.MemberID == currentId).Select(x => x).ToList();

            //return View(courseList, courseList1);

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