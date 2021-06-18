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
        public List<AllCourse> GetAllCourse()
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

        public List<AllCourse> GetMemberCreateCourse(string MemberId)
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
    }
}