using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;
using Tachey001.Repository;
using Tachey001.ViewModel;
using PagedList;
using Dapper;
using System.Data.SqlClient;

namespace Tachey001.Service
{
    public class consoleService
    {
        //宣告資料庫邏輯
        private TacheyRepository _tacheyRepository;
        private DapperRepository _dapperRepository;
        //初始化資料庫邏輯
        public consoleService()
        {
            _tacheyRepository = new TacheyRepository(new TacheyContext());
            _dapperRepository = new DapperRepository();
        }
        //取得所有課程資訊
        public List<consoleViewModel> test()
        {
            var result = _dapperRepository.GetCourse();

            return result;
        }
        //取得收藏表
        public List<Owner> GetOwners(string MId)
        {
            var result = _dapperRepository.GetOwners(MId);

            return result;
        }
        //找尋cat ID
        public int ReturnCategoryId(string category)
        {
            var result = _tacheyRepository.Get<CourseCategory>(x => x.CategoryEngName == category || x.CategoryName == category);

            return result == null ? 0 : result.CategoryID;
        }
        //找尋det ID
        public int ReturnDetailId(string category)
        {
            var result = _tacheyRepository.Get<CategoryDetail>(x => x.DetailName == category);

            return result == null ? 0 : result.DetailID;
        }
        //顯示console我收藏的課
        public List<consoleViewModel> GetConsoleData(string currutId)
        {
            return _dapperRepository.GetCourse(currutId);
        }

        //顯示console我修的課
        public List<consoleViewModel> GetConsoleData1(string currutId)
        {
            var course = _tacheyRepository.GetAll<Course>();
            var member = _tacheyRepository.GetAll<Member>();

            var order = _tacheyRepository.GetAll<Order>(x=>x.MemberID== currutId);
            var oderdetail = _tacheyRepository.GetAll<Order_Detail>();

            var ode = from o in order
                      join od in oderdetail on o.OrderID equals od.OrderID
                      select new { o.OrderID, o.MemberID, od.CourseID };


            var result = from c in course
                         join m in member on c.MemberID equals m.MemberID
                         join o in ode on c.CourseID equals o.CourseID
                         where o.MemberID == currutId
                         select new consoleViewModel
                         {
                             CourseID = c.CourseID,
                             Title = c.Title,
                             TitlePageImageURL = c.TitlePageImageURL,
                             OriginalPrice = c.OriginalPrice,
                             TotalMinTime = c.TotalMinTime,
                             MemberID = m.MemberID,
                             Photo = m.Photo

                         };

            

            return result.ToList();
        }

        //依照分類顯示（路由）
        public List<consoleViewModel> GetGroupData(int? categoryid, int? detailid)
        {
            var category = _tacheyRepository.GetAll<CourseCategory>();
            var detail = _tacheyRepository.GetAll<CategoryDetail>();
            var all = test();
            var result = new List<consoleViewModel>();

            if (categoryid == null || categoryid == 0)
            {
                result = all.Where(x => x.DetailID == detailid).Select(x => x).ToList();
            }
            if (detailid == null || detailid == 0)
            {

                result = all.Where(x => x.CategoryID == categoryid).Select(x => x).ToList();
            }

            return result;
        }

        //猜你想學
        public IPagedList<consoleViewModel> GuessYouLike(string currutId, int page)
        {
            var course = _tacheyRepository.GetAll<Course>();
            var member = _tacheyRepository.GetAll<Member>();
            var category = _tacheyRepository.GetAll<CourseCategory>();
            var detail = _tacheyRepository.GetAll<CategoryDetail>();

            var search = from m in member
                         where m.MemberID == currutId
                         select m.Interest;

            var ss = search.FirstOrDefault().Split('/');

            var last = ss.Reverse().Skip(1);
            //var last = ss.Last()

            var all = test();


            //result = all.Where(x => x.DetailID == detailid).Select(x => x).ToList();
            var result = all.Where(x => last.Any(x.DetailName.Contains)).Select(x => x).ToList();




            int currentPage = page < 1 ? 1 : page;
            var oresult = result.OrderBy(x => x.CreateDate);
            var rresult = oresult.ToPagedList(currentPage, pageSize);


            return rresult;
        }

        //熱門排序
        public IPagedList<consoleViewModel> AllHot(int page)
        {
            var result = test();

            int currentPage = page < 1 ? 1 : page;
            var oresult = result.OrderByDescending(x => x.MainClick);
            var rresult = oresult.ToPagedList(currentPage, pageSize);

            return rresult;
        }


        //搜尋
        public IPagedList<consoleViewModel> Search(string search, int page)
        {
            var result = test();


            int currentPage = page < 1 ? 1 : page;
            var oresult = result.Where(x => x.Title.Contains($"{search}")).OrderBy(x => x.CreateDate);
            var rresult = oresult.ToPagedList(currentPage, pageSize);


            return rresult;
        }

        //最新排序
        private int pageSize = 24;
        public IPagedList<consoleViewModel> GetCardsPageList(int page)
        {

            var result = test();

            int currentPage = page < 1 ? 1 : page;
            var oresult = result.OrderBy(x => x.CreateDate);
            var rresult = oresult.ToPagedList(currentPage, pageSize);

            return rresult;
        }

        //最多人數排序
        public IPagedList<consoleViewModel> GetCardsHotPageList(int page)
        {
            var all = test();

            var result = all.OrderByDescending(x =>x.CountBuyCourse);

            int currentPage = page < 1 ? 1 : page;

            var rresult = result.ToPagedList(currentPage, pageSize);

            return rresult;
        }

        //最長課時
        public IPagedList<consoleViewModel> OrderByTotalTimeOfCourse(int page)
        {
            var result = test();

            int currentPage = page < 1 ? 1 : page;
            var oresult = result.OrderByDescending(x => x.TotalMinTime);
            var rresult = oresult.ToPagedList(currentPage, pageSize);


            return rresult;
        }

        //最高評價
        public IPagedList<consoleViewModel> OrderByCourseScore(int page)
        {
           
            var result = test();

            int currentPage = page < 1 ? 1 : page;
            var oresult = result.OrderByDescending(x => x.AvgScore);
            var rresult = oresult.ToPagedList(currentPage, pageSize);


            return rresult;
        }

        
    }
}