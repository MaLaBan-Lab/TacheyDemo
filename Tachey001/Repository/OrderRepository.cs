using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;
using Tachey001.ViewModel;
using Microsoft.AspNet.Identity;

namespace Tachey001.Repository.Order
{
    public class OrderRepository
    {
        private TacheyContext _context;

        public OrderRepository()
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
        public  IQueryable<Invoice> GetAllInvoice()
        {
            var result = _context.Invoice;

            return result;
        }
        public IQueryable<Models.Course> GetAlLCourse()
        {
            var result = _context.Course;

            return result;
        }
        public System.Data.Entity.DbSet<Models.Order> GetAllOrderDelete()
        {
            var result = _context.Order;

            return result;
        }
    }
}