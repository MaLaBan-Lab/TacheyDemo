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
using Tachey001.Service.Course;
using Tachey001.Service.Order;
using Tachey001.ViewModel;
using System.Collections;
using Tachey001.Service.Member;
using Tachey001.ViewModel.Member;
using Tachey001.Repository;

namespace Tachey001.Controllers
{
    [Authorize]
    public class MemberController : Controller
    { 
        private TacheyContext _context;
       
        private TacheyContext tacheyDb;
        //宣告CourseService
        private consoleService _consoleService;
        private CourseService _courseService;
        //宣告OrderService
        private OrderService _orderService;
        private MemberService _memberService;
        private MemberRepository _memberRepository;

        //初始化CourseService
        public MemberController()
        {
            tacheyDb = new TacheyContext();
            _consoleService = new consoleService();
            _courseService = new CourseService();
            _context = new TacheyContext();
           
            //初始化
            _orderService = new OrderService();
            _memberService = new MemberService();
            _memberRepository = new MemberRepository();
        }
        // GET: Member
        public ActionResult Console()
        {
            var currentId = User.Identity.GetUserId();
            ViewBag.UserId = currentId;

            var ConsoleViews = _memberService.GetConsoleData(currentId);
            var AllCourses = _courseService.GetCourseData(currentId);

            var result = new consoleallViewModel
            {
                consoleViews = ConsoleViews,
                allCourses = AllCourses

            };


            return View(result);
        }
        //刪除指定課程卡片
        public ActionResult DeleteCourse(int? id, string CourseId)
        {
            _courseService.DeleteCurrentIdCourseData(CourseId);

            return RedirectToAction("Console", "Member");
        }
        public ActionResult Point()
        {
            var UserId = User.Identity.GetUserId();

            
            // var result = context.Member.Find(User.Identity.GetUserId());
            var getpointviewmodels = _memberService.GetPointData(UserId);
            var getgetpointviewmodels = _memberService.GetGetPoint(UserId);
            var getusedpointviewmodels = _memberService.GetUsedPoint(UserId);
            var result = new MemberGroup
            {
                pointViewModels = getpointviewmodels,
                usedpointViewModels = getusedpointviewmodels,
                getpointViewModels = getgetpointviewmodels,
            };

            foreach (var item in result.pointViewModels)
            {
                if (item.Point == null)
                {
                    ViewBag.totalPoint = 0;
                }
                else
                {
                    ViewBag.totalPoint = item.Point;
                }
            }

            ViewBag.pointHistoGet = getgetpointviewmodels; // 已獲得
            ViewBag.pointHistoUsed = getusedpointviewmodels; // 已使用

            return View();
        }

