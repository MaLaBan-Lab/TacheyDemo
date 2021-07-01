﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Tachey001.Models;
using Tachey001.Repository;
using Tachey001.Repository.Home;
using Tachey001.ViewModel;
using Tachey001.ViewModel.Course;
using Tachey001.ViewModel.Member;

namespace Tachey001.Service.Member
{
    public class MemberService
    {
        //宣告資料庫邏輯
        private TacheyRepository _tacheyRepository;
       
        private HomeRepository _HomeRepository;
        private MemberRepository _MemberRepository;
        public MemberService()
        {
            _tacheyRepository = new TacheyRepository(new TacheyContext());
            
            _HomeRepository = new HomeRepository();
            _MemberRepository = new MemberRepository();
        }

        public MemberViewModel GetMemberData(string MemberId)
        {
            var member = _tacheyRepository.GetAll<Models.Member>(x => x.MemberID == MemberId);

            var result = from m in member
                         select new MemberViewModel
                         {
                             MemberID = m.MemberID,
                             Account = m.Account,
                             Password = m.Password,
                             Name = m.Name,
                             Email = m.Email,
                             EmailStatus = m.EmailStatus,
                             JoinTime = m.JoinTime,
                             Sex = m.Sex,
                             CountryRegion = m.CountryRegion,
                             City = m.City,
                             Address = m.Address,
                             PostalCode = m.PostalCode,
                             PhoneNumber = m.PhoneNumber,
                             Birthday = m.Birthday,
                             Year = m.Birthday.Value.Year.ToString(),
                             Month = m.Birthday.Value.Month.ToString(),
                             Day = m.Birthday.Value.Day.ToString(),
                             Interest = m.Interest,
                             Like = m.Like,
                             Expertise = m.Expertise,
                             About = m.About,
                             InterestContent = m.InterestContent,
                             Language = m.Language,
                             Photo = m.Photo,
                             Introduction = m.Introduction,
                             Theme = m.Theme,
                             Profession = m.Profession,
                             Point = m.Point,
                             Facebook = m.Facebook,
                             Line = m.Line,
                             Google = m.Google,
                         };



            return (MemberViewModel)result;
        }

        public List<MemberViewModel> GetAllMemberData(string MemberId)
        {
            var member = _tacheyRepository.GetAll<Models.Member>(x => x.MemberID == MemberId);
            var personurl = _tacheyRepository.GetAll<Models.PersonalUrl>(x => x.MemberID == MemberId);

            var result = from m in member
                         join u in personurl on m.MemberID equals u.MemberID into gj
                         from subpet in gj.DefaultIfEmpty()
                         select new MemberViewModel
                         {
                             MemberID = m.MemberID,
                             Account = m.Account,
                             Password = m.Password,
                             Name = m.Name,
                             Email = m.Email,
                             EmailStatus = m.EmailStatus,
                             JoinTime = m.JoinTime,
                             Sex = m.Sex,
                             CountryRegion = m.CountryRegion,
                             City = m.City,
                             Address = m.Address,
                             PostalCode = m.PostalCode,
                             PhoneNumber = m.PhoneNumber,
                             Birthday = m.Birthday,
                             Year = m.Birthday.Value.Year.ToString(),
                             Month = m.Birthday.Value.Month.ToString(),
                             Day = m.Birthday.Value.Day.ToString(),
                             Interest = m.Interest,
                             Like = m.Like,
                             Expertise = m.Expertise,
                             About = m.About,
                             InterestContent = m.InterestContent,
                             Language = m.Language,
                             Photo = m.Photo,
                             Introduction = m.Introduction,
                             Theme = m.Theme,
                             Profession = m.Profession,
                             Point = m.Point,
                             Facebook = m.Facebook,
                             Line = m.Line,
                             Google = m.Google,
                             FbUrl = subpet.FbUrl,
                             GoogleUrl = subpet.GoogleUrl,
                             YouTubeUrl = subpet.YouTubeUrl,
                             BehanceUrl = subpet.BehanceUrl,
                             PinterestUrl = subpet.PinterestUrl,
                             BlogUrl = subpet.BlogUrl
                         };

            

            return result.ToList();
        }

        public StepGroup GetCourseData()
        {
            var category = _tacheyRepository.GetAll<CourseCategory>();
            var detail = _tacheyRepository.GetAll<CategoryDetail>();

            var result = new StepGroup
                         {
                             courseCategory = category,
                             categoryDetails = detail,
                         };

            return result;
        }

            public List<PointViewModel> GetPointData(string MemberId)
        {
            var member = _tacheyRepository.GetAll<Models.Member>(x => x.MemberID == MemberId);
            var point = _tacheyRepository.GetAll<Models.Point>(x => x.MemberID == MemberId);
            //var member = _memberRepository.GetAllMember();
            //var point = _memberRepository.GetPoints();

            var result = from m in member
                         join p in point on m.MemberID equals p.MemberID
                         where m.MemberID == MemberId
                         select new PointViewModel
                         {
                             MemberID = p.MemberID,
                             Point = m.Point,
                             PointName = p.PointName,
                             PointNum = p.PointNum,
                             GetTime = p.GetTime,
                             Deadline = p.Deadline,
                             Status = p.Status,
                         };

            return result.ToList();
        }

