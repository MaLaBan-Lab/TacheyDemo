using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Tachey001.Models;
using Tachey001.Service.Course;
using Tachey001.Service.Order;
using Tachey001.ViewModel;

namespace Tachey001.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private TacheyContext tacheyDb;
        //宣告CourseService
        private CourseService _courseService;
        //宣告OrderService
        private OrderService _orderService;

        //初始化CourseService
        public MemberController()
        {
            tacheyDb = new TacheyContext();
            _courseService = new CourseService();
            //初始化
            _orderService = new OrderService();
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