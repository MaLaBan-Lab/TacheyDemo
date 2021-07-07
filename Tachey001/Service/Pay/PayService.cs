using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;
using Tachey001.Repository.Pay;
using Tachey001.ViewModel.Pay;

namespace Tachey001.Service.Pay
{
    public class PayService
    {
        private PayRepository _payRepository;
        private TacheyContext _context;
        //初始化資料庫邏輯
        public PayService()
        {
            _context = new TacheyContext();
            _payRepository = new PayRepository();
        }
        public void CreateOrder(string orderId, string currentId, string payMethod, string ticketId,string usepoint)
        {
            DateTime localDate = DateTime.Now;
            var order_new = new Models.Order
            {
                OrderID = orderId,
                UsePoint = usepoint,
                TicketID = ticketId,
                MemberID = currentId,
                OrderStatus = "wait",
                OrderDate = localDate,
                PayMethod = payMethod,
                PayDate = null
            };
            _context.Order.Add(order_new);
            _context.SaveChanges();
        }
        public void CreateOrder_Detail(string orderId, string currentid)
        {
            var shoppingCart = _payRepository.GetAllShoppingCart().Where(x => x.MemberID == currentid);
            foreach (var item in shoppingCart)
            {
                var course = _payRepository.GetAllCourse().FirstOrDefault(x => x.CourseID == item.CourseID);
                var od = new Order_Detail
                {
                    OrderID = orderId,
                    CourseID = item.CourseID,
                    UnitPrice = course.OriginalPrice,
                    CourseName = course.Title,
                    BuyMethod = "課程售價"
                };
                _context.Order_Detail.Add(od);
                _context.SaveChanges();
            }
        }
        public IQueryable<DiscountCard> GetDiscountCard(string currentId)
        {
            var ticket = _payRepository.GetAllTicket();
            var ticetowner = _payRepository.GetAllTicketOwner();
            var result = from ticketowner in ticetowner
                         join t in ticket on ticketowner.TicketID equals t.TicketID
                         where ticketowner.MemberID == currentId
                         select new DiscountCard
                         {
                             TicketID = t.TicketID,
                             TicketName = t.TicketName,
                             TiketStatus = t.TicketStatus,
                             Discount = t.Discount,
                             Ticketdate = t.Ticketdate,
                             PayMethod = t.PayMethod,
                             PoductType = t.ProductType,
                             UseTime = t.UseTime
                         };


            return result;
        }
        public decimal GetTotalPrice(string currentId)
        {
            decimal result = 0;
            var shoppingCart = _payRepository.GetAllShoppingCart().Where(x => x.MemberID == currentId);
            foreach (var item in shoppingCart)
            {
                var course = _payRepository.GetAllCourse().FirstOrDefault(x => x.CourseID == item.CourseID);
                result = result + course.OriginalPrice;

            }
            return result;
        }


        public decimal? FindDiscount(string ticketId)
        {
            var ticket = _payRepository.GetAllTicket().Where(x => x.TicketID == ticketId).First();
            var result = ticket.Discount;

            return result;
        }

        public int GetOwnerPoint(string currentId)
        {
            var point = _payRepository.GetAllPoint().Where(x => x.MemberID == currentId);
            var totalPoint = 0;
            foreach (var item in point)
            {
                totalPoint = item.PointNum + totalPoint;
            }

            return totalPoint;
        }

    }
}