        public List<PointViewModel> GetPartialPoint(string MemberId, bool tf)
        {
            var member = _tacheyRepository.GetAll<Models.Member>(x => x.MemberID == MemberId);
            var point = _tacheyRepository.GetAll<Models.Point>(x => x.MemberID == MemberId);
            //var member = _memberRepository.GetAllMember();
            //var point = _memberRepository.GetPoints();

            var result = from m in member
                         join p in point on m.MemberID equals p.MemberID
                         where m.MemberID == MemberId && p.Status == tf
                         select new PointViewModel
                         {
                             MemberID = p.MemberID,
                             Point = m.Point,
                             PointName = p.PointName,
                             PointNum = p.PointNum,
                             GetTime = p.GetTime,
                             Deadline = p.Deadline,
                             Status = p.Status,
                         };

            return result.ToList();
        }

        public List<PointViewModel> GetAllCourse(string MemberId)
        {
            var member = _tacheyRepository.GetAll<Models.Member>(x => x.MemberID == MemberId);
            var point = _tacheyRepository.GetAll<Models.Point>(x => x.MemberID == MemberId);
            //var member = _memberRepository.GetAllMember();
            //var point = _memberRepository.GetPoints();

            var result = from m in member
                         join p in point on m.MemberID equals p.MemberID
                         where m.MemberID == MemberId && p.Status == true
                         select new PointViewModel
                         {
                             MemberID = p.MemberID,
                             Point = m.Point,
                             PointName = p.PointName,
                             PointNum = p.PointNum,
                             GetTime = p.GetTime,
                             Deadline = p.Deadline,
                             Status = p.Status,
                         };

            return result.ToList();
        }
        public void UpdateMemberData(string id)
        {
            var result = _tacheyRepository.Get<Models.Member>(x => x.MemberID == id);

            // _tacheyRepository.Delete(result);
            result.Sex = id;

            _tacheyRepository.SaveChanges();
        }
        //取得收藏的課
        public List<consoleViewModel> GetConsoleData(string currutId)
        {
            var course = _tacheyRepository.GetAll<Models.Course>();
            var member = _tacheyRepository.GetAll<Models.Member>();
            var coursescore = _tacheyRepository.GetAll<CourseScore>();
            var owner = _tacheyRepository.GetAll<Owner>();

            var advscore = coursescore.GroupBy(x => x.CourseID).Select(z => new
            {
                id = z.Key,
                score = z.Average(x => x.Score)
            });

            var all = from o in owner
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
                             Photo = m.Photo,
                             AvgScore = 0,
                             favorite = true
                         };

            var result = all.ToList();

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

            return result;
        }
        //判斷是否收藏
        public void CreateOwner(string MemberId, string CourseId)
        {
            var ToF = _tacheyRepository.Get<Owner>(x => x.MemberID == MemberId && x.CourseID == CourseId);
            if(ToF == null)
            {
                var result = new Owner { MemberID = MemberId, CourseID = CourseId };
                _tacheyRepository.Create(result);
            }
            else
            {
                _tacheyRepository.Delete(ToF);
            }
            _tacheyRepository.SaveChanges();
        }
        //判斷是否加入購物車
        public void CreateCart(string MemberId, string CourseId)
        {
            var ToF = _tacheyRepository.Get<ShoppingCart>(x => x.MemberID == MemberId && x.CourseID == CourseId);
            if (ToF == null)
            {
                var result = new ShoppingCart { MemberID = MemberId, CourseID = CourseId };
                _tacheyRepository.Create(result);
            }
            else
            {
                _tacheyRepository.Delete(ToF);
            }
            _tacheyRepository.SaveChanges();
        }
        public List<CartPartialCardViewModel> GetCartPartialViewModel(string memberId)
        {

            var course = _MemberRepository.GetCourses();
            var shoppingcart = _MemberRepository.GetShoppingCarts();
            var result = from c in course
                         join s in shoppingcart on c.CourseID equals s.CourseID
                         //shoppingCartID=MemberController的Cart裡抓到的會員ID
                         where s.MemberID == memberId
                         select new CartPartialCardViewModel { Title = c.Title, TitlePageImageURL = c.TitlePageImageURL, CreateVerify = c.CreateVerify, OriginalPrice = c.OriginalPrice, CourseID = s.CourseID };
            return result.ToList();
        }

        public List<CourseCardViewModel> GetCourseCardViewModels()
        {
            var member = _HomeRepository.GetMembers();
            var course = _HomeRepository.GetCourses();
            var result = from c in course
                         join m in member on c.MemberID equals m.MemberID
                         select new CourseCardViewModel { Photo = m.Photo, Title = c.Title, TotalMinTime = c.TotalMinTime, OriginalPrice = c.OriginalPrice, TitlePageImageURL = c.TitlePageImageURL };
            return result.ToList();
        }
        public  decimal Caltotal(string memberId)
        {
            //呼叫方法用東西接
            var Cartlist = GetCartPartialViewModel(memberId);
            decimal totalprice = 0;
            foreach (var p in Cartlist)
            {
                totalprice= p.OriginalPrice+totalprice;
            }
            return totalprice;
        }

    }
}