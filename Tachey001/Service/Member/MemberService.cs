using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tachey001.Models;
using Tachey001.Repository;
using Tachey001.ViewModel;
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

        public List<MemberViewModel> GetAllMemberData(string MemberId)
        {
            var member = _tacheyRepository.GetAll<Models.Member>(x => x.MemberID == MemberId);
            var personurl = _tacheyRepository.GetAll<Models.PersonalUrl>(x => x.MemberID == MemberId);

            var result = from m in member
                         join u in personurl on m.MemberID equals u.MemberID
                         where m.MemberID == MemberId
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
                             FbUrl = u.FbUrl,
                             GoogleUrl = u.GoogleUrl,
                             YouTubeUrl = u.YouTubeUrl,
                             BehanceUrl = u.BehanceUrl,
                             PinterestUrl = u.PinterestUrl,
                             BlogUrl = u.BlogUrl
                         };

            return result.ToList();
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

        public List<PointViewModel> GetGetPoint(string MemberId)
        {
            var member = _tacheyRepository.GetAll<Models.Member>(x => x.MemberID == MemberId);
            var point = _tacheyRepository.GetAll<Models.Point>(x => x.MemberID == MemberId);
            //var member = _memberRepository.GetAllMember();
            //var point = _memberRepository.GetPoints();

            var result = from m in member
                         join p in point on m.MemberID equals p.MemberID
                         where m.MemberID == MemberId && p.Status == false
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

        public List<PointViewModel> GetUsedPoint(string MemberId)
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

    }
}