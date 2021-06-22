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

namespace Tachey001.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private TacheyContext tacheyDb;
        //宣告CourseService
        private consoleService _consoleService;
        //private CourseService _courseService;

        //初始化CourseService
        public MemberController()
        {
            tacheyDb = new TacheyContext();
            _consoleService = new consoleService();
        }
        // GET: Member
        public ActionResult Console()
        {
            var currentId = User.Identity.GetUserId();

            var result = _consoleService.GetConsoleData(currentId);
            //var result = _courseService.GetCourseData(currentId);


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
            return View();
        }

        public ActionResult Setting()
        {
            return View();
        }

        public ActionResult Orders()
        {
            //var currentId = User.Identity.GetUserId();
            //var OrderRecord = from O in tacheyDb.Order
            //                 join OD in tacheyDb.Order_Detail on O.OrderID equals OD.OrderID
            //                 join invoice in tacheyDb.Invoice on O.InvoiceID equals invoice.InvoiceID
            //                 where O.MemberID == currentId
            //                 select new OrderRecord
            //                 {
            //                     OrderDate =O.OrderDate,
            //                     PayDate = O.PayDate,
            //                     PayMethod =O.PayMethod,
            //                     UnitPrice=OD.UnitPrice,
            //                     InvoiceType=invoice.InvoiceType,
            //                     InvoiceName=invoice.InvoiceName,
            //                     InvoiceEmail=invoice.InvoiceEmail,
            //                     InvoiceDate=invoice.InvoiceDate,
            //                     InvoiceNum=invoice.InvoiceNum,
            //                     InvoiceRandomNum=invoice.InvoiceRandomNum
            //                 };

            
            //return View(OrderRecord);
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
            return View();
        }
    }
}