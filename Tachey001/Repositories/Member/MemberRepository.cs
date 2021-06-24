using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;

namespace Tachey001.Repository
{
    public class MemberRepository
    {
        private TacheyContext _context;

        public MemberRepository()
        {
            _context = new TacheyContext();
        }

        public IEnumerable<Member> GetAllMember()
        {
            var result = _context.Member;

            return result;
        }
        
        public Member GetCurrentMember(string currentId)
        {
            var result = _context.Member.Find(currentId);

            return result;
        }

        public IQueryable<PersonalUrl> GetPersonalUrls()
        {
            var result = _context.PersonalUrl;

            return result;
        }

        public PersonalUrl GetCurrentPersonalUrl(string currentId)
        {
            var result = _context.PersonalUrl.Find(currentId);

            return result;
        }

        public IQueryable<Point> GetPoints()
        {
            var result = _context.Point;

            return result;
        }

        public Point GetCurrentPoint(string currentId)
        {
            var result = _context.Point.Find(currentId);

            return result;
        }
    }

}