using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Tachey001.Models;
using Tachey001.Service;
using Tachey001.ViewModel;
using System.Collections;

namespace Tachey001.Controllers
{
    [Authorize]
    public class MemberController : Controller
    { 
        private TacheyContext _context;
        
        private TacheyContext tacheyDb;
        //宣告CourseService
        private CourseService _courseService;

        //初始化CourseService
        public MemberController()
        {
            tacheyDb = new TacheyContext();
            _courseService = new CourseService();
            _context = new TacheyContext();
        }
        // GET: Member
        public ActionResult Console()
        {
            var currentId = User.Identity.GetUserId();

            var result = _courseService.GetMemberCreateCourse(currentId);

            return View(result);
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

                if (result.Profession != null)
                {
                    ViewBag.profession = result.Profession.Split('/');
                }
                else
                {
                    ViewBag.profession = result.Profession;
                }
                var jobList = new List<string>()
                {
                    "藝文設計", "出版業", "金融業", "製造業", "資訊科技", "營建工程", "科技業", "廣告傳播", "服務業", "家管", "自由業", "職業軍人", "公務人員", "教學專業", "法律、社會及文化專業", "醫療", "退休", "學生", "非營利組織", "其他"
                };

                ViewBag.jobList = jobList;

                if (result.Like != null)
                {
                    ViewBag.like = result.Like.Split('/');
                }
                else
                {
                    ViewBag.like = result.Like;
                }
                var likeList = new List<string>()
                {
                    "旅行旅遊", "運動健身", "瑜珈", "桌遊", "棋類遊戲", "插花", "素描", "插畫", "水彩", "速寫", "手寫字", "書法", "電腦繪圖", "手作", "寫作", "社會服務", "電玩", "手遊", "電影", "電視劇", "舞台劇", "舞蹈", "閱讀", "狗派", "占卜", "美容妝髮", "區塊鏈", "金融理財", "運動賽事", "政治經濟"
                };

                ViewBag.likeList = likeList;

                if (result.Interest != null)
                {
                    ViewBag.interestSp = result.Interest.Split('/');
                }
                else
                {
                    ViewBag.interestSp = new ArrayList();
                }
                //var interestList = new List<string>()
                //{
                //    "音樂", "語言", "攝影", "藝術", "設計", "人文", "行銷", "程式", "投資理財", "職場技能", "手作", "生活品味"
                //};
                //ViewBag.interestList = interestList;
                
                
                var courseCategory = _context.CourseCategory.Select(x => x);
                Dictionary<string, string> interestDic = new Dictionary<string, string>();
                foreach (var group in courseCategory)
                {
                    interestDic.Add(group.CategoryID.ToString(), group.CategoryName);
                }
                ViewBag.interest = _context.CourseCategory.Select(x => x.CategoryName);

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


                var selectListYear = new List<SelectListItem>()
                {
                    new SelectListItem {Text="2021", Value="2021" },
                    new SelectListItem {Text="2020", Value="2020" },
                    new SelectListItem {Text="2019", Value="2019" },
                    new SelectListItem {Text="2018", Value="2018" },
                    new SelectListItem {Text="2017", Value="2017" },
                    new SelectListItem {Text="2016", Value="2016" },
                    new SelectListItem {Text="2015", Value="2015" },
                    new SelectListItem {Text="2014", Value="2014" },
                    new SelectListItem {Text="2013", Value="2013" },
                    new SelectListItem {Text="2012", Value="2012" },
                    new SelectListItem {Text="2011", Value="2011" },
                    new SelectListItem {Text="2010", Value="2010" },
                    new SelectListItem {Text="2009", Value="2009" },
                    new SelectListItem {Text="2008", Value="2008" },
                    new SelectListItem {Text="2007", Value="2007" },
                    new SelectListItem {Text="2006", Value="2006" },
                    new SelectListItem {Text="2005", Value="2005" },
                    new SelectListItem {Text="2004", Value="2004" },
                    new SelectListItem {Text="2003", Value="2003" },
                    new SelectListItem {Text="2002", Value="2002" },
                    new SelectListItem {Text="2001", Value="2001" },
                    new SelectListItem {Text="2000", Value="2000" },
                    new SelectListItem {Text="1999", Value="1999" },
                    new SelectListItem {Text="1998", Value="1998" },
                    new SelectListItem {Text="1997", Value="1997" },
                    new SelectListItem {Text="1996", Value="1996" },
                    new SelectListItem {Text="1995", Value="1995" },
                    new SelectListItem {Text="1994", Value="1994" },
                    new SelectListItem {Text="1993", Value="1993" },
                    new SelectListItem {Text="1992", Value="1992" },
                    new SelectListItem {Text="1991", Value="1991" },
                    new SelectListItem {Text="1990", Value="1990" },
                    new SelectListItem {Text="1989", Value="1989" },
                    new SelectListItem {Text="1988", Value="1988" },
                    new SelectListItem {Text="1987", Value="1987" },
                    new SelectListItem {Text="1986", Value="1986" },
                    new SelectListItem {Text="1985", Value="1985" },
                    new SelectListItem {Text="1984", Value="1984" },
                    new SelectListItem {Text="1983", Value="1983" },
                    new SelectListItem {Text="1982", Value="1982" },
                    new SelectListItem {Text="1981", Value="1981" },
                    new SelectListItem {Text="1980", Value="1980" },
                    new SelectListItem {Text="1979", Value="1979" },
                    new SelectListItem {Text="1978", Value="1978" },
                    new SelectListItem {Text="1977", Value="1977" },
                    new SelectListItem {Text="1976", Value="1976" },
                    new SelectListItem {Text="1975", Value="1975" },
                    new SelectListItem {Text="1974", Value="1974" },
                    new SelectListItem {Text="1973", Value="1973" },
                    new SelectListItem {Text="1972", Value="1972" },
                    new SelectListItem {Text="1971", Value="1971" },
                    new SelectListItem {Text="1970", Value="1970" },
                };

                var selectListMonth = new List<SelectListItem>()
                {
                    new SelectListItem {Text="1", Value="1" },
                    new SelectListItem {Text="2", Value="2" },
                    new SelectListItem {Text="3", Value="3" },
                    new SelectListItem {Text="4", Value="4" },
                    new SelectListItem {Text="5", Value="5" },
                    new SelectListItem {Text="6", Value="6" },
                    new SelectListItem {Text="7", Value="7" },
                    new SelectListItem {Text="8", Value="8" },
                    new SelectListItem {Text="9", Value="9" },
                    new SelectListItem {Text="10", Value="10" },
                    new SelectListItem {Text="11", Value="11" },
                    new SelectListItem {Text="12", Value="12" },
                };

                var selectListDay = new List<SelectListItem>()
                {
                    new SelectListItem {Text="1", Value="1" },
                    new SelectListItem {Text="2", Value="2" },
                    new SelectListItem {Text="3", Value="3" },
                    new SelectListItem {Text="4", Value="4" },
                    new SelectListItem {Text="5", Value="5" },
                    new SelectListItem {Text="6", Value="6" },
                    new SelectListItem {Text="7", Value="7" },
                    new SelectListItem {Text="8", Value="8" },
                    new SelectListItem {Text="9", Value="9" },
                    new SelectListItem {Text="10", Value="10" },
                    new SelectListItem {Text="11", Value="11" },
                    new SelectListItem {Text="12", Value="12" },
                    new SelectListItem {Text="13", Value="13" },
                    new SelectListItem {Text="14", Value="14" },
                    new SelectListItem {Text="15", Value="15" },
                    new SelectListItem {Text="16", Value="16" },
                    new SelectListItem {Text="17", Value="17" },
                    new SelectListItem {Text="18", Value="18" },
                    new SelectListItem {Text="19", Value="19" },
                    new SelectListItem {Text="20", Value="20" },
                    new SelectListItem {Text="21", Value="21" },
                    new SelectListItem {Text="22", Value="22" },
                    new SelectListItem {Text="23", Value="23" },
                    new SelectListItem {Text="24", Value="24" },
                    new SelectListItem {Text="25", Value="25" },
                    new SelectListItem {Text="26", Value="26" },
                    new SelectListItem {Text="27", Value="27" },
                    new SelectListItem {Text="28", Value="28" },
                    new SelectListItem {Text="29", Value="29" },
                    new SelectListItem {Text="30", Value="30" },
                    new SelectListItem {Text="31", Value="31" },
                };

                //預設選擇哪一筆
                //selectList.Where(q => q.Value == "男").First().Selected = true;
                if (result.Birthday != null)
                {
                    DateTime date = DateTime.Parse(result.Birthday.ToString());
                    selectListYear.Where(q => q.Value == date.ToString().Split('/')[0]).First().Selected = true;
                    selectListMonth.Where(q => q.Value == date.ToString().Split('/')[1]).First().Selected = true;
                    selectListDay.Where(q => q.Value == date.ToString().Split('/')[2].Split(' ')[0]).First().Selected = true;
                }

                

                ViewBag.SelectListYear = selectListYear;
                ViewBag.SelectListMonth = selectListMonth;
                ViewBag.SelectListDay = selectListDay;

                //context.SaveChanges();
            }

            

