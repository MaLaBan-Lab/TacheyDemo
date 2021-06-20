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
        //取得開課View渲染資料
        public StepGroup GetStepGroup(string CourseId)
        {
            var currentCourse = _courseRepository.GetCurrentCourse(CourseId);

            var chapterList = _courseRepository.GetCurrentCourseChapters(CourseId);
            var unitList = _courseRepository.GetCourseUnits(CourseId);

            var categoryList = _courseRepository.GetCourseCategory();
            var detailList = _courseRepository.GetCategoryDetail();

            var result = new StepGroup
            {
                course = currentCourse,
                courseChapter = chapterList,
                courseUnit = unitList,
                courseCategory = categoryList,
                categoryDetails = detailList
            };

            return result;
        }
        public string NewCourseStep(string currentUserId)
        {
            //取得12位數亂碼課程ID
            var CourseId = GetRandomId(12);

            //檢查是否重複課程ID
            while(_courseRepository.GetCurrentCourse(CourseId) != null)
            {
                CourseId = GetRandomId(12);
            }

            _courseRepository.CreateNewCourse(CourseId, currentUserId);

            return CourseId;
        }
        //取得自訂位數的亂數方法
        private string GetRandomId(int Length)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            int passwordLength = Length;
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            return new string(chars);
        }
    }
}