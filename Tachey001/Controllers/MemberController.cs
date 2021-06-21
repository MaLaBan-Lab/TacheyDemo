using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Tachey001.Models;
using Tachey001.Service.Course;
using Tachey001.ViewModel;

namespace Tachey001.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private TacheyContext tacheyDb;
        //宣告CourseService
        private CourseService _courseService;

        //初始化CourseService
        public MemberController()
        {
            tacheyDb = new TacheyContext();
            _courseService = new CourseService();
        }
        // GET: Member
        public ActionResult Console()
        {
            var currentId = User.Identity.GetUserId();

            var result = _courseService.GetMemberCreateCourse(currentId);

            return View(result);
        }
        //刪除課程卡片
        public ActionResult DeleteCourse(string id)
        {
            var result = tacheyDb.Course.Find(id);

            tacheyDb.Course.Remove(result);

            tacheyDb.SaveChanges();

            tacheyDb.Dispose();

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

        public ActionResult Orders( int? type)
        {
            var time = 1;
            GroupOrderRecord AllTypeData = new GroupOrderRecord();
            if (type == null)
                type = 1;
            
            if (type == 1)
            {

                var currentId = User.Identity.GetUserId();
                var OrderRecord = from O in tacheyDb.Order
                                  join OD in tacheyDb.Order_Detail on O.OrderID equals OD.OrderID
                                  join invoice in tacheyDb.Invoice on O.InvoiceID equals invoice.InvoiceID
                                  join Course in tacheyDb.Course on O.CourseID equals Course.CourseID
                                  where O.MemberID == currentId && O.OrderStatus == "success"
                                  select new OrderRecordSuccess
                                  {
                                      CourseName = OD.CourseName,
                                      OrderID = O.OrderID,
                                      TitlePageImageURL = Course.TitlePageImageURL,
                                      OrderDate = O.OrderDate,
                                      PayDate = O.PayDate,
                                      PayMethod = O.PayMethod,
                                      UnitPrice = OD.UnitPrice,
                                      InvoiceType = invoice.InvoiceType,
                                      InvoiceName = invoice.InvoiceName,
                                      InvoiceEmail = invoice.InvoiceEmail,
                                      InvoiceDate = invoice.InvoiceDate,
                                      InvoiceNum = invoice.InvoiceNum,
                                      InvoiceRandomNum = invoice.InvoiceRandomNum,
                                      BuyMethod = OD.BuyMethod
                                  };
                foreach (var item in OrderRecord)
                {
                    time = time + time;
                }

                AllTypeData.Success = OrderRecord;
                if (OrderRecord.FirstOrDefault() == null)
                    ViewBag.id = 0;
                else
                    ViewBag.id = 1;
                
                return View(AllTypeData);
            }
            else if (type == 3)
            {
              
                var currentId = User.Identity.GetUserId();
                var OrderRecord = from O in tacheyDb.Order
                                  join OD in tacheyDb.Order_Detail on O.OrderID equals OD.OrderID
                                 
                                  join Course in tacheyDb.Course on O.CourseID equals Course.CourseID
                                  where O.MemberID == currentId && O.OrderStatus == "error"
                                  select new OrderRecordOther
                                  {
                                      CourseName = OD.CourseName,
                                      OrderID = O.OrderID,
                                      TitlePageImageURL = Course.TitlePageImageURL,
                                      OrderDate = O.OrderDate,
                                      PayDate = O.PayDate,
                                      PayMethod = O.PayMethod,
                                      UnitPrice = OD.UnitPrice,
                                      BuyMethod = OD.BuyMethod
                                  };

                
                AllTypeData.Other = OrderRecord;
                if (OrderRecord.FirstOrDefault() == null)
                    ViewBag.id = 0;
                else
                    ViewBag.id = 3;

                return View(AllTypeData);
            }




            else
            {
                ViewBag.id = 0;
                return View();
            }
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