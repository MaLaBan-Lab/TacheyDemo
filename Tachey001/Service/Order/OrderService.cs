using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Repository.Order;
using Tachey001.ViewModel;
using System.Data.Entity;
using Tachey001.Models;

namespace Tachey001.Service.Order
{
    public class OrderService
    {
        //宣告資料庫邏輯
        private OrderRepository _orderRepository;
        private TacheyContext _context;
        //初始化資料庫邏輯
        public OrderService()
        {
            _context = new TacheyContext();
            _orderRepository = new OrderRepository();
        }


        public IEnumerable<OrderRecordSuccess> GetOrderSuccess(string currentID)
        {
            
            var order = _orderRepository.GetAllOrder();
            var order_detail = _orderRepository.GetAllOrder_Detail();
            var invoice = _orderRepository.GetAllInvoice();
            var course = _orderRepository.GetAlLCourse();

            var result = from O in order
                              join OD in order_detail on O.OrderID equals OD.OrderID
                              join _invoice in invoice on O.OrderID equals _invoice.OrderID
                              join _course in course on O.CourseID equals _course.CourseID
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

            var order = _orderRepository.GetAllOrder();
            var order_detail = _orderRepository.GetAllOrder_Detail();
            var invoice = _orderRepository.GetAllInvoice();
            var course = _orderRepository.GetAlLCourse();

            var result = from O in order
                         join OD in order_detail on O.OrderID equals OD.OrderID
                         join _invoice in invoice on O.OrderID equals _invoice.OrderID
                         join _course in course on O.CourseID equals _course.CourseID
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
            var order = _orderRepository.GetAllOrder();
            var order_detail = _orderRepository.GetAllOrder_Detail();
            var course = _orderRepository.GetAlLCourse();
            var result = from O in order
                         join OD in order_detail on O.OrderID equals OD.OrderID
                         join _course in course on O.CourseID equals _course.CourseID
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
                             BuyMethod = OD.BuyMethod
                         };

            return result;
        }

        public void DeleteOrder( String orderID)
        {
            var target = _context.Order.Find(orderID);
            _context.Order.Remove(target);

            _context.SaveChanges();
        }
        public void DeleteOrderDetail(String orderID)
        {
            var target = _context.Order_Detail.Find(orderID);
            _context.Order_Detail.Remove(target);

            _context.SaveChanges();
        }
        public void DeleteInvoice(String orderID)
        {
            var target = _context.Invoice.Find(orderID);
            _context.Invoice.Remove(target);

            _context.SaveChanges();
        }




    }
}