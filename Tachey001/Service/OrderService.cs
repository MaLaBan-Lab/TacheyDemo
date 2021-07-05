using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.ViewModel;
using System.Data.Entity;
using Tachey001.Models;
using Tachey001.Repository;

namespace Tachey001.Service
{
    public class OrderService
    {
        //宣告資料庫邏輯
        private TacheyRepository _tacheyRepository;
        //初始化資料庫邏輯
        public OrderService()
        {
            _tacheyRepository = new TacheyRepository(new TacheyContext());
        }
        public IEnumerable<OrderRecordSuccess> GetOrderSuccess(string currentID)
        {
            var order = _tacheyRepository.GetAll<Order>();
            var order_detail = _tacheyRepository.GetAll<Order_Detail>();
            var invoice = _tacheyRepository.GetAll<Invoice>();
            var course = _tacheyRepository.GetAll<Course>();

            var result = from O in order
                              join OD in order_detail on O.OrderID equals OD.OrderID
                              join _invoice in invoice on O.OrderID equals _invoice.OrderID
                              join _course in course on OD.CourseID equals _course.CourseID
                              where O.MemberID == currentID && O.OrderStatus == "success"
                              select new OrderRecordSuccess
                              {
                                  CourseName = OD.CourseName,
                                  OrderID = O.OrderID,
                                  TitlePageImageURL = _course.TitlePageImageURL,
                                  OrderDate = O.OrderDate,
                                  PayDate = O.PayDate,
                                  PayMethod = O.PayMethod,
                                  UnitPrice = OD.UnitPrice,
                                  InvoiceType = _invoice.InvoiceType,
                                  InvoiceName = _invoice.InvoiceName,
                                  InvoiceEmail = _invoice.InvoiceEmail,
                                  InvoiceDate = _invoice.InvoiceDate,
                                  InvoiceNum = _invoice.InvoiceNum,
                                  InvoiceRandomNum = _invoice.InvoiceRandomNum,
                                  BuyMethod = OD.BuyMethod
                              };


            return result;
        }

        public IEnumerable<OrderRecordSuccess> GetOrderWait(string currentID)
        {

            var order = _tacheyRepository.GetAll<Order>();
            var order_detail = _tacheyRepository.GetAll<Order_Detail>();
            var invoice = _tacheyRepository.GetAll<Invoice>();
            var course = _tacheyRepository.GetAll<Course>();

            var result = from O in order
                         join OD in order_detail on O.OrderID equals OD.OrderID
                         join _invoice in invoice on O.OrderID equals _invoice.OrderID
                         join _course in course on OD.CourseID equals _course.CourseID
                         where O.MemberID == currentID && O.OrderStatus == "wait"
                         select new OrderRecordSuccess
                         {
                             CourseName = OD.CourseName,
                             OrderID = O.OrderID,
                             TitlePageImageURL = _course.TitlePageImageURL,
                             OrderDate = O.OrderDate,
                             PayDate = O.PayDate,
                             PayMethod = O.PayMethod,
                             UnitPrice = OD.UnitPrice,
                             InvoiceType = _invoice.InvoiceType,
                             InvoiceName = _invoice.InvoiceName,
                             InvoiceEmail = _invoice.InvoiceEmail,
                             InvoiceDate = _invoice.InvoiceDate,
                             InvoiceNum = _invoice.InvoiceNum,
                             InvoiceRandomNum = _invoice.InvoiceRandomNum,
                             BuyMethod = OD.BuyMethod
                         };


            return result;
        }
        public IEnumerable<OrderRecordOther> GetOrderError(string currentID)
        {
            var order = _tacheyRepository.GetAll<Order>();
            var order_detail = _tacheyRepository.GetAll<Order_Detail>();
            var course = _tacheyRepository.GetAll<Course>();
            var result = from O in order
                         join OD in order_detail on O.OrderID equals OD.OrderID
                         join _course in course on OD.CourseID equals _course.CourseID
                         where O.MemberID == currentID && O.OrderStatus == "error"
                         select new OrderRecordOther
                         {
                             CourseName = OD.CourseName,
                             OrderID = O.OrderID,
                             TitlePageImageURL = _course.TitlePageImageURL,
                             OrderDate = O.OrderDate,
                             PayDate = O.PayDate,
                             PayMethod = O.PayMethod,
                             UnitPrice = OD.UnitPrice,
                             BuyMethod = OD.BuyMethod,
                             CourseID = OD.CourseID
                         };

            return result;
        }

        public void DeleteOrder( String orderID)
        {
            var result = _tacheyRepository.Get<Order>(x => x.OrderID == orderID);

            _tacheyRepository.Delete(result);

            _tacheyRepository.SaveChanges();
        }
        public void DeleteOrderDetail(String orderID)
        {
            var result = _tacheyRepository.Get<Order_Detail>(x => x.OrderID == orderID);

            _tacheyRepository.Delete(result);

            _tacheyRepository.SaveChanges();
        }
        public void DeleteInvoice(String orderID)
        {
            var result = _tacheyRepository.Get<Invoice>(x => x.OrderID == orderID);

            _tacheyRepository.Delete(result);

            _tacheyRepository.SaveChanges();
        }
    }
}