using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.ViewModel;
using Tachey001.Models;
using Tachey001.Repository;

namespace Tachey001.Service
{
    public class HomeService
    {
        //跟Repository拿資料
        private TacheyRepository _tacheyRepository;
        //初始化資料庫邏輯
        public HomeService()
        {
            _tacheyRepository = new TacheyRepository(new TacheyContext());
        }
        //作方法篩選資料
        public List<CommentViewModel> GetCommentViewModel()
        {
            //select完成變成view model
            var member = _tacheyRepository.GetAll<Member>();
            var coursescore = _tacheyRepository.GetAll<CourseScore>();
            var result = from m in member
                         join c in coursescore on m.MemberID equals c.MemberID 
                         select new CommentViewModel { Name = m.Name, Photo = m.Photo, ToTachey = c.ToTachey };
            //要建view model接資料
            return result.Take(3).ToList();
        }
        public List<CourseCardViewModel> GetCourseCardViewModels(string MemberId)
        {
            var member = _tacheyRepository.GetAll<Member>();
            var course = _tacheyRepository.GetAll<Course>();
            var coursescore = _tacheyRepository.GetAll<CourseScore>();
            var owner = _tacheyRepository.GetAll<Owner>(x => x.MemberID == MemberId);

            var advscore = coursescore.GroupBy(x => x.CourseID).Select(z => new
            {
                id = z.Key,
                score = z.Average(x => x.Score),
                total = z.Count()
            });

            var all = from c in course
                         join mem in member on c.MemberID equals mem.MemberID into gj
                         from m in gj.DefaultIfEmpty()
                         select new CourseCardViewModel
                         {
                             CourseID = c.CourseID,
                             MemberID = c.MemberID,
                             Photo = m.Photo,
                             Title = c.Title,
                             TotalMinTime = c.TotalMinTime,
                             OriginalPrice = c.OriginalPrice,
                             TitlePageImageURL = c.TitlePageImageURL,
                             AvgScore = 0,
                             favorite = false
                         };

            var result = all.ToList();

            foreach (var item in result)
            {
                foreach (var score in advscore)
                {
                    if (item.CourseID == score.id)
                    {
                        item.AvgScore = Convert.ToInt32(score.score);
                        item.TotalScore = score.total;
                    }
                }
            }

            if (owner.Count() != 0)
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

            return result;
        }
        public List<HighlightCourseViewModel> GetHighlightCourseViewModels()
        {
            var course = _tacheyRepository.GetAll<Course>();
            var result = from c in course
                         select new HighlightCourseViewModel
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