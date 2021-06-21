using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;

namespace Tachey001.Repository.Course
{
    public class CourseRepository
    {
        private TacheyContext _tacheyContext;
        public CourseRepository()
        {
            _tacheyContext = new TacheyContext();
        }
        //Read 取得當前課程章節
        public IEnumerable<CourseChapter> GetCurrentCourseChapters(string CourseId)
        {
            var result = _tacheyContext.CourseChapter.Where(x => x.CourseID == CourseId).Select(x => x);

            return result;
        }
        //Read 取得當前課程單元
        public IEnumerable<CourseUnit> GetCourseUnits(string CourseId)
        {
            var result = _tacheyContext.CourseUnit.Where(x => x.CourseID == CourseId).Select(x => x);

            return result;
        }
        //Read 取得課程種類
        public IEnumerable<CourseCategory> GetCourseCategory()
        {
            var result = _tacheyContext.CourseCategory;

            return result;
        }
        //Read 取得課程種類細項
        public IEnumerable<CategoryDetail> GetCategoryDetail()
        {
            var result = _tacheyContext.CategoryDetail;

            return result;
        }
        //Read 讀取課程表
        public IEnumerable<Models.Course> GetAllCourse()
        {
            var result = _tacheyContext.Course;

            return result;
        }
        //取得指定課程資料表
        public Models.Course GetCurrentCourse(string currentId)
        {
            var result = _tacheyContext.Course.Find(currentId);

            return result;
        }
        //Read 讀取會員表
        public IEnumerable<Member> GetAllMember()
        {
            var result = _tacheyContext.Member;

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