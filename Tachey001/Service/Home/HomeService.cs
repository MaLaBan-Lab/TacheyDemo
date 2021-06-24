using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.ViewModel;
using Tachey001.Models;
using Tachey001.Repository;
using Tachey001.Repository.Home;

namespace Tachey001.Service.Home
{
    public class HomeService
    {
        //跟Repository拿資料
        private HomeRepository _HomeRepository;
        private TacheyRepository _tacheyRepository;
        //初始化資料庫邏輯
        public HomeService()
        {
            _HomeRepository = new HomeRepository();
            _tacheyRepository = new TacheyRepository(new TacheyContext());
        }
        //作方法篩選資料
        public List<CommentViewModel> GetCommentViewModel()
        {
            //select完成變成view model
            var member = _HomeRepository.GetMembers();
            var coursescore = _HomeRepository.GetCourseScores();
            var result = from m in member
                         join c in coursescore on m.MemberID equals c.MemberID
                         select new CommentViewModel { Name = m.Name, Photo = m.Photo, ToTachey = c.ToTachey };
            //要建view model接資料
            return result.ToList();
        }
        public List<CourseCardViewModel> GetCourseCardViewModels(string MemberId)
        {
            var member = _tacheyRepository.GetAll<Models.Member>();
            var course = _tacheyRepository.GetAll<Models.Course>();

            var result = from c in course
                         join m in member on c.MemberID equals m.MemberID
                         select new CourseCardViewModel
                         {
                             CourseID = c.CourseID,
                             Photo = m.Photo,
                             Title = c.Title,
                             TotalMinTime = c.TotalMinTime,
                             OriginalPrice = c.OriginalPrice,
                             TitlePageImageURL = c.TitlePageImageURL,
                             favorite = false
                         };

            var owner = _tacheyRepository.GetAll<Owner>(x => x.MemberID == MemberId);

            if(owner.Count() != 0)
            {
                foreach (var o in owner)
                {
                    foreach (var r in result)
                    {
                        if (r.CourseID == o.CourseID)
                        {
                            r.favorite = true;
                        }
                    }
                }
            }

            return result.ToList();
        }
        public List<HighlightCourseViewModel> GetHighlightCourseViewModels()
        {
            var course = _HomeRepository.GetCourses();
            var result = from c in course
                         select new
HighlightCourseViewModel
                         {
                             Title = c.Title,
                             Introduction = c.Introduction,
                             TotalMinTime = c.TotalMinTime,
                             Description = c.Description,
                             TitlePageImageURL = c.TitlePageImageURL
                         };

            return result.ToList();
        }

    }
}