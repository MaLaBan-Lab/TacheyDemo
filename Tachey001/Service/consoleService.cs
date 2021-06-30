using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;
using Tachey001.Repository;
using Tachey001.ViewModel;
using PagedList;


namespace Tachey001.Service
{
    public class consoleService
    {
        //宣告資料庫邏輯
        private TacheyRepository _tacheyRepository;
        private consoleRepository _consoleRepository;
        //初始化資料庫邏輯
        public consoleService()
        {
            _consoleRepository = new consoleRepository();
            _tacheyRepository = new TacheyRepository(new TacheyContext());
        }
        public List<consoleViewModel> GetConsoleData()
        {
            var course = _tacheyRepository.GetAll<Models.Course>();
            var member = _tacheyRepository.GetAll<Models.Member>();

            var result = from c in course
                         join m in member on c.MemberID equals m.MemberID
                         select new consoleViewModel
                         {
                             CourseID = c.CourseID,
                             Title = c.Title,
                             Description = c.Description,
                             TitlePageImageURL = c.TitlePageImageURL,
                             OriginalPrice = c.OriginalPrice,
                             TotalMinTime = c.TotalMinTime,
                             MemberID = m.MemberID,
                             Photo = m.Photo,
                             CategoryID = c.CategoryID,
                             DetailID = c.CategoryDetailsID,
                             CreateDate = c.CreateDate
                         };

            
            return result.ToList();
        }

        public List<consoleViewModel> GetConsoleData(string currutId)
        {
            var course = _tacheyRepository.GetAll<Models.Course>();
            var member = _tacheyRepository.GetAll<Models.Member>();
            var coursescore = _tacheyRepository.GetAll<Models.CourseScore>();
            var owner = _tacheyRepository.GetAll<Models.Owner>();

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

        public List<consoleViewModel> GetGroupData(int? categoryid, int? detailid)
        {
            var category = _tacheyRepository.GetAll<Models.CourseCategory>();
            var detail = _tacheyRepository.GetAll<Models.CategoryDetail>();
            var all = GetConsoleData();
            var result = new List<consoleViewModel>();

            if (categoryid == null)
            {
                result = all.Where(x => x.DetailID == detailid).Select(x => x).ToList();
            }
            if (detailid == null)
            {

                result = all.Where(x => x.CategoryID == categoryid).Select(x => x).ToList();
            }

            return result;
        }

    }
}