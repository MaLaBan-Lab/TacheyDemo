using System;
using System.Collections.Generic;
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
            return View();
        }

        public ActionResult Setting()
        {
            return View();
        }

        public ActionResult Orders()
        {
            var currentId = User.Identity.GetUserId();
            var OrderRecord = from O in tacheyDb.Order
                             join OD in tacheyDb.Order_Detail on O.OrderID equals OD.OrderID
                             join invoice in tacheyDb.Invoice on O.InvoiceID equals invoice.InvoiceID
                             join Course in tacheyDb.Course on OD.CourseID equals Course.CourseID
                             where currentId == O.MemberID
                             select new OrderRecord
                             {
                                 OrderID = O.OrderID,
                                 TitlePageImageURL = Course.TitlePageImageURL,
                                 CourseName = OD.CourseName,
                                 OrderDate =O.OrderDate,
                                 PayDate=O.PayDate,
                                 PayMethod=O.PayMethod,
                                 UnitPrice=OD.UnitPrice,
                                 InvoiceType=invoice.InvoiceType,
                                 InvoiceName=invoice.InvoiceName,
                                 InvoiceEmail=invoice.InvoiceEmail,
                                 InvoiceDate=invoice.InvoiceDate,
                                 InvoiceNum=invoice.InvoiceNum,
                                 InvoiceRandomNum=invoice.InvoiceRandomNum,
                                 BuyMethod = OD.BuyMethod
                             };

            
            return View(OrderRecord);
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