        public ActionResult Setting()
        {
            var UserId = User.Identity.GetUserId();
            var getmemberviewmodels = _memberService.GetAllMemberData(UserId);
            //var getCourseItem = _memberService.GetCourseItem();

            var result = new MemberGroup
            {
                memberViewModels = getmemberviewmodels,
            };

            // 職業行業選項
            var jobList = new List<string>()
                {
                    "藝文設計", "出版業", "金融業", "製造業", "資訊科技", "營建工程", "科技業", "廣告傳播", "服務業", "家管", "自由業", "職業軍人", "公務人員", "教學專業", "法律、社會及文化專業", "醫療", "退休", "學生", "非營利組織", "其他"
                };
            // 其他個人喜好選項
            var likeList = new List<string>()
                {
                    "旅行旅遊", "運動健身", "瑜珈", "桌遊", "棋類遊戲", "插花", "素描", "插畫", "水彩", "速寫", "手寫字", "書法", "電腦繪圖", "手作", "寫作", "社會服務", "電玩", "手遊", "電影", "電視劇", "舞台劇", "舞蹈", "閱讀", "狗派", "占卜", "美容妝髮", "區塊鏈", "金融理財", "運動賽事", "政治經濟"
                };
            // 生日選項
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

            var selectListArea = new List<SelectListItem>()
                {
                    new SelectListItem {Text="TW 台灣", Value="TW 台灣" },
                    new SelectListItem {Text="HK 香港", Value="HK 香港" },
                    new SelectListItem {Text="MY 馬來西亞", Value="MY 馬來西亞" },
                    new SelectListItem {Text="US 美國", Value="US 美國" },
                };

            var selectListLang = new List<SelectListItem>()
                {
                    new SelectListItem {Text="繁體中文", Value="繁體中文" },
                    new SelectListItem {Text="简体中文", Value="简体中文" },
                };

            foreach (var item in result.memberViewModels)
            {
                // 頭照
                ViewBag.photoUrl = item.Photo;
                
                // 使用者姓名
                ViewBag.name = item.Name;
                
                // program bar
                List<string> linek_lists = new List<string>();

                if (item.Sex == null || item.Birthday == null)
                {
                    linek_lists.Add("性別生日");
                }
                if (item.Profession == null)
                {
                    linek_lists.Add("職業行業");
                }
                if (item.Interest == null)
                {
                    linek_lists.Add("個人興趣");
                }
                if (item.Like == null)
                {
                    linek_lists.Add("個人喜好");
                }
                if (item.CountryRegion == null)
                {
                    linek_lists.Add("所在地區");
                }
                //if (result.EmailStatus == null)
                //{
                //    linek_lists.Add("信箱驗證");
                //}
                ViewBag.link = linek_lists;
                // 電子信箱
                ViewBag.email = item.Email;
                ViewBag.emailStatus = item.EmailStatus;
                // 性別
                ViewBag.sex = item.Sex;
                // 職業行業
                if (item.Profession != null) // 已選
                {
                    ViewBag.profession = item.Profession.Split('/');
                }
                else
                {
                    ViewBag.profession = item.Profession;
                }
                ViewBag.jobList = jobList; // all 選項
                // 其他個人喜好
                if (item.Like != null) // 已選
                {
                    ViewBag.like = item.Like.Split('/');
                }
                else
                {
                    ViewBag.like = item.Like;
                }
                ViewBag.likeList = likeList; // all 選項
                // 個人興趣
                if (item.Interest != null) // 已選
                {
                    ViewBag.interestSp = item.Interest.Split('/');
                }
                else
                {
                    ViewBag.interestSp = new ArrayList();
                }

                // all 選項 - 主選項
                var courseCategory = _context.CourseCategory.Select(x => x);
                Dictionary<string, string> interestDic = new Dictionary<string, string>();
                foreach (var group in courseCategory)
                {
                    interestDic.Add(group.CategoryID.ToString(), group.CategoryName);
                }
                ViewBag.interest = _context.CourseCategory.Select(x => x.CategoryName);
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
                // 生日
                if (item.Birthday != null)
                {
                    DateTime date = DateTime.Parse(item.Birthday.ToString());
                    selectListYear.Where(q => q.Value == date.ToString().Split('/')[0]).First().Selected = true;
                    selectListMonth.Where(q => q.Value == date.ToString().Split('/')[1]).First().Selected = true;
                    selectListDay.Where(q => q.Value == date.ToString().Split('/')[2].Split(' ')[0]).First().Selected = true;
                }
                ViewBag.SelectListYear = selectListYear;
                ViewBag.SelectListMonth = selectListMonth;
                ViewBag.SelectListDay = selectListDay;

                // 所在地區 // 顯示語言
                selectListArea.Where(q => q.Value == item.CountryRegion).First().Selected = true;
                selectListLang.Where(q => q.Value == item.Language).First().Selected = true;
                ViewBag.Lang = selectListLang;
                ViewBag.Area = selectListArea;
            }

            return View(result);
        }

