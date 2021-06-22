using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;

namespace Tachey001.Repository
{
    public class consoleRepository
    {
        private TacheyContext _tacheyContext;

        public consoleRepository()
        {
            _tacheyContext = new TacheyContext();
        }

        // Read 讀取課程表
        public IQueryable<Models.Course> GetAllCourse()
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
        //取得指定課程資料表
        public Models.Course GetCurrentCourse(string currentId)
        {
            var result = _tacheyContext.Course.Find(currentId);

            return result;
        }
        //Delete刪除指定課程資料
        public void DeleteCurrentIdCourseData(string CourseId)
        {
            var result = GetCurrentCourse(CourseId);

            _tacheyContext.Course.Remove(result);

            _tacheyContext.SaveChanges();

            _tacheyContext.Dispose();
        }
    }
}