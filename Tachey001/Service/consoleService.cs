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
        //初始化資料庫邏輯
        public consoleService()
        {
            _tacheyRepository = new TacheyRepository(new TacheyContext());
        }

        //顯示所有
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

        //顯示console
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
                foreach (var score in advscore)
                {
                    if (item.CourseID == score.id)
                    {
                        item.AvgScore = Convert.ToInt32(score.score);
                    }
                }
            }

            return result.ToList();
        }

        //依照分類顯示（路由）
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

        //最新排序
        private int pageSize = 24;
        public IPagedList<consoleViewModel> GetCardsPageList(int page)
        {
            var course = _tacheyRepository.GetAll<Models.Course>();
            var member = _tacheyRepository.GetAll<Models.Member>();
            var orderTail = _tacheyRepository.GetAll<Models.Order_Detail>();

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

            int currentPage = page < 1 ? 1 : page;
            var oresult = result.OrderBy(x => x.CreateDate);
            var rresult = oresult.ToPagedList(currentPage, pageSize);

            return rresult;
        }


        //熱門排序
        public IPagedList<consoleViewModel> AllHot(int page)
        {
            var course = _tacheyRepository.GetAll<Models.Course>();
            var member = _tacheyRepository.GetAll<Models.Member>();
            var orderTail = _tacheyRepository.GetAll<Models.Order_Detail>();

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

            int currentPage = page < 1 ? 1 : page;
            var oresult = result.OrderBy(x => x.CreateDate);
            var rresult = oresult.ToPagedList(currentPage, pageSize);

            return rresult;
        }
        
        
        //最多人數排序
        public IPagedList<consoleViewModel> GetCardsHotPageList(int page)
        {
            var all = GetConsoleData();

            var oD = _tacheyRepository.GetAll<Models.Order_Detail>()
                                                                .GroupBy(x => x.CourseID)
                                                                .Select(x => new { id = x.Key, Count = x.Count() });

            foreach (var C in all)
            {
                foreach (var item in oD)
                {
                    if (C.CourseID == item.id)
                    {
                        C.CountBuyCourse = item.Count;
                    }
                }
            }

            var result = all.OrderByDescending(x => x.CountBuyCourse).Take(24);

            int currentPage = page < 1 ? 1 : page;

            var rresult = result.ToPagedList(currentPage, pageSize);

            return rresult;
        }

        //最常課時
        public IPagedList<consoleViewModel> OrderByTotalTimeOfCourse(int page)
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
                             DetailID = c.CategoryDetailsID
                         };

            int currentPage = page < 1 ? 1 : page;
            var oresult = result.OrderBy(x => x.TotalMinTime);
            var rresult = oresult.ToPagedList(currentPage, pageSize);


            return rresult;
        }

        //最高評價
        public IPagedList<consoleViewModel> OrderByCourseScore( int page )
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
                foreach (var score in advscore)
                {
                    if (item.CourseID == score.id)
                    {
                        item.AvgScore = Convert.ToInt32(score.score);
                    }
                }
            }

            int currentPage = page < 1 ? 1 : page;
            var oresult = result.OrderBy(x => x.AvgScore);
            var rresult = oresult.ToPagedList(currentPage, pageSize);


            return rresult;
        }

        //搜尋
        public IPagedList<consoleViewModel> Search(string search, int page)
        {
            var course = _tacheyRepository.GetAll<Models.Course>();
            var member = _tacheyRepository.GetAll<Models.Member>();
            
            var result = from m in member 
                         join c in course on m.MemberID equals c.MemberID
                         where c.Title.Contains(search)
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


            int currentPage = page < 1 ? 1 : page;
            var oresult = result.OrderBy(x => x.CreateDate);
            var rresult = oresult.ToPagedList(currentPage, pageSize);


            return rresult;
        }

        //猜你想學
        public IPagedList<consoleViewModel> GuessYouLike(string currutId, int page)
        {
            var course = _tacheyRepository.GetAll<Models.Course>();
            var member = _tacheyRepository.GetAll<Models.Member>();
            var category = _tacheyRepository.GetAll<Models.CourseCategory>();
            var detail = _tacheyRepository.GetAll<Models.CategoryDetail>();

            var search = from m in member
                         where m.MemberID == currutId
                         select m.Interest;

            var ss = search.FirstOrDefault().Split('/');



            var all = GetConsoleData();
            var result = new List<consoleViewModel>();

            //result = all.Where(x => x.DetailID == detailid).Select(x => x).ToList();
            result = all.Where(x => ss.Any(x.DetailName.Contains) || ss.Any(x.CategoryName.Contains)).Select(x => x).ToList();




            int currentPage = page < 1 ? 1 : page;
            var oresult = result.OrderBy(x => x.CreateDate);
            var rresult = oresult.ToPagedList(currentPage, pageSize);


            return rresult;
        }
    }    
}