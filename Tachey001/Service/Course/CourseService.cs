using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Repository.Course;
using Tachey001.ViewModel.Course;

namespace Tachey001.Service.Course
{
    public class CourseService
    {
        //宣告資料庫邏輯
        private CourseRepository _courseRepository;
        //初始化資料庫邏輯
        public CourseService()
        {
            _courseRepository = new CourseRepository();
        }
        //取得渲染課程卡片所需資料欄位
        public List<AllCourse> GetCourseData()
        {
            var course = _courseRepository.GetAllCourse();
            var member = _courseRepository.GetAllMember();

            var result = from c in course
                              join m in member on c.MemberID equals m.MemberID
                              select new AllCourse
                              {
                                  CourseID = c.CourseID,
                                  Title = c.Title,
                                  Description = c.Description,
                                  TitlePageImageURL = c.TitlePageImageURL,
                                  OriginalPrice = c.OriginalPrice,
                                  TotalMinTime = c.TotalMinTime,
                                  MemberID = m.MemberID,
                                  Photo = m.Photo
                              };

            return result.ToList();
        }
        //取得會員所開課程的渲染課程卡片所需資料欄位(多載+1)
        public List<AllCourse> GetCourseData(string MemberId)
        {
            var course = _courseRepository.GetAllCourse();
            var member = _courseRepository.GetAllMember();

            var result = from c in course
                         join m in member on c.MemberID equals m.MemberID
                         where c.MemberID == MemberId
                         select new AllCourse
                         {
                             CourseID = c.CourseID,
                             Title = c.Title,
                             Description = c.Description,
                             TitlePageImageURL = c.TitlePageImageURL,
                             OriginalPrice = c.OriginalPrice,
                             TotalMinTime = c.TotalMinTime,
                             MemberID = m.MemberID,
                             Photo = m.Photo
                         };

            return result.ToList();
        }
        //刪除指定課程資料
        public void DeleteCurrentIdCourseData(string id)
        {
            _courseRepository.DeleteCurrentIdCourseData(id);
        }

    }
}