        [HttpPost]
        public ActionResult SettingIndex(FormCollection frmcol, MemberGroup m)
        {
            var year = frmcol["year"]; // get dropdownlist value
            var month = frmcol["month"]; // get dropdownlist value
            var date = frmcol["date"]; // get dropdownlist value

            var UserId = User.Identity.GetUserId();

            var result = _context.Member.Find(UserId);

            result.Birthday = Convert.ToDateTime(date + "/" + month + "/" + year);
            result.Sex = m.member.Sex;

            _context.SaveChanges();

            return RedirectToAction("Setting", "Member");
        }

        [HttpPost]
        public ActionResult SettingComm(FormCollection frmcol, MemberGroup m)
        {
            var area = frmcol["area"]; // get dropdownlist value
            var lang = frmcol["lang"]; // get dropdownlist value

            var UserId = User.Identity.GetUserId();

            var result = _context.Member.Find(UserId);

            result.CountryRegion = area;
            result.Language = lang;

            _context.SaveChanges();

            return RedirectToAction("Setting", "Member");
        }

        [HttpPost]
        public ActionResult ProfileExpertise(MemberGroup m, string submitButton)
        {
            if (submitButton == "Send")
            {
                var UserId = User.Identity.GetUserId();

                var result = _context.Member.Find(UserId);

                result.Expertise = m.member.Expertise;

                _context.SaveChanges();

            }

            return RedirectToAction("Profile", "Member");
        }

        [HttpPost]
        public ActionResult ProfileAbout(MemberGroup m, string submitButton)
        {
            if (submitButton == "Send")
            {
                var UserId = User.Identity.GetUserId();

                var result = _context.Member.Find(UserId);

                result.About = m.member.About;

                _context.SaveChanges();

            }

            return RedirectToAction("Profile", "Member");
        }

        [HttpPost]
        public ActionResult ProfileInterest(MemberGroup m, string submitButton)
        {
            if (submitButton == "Send")
            {
                var UserId = User.Identity.GetUserId();

                var result = _context.Member.Find(UserId);

                result.InterestContent = m.member.Interest;

                _context.SaveChanges();

            }

            return RedirectToAction("Profile", "Member");
        }

        [HttpPost]
        public ActionResult ProfileName(MemberGroup m, string submitButton)
        {
            if (submitButton == "Send")
            {
                var UserId = User.Identity.GetUserId();

                var result = _context.Member.Find(UserId);

                result.Name = m.member.Name;

                _context.SaveChanges();

            }

            return RedirectToAction("Profile", "Member");
        }

        [HttpPost]
        public ActionResult ProfileIntro(MemberGroup m, string submitButton)
        {
            if (submitButton == "Send")
            {
                var UserId = User.Identity.GetUserId();

                var result = _context.Member.Find(UserId);

                result.Introduction = m.member.Introduction;

                _context.SaveChanges();

            }

            return RedirectToAction("Profile", "Member");
        }

        [HttpPost]
        public ActionResult ProfileConnection(MemberGroup m, string submitButton)
        {
            if (submitButton == "Send")
            {
                var UserId = User.Identity.GetUserId();

                //var result = _context.PersonalUrl.Find(UserId); // not member
                var result = (from p in _context.PersonalUrl where p.MemberID == UserId select p).ToList();
                
                foreach (var item in result)
                {
                    item.FbUrl = m.member.FbUrl;
                    item.GoogleUrl = m.member.GoogleUrl;
                    item.YouTubeUrl = m.member.YouTubeUrl;
                    item.BehanceUrl = m.member.BehanceUrl;
                    item.PinterestUrl = m.member.PinterestUrl;
                    item.BlogUrl = m.member.BlogUrl;
                }
                

                _context.SaveChanges();

            }

            return RedirectToAction("Profile", "Member");
        }

        [HttpPost]
        public ActionResult SettingJob(string clickedOption)
        {
            var UserId = User.Identity.GetUserId();

            var result = _context.Member.Find(UserId); // not member

            result.Profession = clickedOption;

            _context.SaveChanges();

            return RedirectToAction("Setting", "Member");
        }

