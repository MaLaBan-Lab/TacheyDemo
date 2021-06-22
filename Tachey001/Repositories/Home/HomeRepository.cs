
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;

namespace Tachey001.Repository.Home
{
    public class HomeRepository
    {
        //初始化資料庫
        private TacheyContext _tacheyContext;

        public HomeRepository()
        {
            _tacheyContext = new TacheyContext();
        }
        //做方法 拿要的資料表
        public IQueryable<Member> GetMembers()
        {
            var result = _tacheyContext.Member;
            return result;
        }
        public IQueryable<CourseScore> GetCourseScores()
        {
            var result = _tacheyContext.CourseScore;
            return result;
        }
        public IQueryable<Models.Course> GetCourses()
        {
            var result = _tacheyContext.Course;
            return result;
        }

    }
}