using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tachey001.Models;
using Tachey001.Service;
using Tachey001.Service.Pay;
using Tachey001.ViewModel.Pay;

namespace Tachey001.Controllers
{
    public class PayController : Controller
    {
       
        private CheckoutService _checkoutService;
        private PayService _payService;
        private TacheyContext _context;

        public PayController()
        {
            //初始化
            _context = new TacheyContext();
            _payService = new PayService();
            _checkoutService = new CheckoutService();
        }
        // GET: Pay
        public ActionResult check(string ticketId = null,string usepoint = null)
        {
          //  onclick = "location.href='@Url.Action("Orders", "Member",new { type=1})
            var currentId = User.Identity.GetUserId();
            var getcheckoutviewmodels = _checkoutService.GetCheckoutInformation(currentId);
            ViewBag.ticketId = ticketId;
            var paygroup = new PayGroup();
            paygroup.checkoutViewModel = getcheckoutviewmodels;
            paygroup.discountCards = _payService.GetDiscountCard(currentId);

            ViewBag.totalPrice = _payService.GetTotalPrice(currentId);
            if (ticketId != null)
                ViewBag.discount = _payService.FindDiscount(ticketId);
            else
                ViewBag.discount = 1;

            ViewBag.usePoint = usepoint; 
            ViewBag.totalPoint =  _payService.GetOwnerPoint(currentId);

            return View(paygroup);
        }
        public ActionResult create(string ticketId ,string usepoint)
        {
            var currentId = User.Identity.GetUserId();
            var orderID = "Tachey" + new Random().Next(0, 99999).ToString();
            if (ticketId != null)
                _payService.CreateOrder(orderID, currentId, "信用卡", ticketId, null);
            else if (usepoint == "use")
                _payService.CreateOrder(orderID, currentId, "信用卡", "0", usepoint);
            else
                _payService.CreateOrder(orderID, currentId, "信用卡", "0", null);

            _payService.CreateOrder_Detail(orderID, currentId);
            Session["ID"] = orderID;
            //var test = usepoint;
            return Redirect("~/AioCheckOut.aspx");
            //return View();
        }
        public ActionResult Error()
        {
            //var orderID = Session["ID"].ToString();
            //var od = _context.Order_Detail.Where(x => x.OrderID == orderID);
            //foreach (var item in od)
            //{
            //    var temp = _context.Order_Detail.Find(orderID,item.CourseID);
            //    _context.Order_Detail.Remove(temp);
               
            //}
            //_context.SaveChanges();
            return RedirectToAction("Orders", "Member");
        }
        public ActionResult success()
        {
            var currentId = User.Identity.GetUserId();
            var shoppingCarts = _context.ShoppingCart.Where(x => x.MemberID == currentId);
            foreach (var item in shoppingCarts)
            {
                var temp = _context.ShoppingCart.Find(currentId, item.CourseID);
                _context.ShoppingCart.Remove(temp);

            }
            _context.SaveChanges();
            return RedirectToAction("Orders", "Member");
        }
    }
}