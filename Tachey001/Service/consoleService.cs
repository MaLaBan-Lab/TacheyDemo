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

        public List<consoleViewModel> GetConsoleData(string MemberId)
        {
            var course = _consoleRepository.GetAllCourse();
            var member = _consoleRepository.GetAllMember();

            var result = from c in course
                         join m in member on c.MemberID equals m.MemberID
                         where c.MemberID == MemberId
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

            return result.ToList();
        }

        //刪除指定課程資料
        public void DeleteCurrentIdCourseData(string id)
        {
            _consoleRepository.DeleteCurrentIdCourseData(id);
        }
    }
}