using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Repository;
using Tachey001.ViewModel.Member;

namespace Tachey001.Service.Member
{
    public class MemberService
    {
        private MemberRepository _memberRepository;
        //初始化資料庫邏輯
        public MemberService()
        {
            _memberRepository = new MemberRepository();
        }

        public List<MemberViewModel> GetMemberData(string MemberId)
        {
            var member = _memberRepository.GetAllMember();
            var personurl = _memberRepository.GetPersonalUrls();

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
            var member = _memberRepository.GetAllMember();
            var point = _memberRepository.GetPoints();

            var result = from m in member
                         join p in point on m.MemberID equals p.MemberID
                         where m.MemberID == MemberId
                         select new PointViewModel
                         {
                             MemberID = p.MemberID,
                             PointName = p.PointName,
                             PointNum = p.PointNum,
                             GetTime = p.GetTime,
                             Deadline = p.Deadline,
                             Status = p.Status,
                         };

            return result.ToList();
        }
    }
}