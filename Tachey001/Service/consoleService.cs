using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Repository;
using Tachey001.ViewModel;

namespace Tachey001.Service
{
    public class consoleService
    {
        //宣告資料庫邏輯
        private consoleRepository _consoleRepository;
        //初始化資料庫邏輯
        public consoleService()
        {
            _consoleRepository = new consoleRepository();
        }

        public List<consoleViewModel> GetConsoleData(string currutId)
        {
            var course = _consoleRepository.GetAllCourse();
            var member = _consoleRepository.GetAllMember();
            var coursescore = _consoleRepository.GetCourseScore();
            var owner = _consoleRepository.GetOwer();

            var advscore = coursescore.GroupBy(x => x.CourseID).Select(z => new
            {
                id = z.Key,
                score = z.Average(x => x.Score)
            });


            var result = from o in owner
                         join m in member on o.MemberID equals m.MemberID
                         join c in course on o.CourseID equals c.CourseID
                         where o.MemberID == currutId
                         select new consoleViewModel
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

            foreach (var item in result)
            {
                foreach(var score in advscore)
                {
                    if(item.CourseID==score.id)
                    {
                        item.AvgScore = Convert.ToInt32(score.score);
                    }
                }
            }

            return result.ToList();
        }
    }
}