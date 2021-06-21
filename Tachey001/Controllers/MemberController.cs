using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Tachey001.Models;
using Tachey001.Service;
using Tachey001.Service.Course;
using Tachey001.ViewModel;
using Tachey001.Repository;

namespace Tachey001.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private TacheyContext tacheyDb;
        //宣告CourseService
        private CourseService _courseService;
        private MemberService _memberService;
        private MemberRepository _memberRepository;

        //初始化CourseService
        public MemberController()
        {
            tacheyDb = new TacheyContext();
            _courseService = new CourseService();
            _memberService = new MemberService();
            _memberRepository = new MemberRepository();
        }
        // GET: Member
        public ActionResult Console()
        {
            var currentId = User.Identity.GetUserId();

            var result = _courseService.GetCourseData(currentId);

            return View(result);
        }
        //刪除指定課程卡片
        public ActionResult DeleteCourse(string id)
        {
            _courseService.DeleteCurrentIdCourseData(id);

            return RedirectToAction("Console", "Member");
        }
        public ActionResult Point()
        {
            return View();
        }

        public ActionResult Setting()
        {
            return View();
        }

        public ActionResult Orders()
        {
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

            
            return View();
        }

        public ActionResult Profile()
        {
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
    }
}