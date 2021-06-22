using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Tachey001.Models;

namespace Tachey001.Repository
{
    public class TacheyRepository
    {
        private DbContext _context;
        public TacheyRepository(DbContext context)
        {
            _context = context;
        }
        public void Create<T>(T value) where T : class
        {
            _context.Entry(value).State = EntityState.Added;
        }
        public void Update<T>(T value) where T : class
        {
            _context.Entry(value).State = EntityState.Modified;
        }
        public void Delete<T>(T value) where T : class
        {
            _context.Entry(value).State = EntityState.Deleted;
        }
        public void DeleteRange<T>(IEnumerable<T> value) where T : class
        {
            foreach (var item in value)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
        }
        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }
        public IQueryable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>();
        }
        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().Where(predicate);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}