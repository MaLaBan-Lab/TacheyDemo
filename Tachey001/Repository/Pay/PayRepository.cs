using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;

namespace Tachey001.Repository.Pay
{
    public class PayRepository
    {
        private TacheyContext _context;

        public PayRepository()
        {
            _context = new TacheyContext();
        }


        public IQueryable<Models.Order> GetAllOrder()
        {
            var result = _context.Order;

            return result;
        }
        public IQueryable<Order_Detail> GetAllOrder_Detail()
        {
            var result = _context.Order_Detail;

            return result;
        }
        public IQueryable<ShoppingCart> GetAllShoppingCart()
        {
            var result = _context.ShoppingCart;

            return result;
        }
        public IQueryable<Models.Course> GetAllCourse()
        {
            var result = _context.Course;

            return result;
        }

        public IQueryable<Ticket> GetAllTicket()
        {
            var result = _context.Ticket;

            return result;
        }
        public IQueryable<TicketOwner> GetAllTicketOwner()
        {
            var result = _context.TicketOwner;

            return result;
        }
        public IQueryable<Point> GetAllPoint()
        {
            var result = _context.Point;

            return result;
        }
    }
}