        [HttpPost]
        public ActionResult SettingLike(string clickedOption)
        {
            var UserId = User.Identity.GetUserId();

            var result = _context.Member.Find(UserId); // not member

            result.Like = clickedOption;

            _context.SaveChanges();

            return RedirectToAction("Setting", "Member");
        }

        [HttpPost]
        public ActionResult SettingInterval(string clickedOption)
        {
            var UserId = User.Identity.GetUserId();

            var result = _context.Member.Find(UserId); // not member

            result.Interest = clickedOption;

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

        public ActionResult Orders( int? type)
        {
            GroupOrderRecord AllTypeData = new GroupOrderRecord();
            var currentId = User.Identity.GetUserId();
            if (type == null)
                type = 1;

            if (type == 1)
            {   
                var result = _orderService.GetOrderSuccess(currentId);
                AllTypeData.Success = result;
                if (result.FirstOrDefault() == null)
                    ViewBag.id = 0;
                else
                    ViewBag.id = 1;
                return View(AllTypeData);
            }
            else if (type == 2)
            {
                var result = _orderService.GetOrderWait(currentId);
                AllTypeData.Success = result;
                if (result.FirstOrDefault() == null)
                    ViewBag.id = 0;
                else
                    ViewBag.id = 2;
                return View(AllTypeData);
            }

            else if (type == 3)
            {
                var result = _orderService.GetOrderError(currentId);
                AllTypeData.Other = result;
                if (result.FirstOrDefault() == null)
                    ViewBag.id = 0;
                else
                    ViewBag.id = 3;
        //    var currentId = User.Identity.GetUserId();
        //    var OrderRecord = from O in tacheyDb.Order
        //                     join OD in tacheyDb.Order_Detail on O.OrderID equals OD.OrderID
        //                     join invoice in tacheyDb.Invoice on O.OrderID equals invoice.OrderID
        //                     where O.MemberID == currentId
        //                     select new OrderRecord
        //                     {
        //                         OrderDate =O.OrderDate,
        //                         PayDate=O.PayDate,
        //                         PayMethod=O.PayMethod,
        //                         UnitPrice=OD.UnitPrice,
        //                         InvoiceType=invoice.InvoiceType,
        //                         InvoiceName=invoice.InvoiceName,
        //                         InvoiceEmail=invoice.InvoiceEmail,
        //                         InvoiceDate=invoice.InvoiceDate,
        //                         InvoiceNum=invoice.InvoiceNum,
        //                         InvoiceRandomNum=invoice.InvoiceRandomNum
        //                     };

                return View(AllTypeData);
            }
            else
            {
                ViewBag.id = 0;
                return View();
            }
        }

        public ActionResult DeleteOrder(string cancel)
        {
            _orderService.DeleteInvoice(cancel);
            _orderService.DeleteOrder(cancel);
            _orderService.DeleteOrderDetail(cancel);
            return RedirectToAction("Orders",new { type = 2});
            
            return View();
        }

        public ActionResult Profile()
        {
            var UserId = User.Identity.GetUserId();
            var getmemberviewmodels = _memberService.GetAllMemberData(UserId);
            var result = new MemberGroup
            {
                memberViewModels = getmemberviewmodels,
            };

            foreach (var item in result.memberViewModels)
            {
                if (item.Photo != null)
                {
                    ViewBag.photoUrl = item.Photo;
                }
                else
                {
                    ViewBag.photoUrl = "/Assets/img/photo.png";
                }
                if (item.Theme != null)
                {
                    ViewBag.theme = item.Theme;
                }
                else
                {
                    ViewBag.theme = "/Assets/img/cover-default.png";
                }
                if (item.Name != null)
                {
                    ViewBag.name = item.Name;
                }
                else
                {
                    ViewBag.name = "";
                }

                if (item.About != null)
                {
                    ViewBag.about = item.About;
                }
                else
                {
                    ViewBag.about = "簡單介紹一下自己吧！";
                }
                if (item.Expertise != null)
                {
                    ViewBag.expertise = item.Expertise;
                }
                else
                {
                    ViewBag.expertise = "有什麼擅長的事情嗎？";
                }
                if (item.InterestContent != null)
                {
                    ViewBag.interestContent = item.InterestContent;
                }
                else
                {
                    ViewBag.interestContent = "平常喜歡做什麼呢？";
                }
                if (item.Introduction != null)
                {
                    ViewBag.introduction = item.Introduction;
                }
                else
                {
                    ViewBag.introduction = "編輯個人頁面，和大家分享更多精彩故事";
                }
                ViewBag.fbConnection = item.FbUrl;
                if (ViewBag.fbConnection == null)
                {
                    ViewBag.fbConnection = "";
                }
                ViewBag.googleConnection = item.GoogleUrl;
                if (ViewBag.googleConnection == null)
                {
                    ViewBag.googleConnection = "";
                }
                ViewBag.ytConnection = item.YouTubeUrl;
                if (ViewBag.ytConnection == null)
                {
                    ViewBag.ytConnection = "";
                }
                ViewBag.behanceConnection = item.BehanceUrl;
                if (ViewBag.behanceConnection == null)
                {
                    ViewBag.behanceConnection = "";
                }
                ViewBag.pinterestConnection = item.PinterestUrl;
                if (ViewBag.pinterestConnection == null)
                {
                    ViewBag.pinterestConnection = "";
                }
                ViewBag.blogConnection = item.BlogUrl;
                if (ViewBag.blogConnection == null)
                {
                    ViewBag.blogConnection = "";
                }
            }
            

            ViewBag.giveCourseCount = from p in _context.Course where p.MemberID == UserId select p; // 已開設
            ViewBag.giveCourseCount = Enumerable.Count(ViewBag.giveCourseCount);
            ViewBag.takeCourseCount = from p in _context.CourseBuyed where p.MemberID == UserId select p; // 已參加
            ViewBag.takeCourseCount = Enumerable.Count(ViewBag.takeCourseCount);

            

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

            //var personalUrl = from p in _context.PersonalUrl
            //                  join m in _context.Member on p.MemberID equals m.MemberID
            //                  where p.MemberID == UserId
            //                  select new PersonalUrlView
            //                  {
            //                      FbUrl = p.FbUrl,
            //                      GoogleUrl = p.GoogleUrl,
            //                      YouTubeUrl = p.YouTubeUrl,
            //                      BehanceUrl = p.BehanceUrl,
            //                      BlogUrl = p.BlogUrl,
            //                      PinterestUrl = p.PinterestUrl,
            //                      Name = m.Name
            //                  };

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
            //抓到現在登入狀態的會員ID
            var currentId = User.Identity.GetUserId();
            ViewBag.MemberId = currentId;
            var getcartcardviewmodels = _memberService.GetCartPartialViewModel(currentId);
            var getcoursecardviewmodels = _memberService.GetCourseCardViewModels();
            //資料是複數就要toloist,引用的型別是單數所以無法使用
            //return View(getcoursecardviewmodels);
            var result = new Cart_GroupViewModel
            {
                //跟他說要放甚麼 like select new
                //也可以小括號用.的
                cartpartialViewModels = getcartcardviewmodels,
                courseCardViewModels = getcoursecardviewmodels
            };
            //丟入view
            return View(result);
        }
        //刪除購物車卡片
        public ActionResult DeleteRowCarts(string CourseId,string MemberId)
        {
            try
            {
            _memberRepository.DeleteCurrentIdRowCart(CourseId, MemberId);
                return RedirectToAction("Cart", "Member");

            }
            catch (Exception e)
            {
                return RedirectToAction("Cart", "Member");
            }
        }
        //收藏功能
        public void Owner(string MemberId, string CourseID)
        {
            _memberService.CreateOwner(MemberId, CourseID);
        }
    }
}