            return View();
        }

        [HttpPost]
        public ActionResult SettingIndex(FormCollection frmcol, string Sex)
        {
            var year = frmcol["year"]; //get value
            var month = frmcol["month"]; //get value
            var date = frmcol["date"]; //get value

            var UserId = User.Identity.GetUserId();

            var result = _context.Member.Find(UserId);

            result.Birthday = Convert.ToDateTime(date + "/" + month + "/" + year);
            result.Sex = Sex;

            _context.SaveChanges();

            return RedirectToAction("Setting", "Member");
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
                //result.Introduction = Introduction;
                //result.Introduction = Introduction;
                //result.Introduction = Introduction;
                //result.Introduction = Introduction;
                //result.Introduction = Introduction;
                //result.Introduction = Introduction;

                _context.SaveChanges();

            }

            return RedirectToAction("Profile", "Member");
        }

        [HttpPost]
        public ActionResult SettingJob(string clickedOption)
        {
            
                //int personId = person.PersonId;
                //string name = person.Name;
                //string gender = person.Gender;
                //string city = person.City;

                var UserId = User.Identity.GetUserId();

                var result = _context.Member.Find(UserId); // not member

                result.Profession = clickedOption;
                //result.Description = Description;
                //result.TitlePageImageURL = TitlePageImageURL;
                //result.Introduction = Introduction;
                //result.Introduction = Introduction;
                //result.Introduction = Introduction;
                //result.Introduction = Introduction;
                //result.Introduction = Introduction;
                //result.Introduction = Introduction;

            _context.SaveChanges();

            return RedirectToAction("Setting", "Member");
        }

        [HttpPost]
        public ActionResult SettingLike(string clickedOption)
        {

            //int personId = person.PersonId;
            //string name = person.Name;
            //string gender = person.Gender;
            //string city = person.City;

            var UserId = User.Identity.GetUserId();

            var result = _context.Member.Find(UserId); // not member

            result.Like = clickedOption;
            //result.Description = Description;
            //result.TitlePageImageURL = TitlePageImageURL;
            //result.Introduction = Introduction;
            //result.Introduction = Introduction;
            //result.Introduction = Introduction;
            //result.Introduction = Introduction;
            //result.Introduction = Introduction;
            //result.Introduction = Introduction;

            _context.SaveChanges();

            return RedirectToAction("Setting", "Member");
        }

        [HttpPost]
        public ActionResult SettingInterval(string clickedOption)
        {

            //int personId = person.PersonId;
            //string name = person.Name;
            //string gender = person.Gender;
            //string city = person.City;

            var UserId = User.Identity.GetUserId();

            var result = _context.Member.Find(UserId); // not member

            result.Interest = clickedOption;
            //result.Description = Description;
            //result.TitlePageImageURL = TitlePageImageURL;
            //result.Introduction = Introduction;
            //result.Introduction = Introduction;
            //result.Introduction = Introduction;
            //result.Introduction = Introduction;
            //result.Introduction = Introduction;
            //result.Introduction = Introduction;

            _context.SaveChanges();

            return RedirectToAction("Setting", "Member");
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

            // 課程var courseList = tacheyDb.Course.Where(x => x.MemberID == currentId).Select(x => x).ToList();
            ViewBag.courseGive = (from p in _context.Course where p.MemberID == UserId select p).ToList(); // 開課
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

            var personalUrl = from p in _context.PersonalUrl
                              join m in _context.Member on p.MemberID equals m.MemberID
                              where p.MemberID == UserId
                              select new PersonalUrlView
                              {
                                  FbUrl = p.FbUrl,
                                  GoogleUrl = p.GoogleUrl,
                                  YouTubeUrl = p.YouTubeUrl,
                                  BehanceUrl = p.BehanceUrl,
                                  BlogUrl = p.BlogUrl,
                                  PinterestUrl = p.PinterestUrl,
                                  Name = m.Name
                              };

            ViewBag.UserPhoto = tacheyDb.Member.Find(UserId).Photo;

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