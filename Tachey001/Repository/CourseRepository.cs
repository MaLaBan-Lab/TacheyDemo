using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;

namespace Tachey001.Repository
{
    public class CourseRepository
    {
        private TacheyContext _tacheyContext;

        public CourseRepository()
        {
            _tacheyContext = new TacheyContext();
        }

        // Read 讀取課程表
        public IQueryable<Course> GetAllCourse()
        {
            var result = _tacheyContext.Course;

            return result;
        }
        // Read 讀取會員表
        public IQueryable<Member> GetAllMember()
        {
            var result = _tacheyContext.Member;

            return result;
        }
    }
}