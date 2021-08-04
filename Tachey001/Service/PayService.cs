using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tachey001.Models;
using Tachey001.Repository;
using Tachey001.ViewModel.ApiViewModel;
using Tachey001.ViewModel.Pay;

namespace Tachey001.Service.Pay
{
    public class PayService
    {
        private TacheyRepository _tacheyRepository;
        private DbContext _context;
        //初始化資料庫邏輯
        public PayService() 
        {
            _tacheyRepository = new TacheyRepository(new TacheyContext());
            _context = _tacheyRepository._context;
        }
        public OperationResult OrderCreate(string currentId, string ticketId, string usepoint)
        {
            decimal? discount = 1;
            var orderID = "Tachey" + new Random().Next(0, 99999).ToString();
            usepoint = usepoint == "use" ? usepoint : null;
            ticketId = ticketId == null || ticketId == "null" ? "0" : ticketId;
            var result = new OperationResult();
            using (var transcation = _context.Database.BeginTransaction())
            {
                try
                {
                    if(ticketId != "0")
                    {
                        discount = _tacheyRepository.Get<Ticket>(x => x.TicketID == ticketId).Discount;
                    }
                    //Create Order 創建Order表
                    DateTime localDate = DateTime.Now;
                    var order_new = new Order
                    {
                        OrderID = orderID,
                        UsePoint = usepoint,
                        TicketID = ticketId,
                        MemberID = currentId,
                        OrderStatus = "wait",
                        OrderDate = localDate,
                        PayMethod = "信用卡",
                        PayDate = null
                    };
                    _tacheyRepository.Create(order_new);
                    _tacheyRepository.SaveChanges();

                    //Create OD 創建Order Detail表
                    var shoppingCart = _tacheyRepository.GetAll<ShoppingCart>(x => x.MemberID == currentId);
                    foreach (var item in shoppingCart)
                    {
                        var course = _tacheyRepository.Get<Course>(x => x.CourseID == item.CourseID);
                        var od = new Order_Detail
                        {
                            OrderID = orderID,
                            CourseID = item.CourseID,
                            UnitPrice = course.OriginalPrice * discount,
                            CourseName = course.Title,
                            BuyMethod = "課程售價"
                        };
                        _tacheyRepository.Create(od);
                    }
                    _tacheyRepository.SaveChanges();

                    //Delete SC 刪除購物車商品
                    var shoppingCarts = _tacheyRepository.GetAll<ShoppingCart>(x => x.MemberID == currentId);
                    _tacheyRepository.DeleteRange(shoppingCarts);
                    _tacheyRepository.SaveChanges();

                    result.IsSuccessful = true;
                    result.Exception = orderID;
                    transcation.Commit();
                }
                catch(Exception ex)
                {
                    result.IsSuccessful = false;
                    result.Exception = ex.Message;
                    transcation.Rollback();
                }
            }
            return result;
        }
        public IQueryable<DiscountCard> GetDiscountCard(string currentId)
        {
            var ticket = _tacheyRepository.GetAll<Ticket>();
            var ticetowner = _tacheyRepository.GetAll<TicketOwner>();
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
            var shoppingCart = _tacheyRepository.GetAll<ShoppingCart>(x => x.MemberID == currentId);
            foreach (var item in shoppingCart)
            {
                var course = _tacheyRepository.Get<Course>(x => x.CourseID == item.CourseID);
                result = result + course.OriginalPrice;
                
            }
            return result;
        }
        public decimal? FindDiscount(string ticketId)
        {
            var ticket = _tacheyRepository.Get<Ticket>(x => x.TicketID == ticketId);
            var result = ticket.Discount;

            return result;
        }
        public int GetOwnerPoint(string currentId)
        {
            var point = _tacheyRepository.GetAll<Point>(x => x.MemberID == currentId);
            var totalPoint = 0;
            foreach (var item in point)
            {
                totalPoint = item.PointNum + totalPoint;
            }

            return totalPoint;
        }
    }
}