using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Tachey001.Service;
using Tachey001.ViewModel.ApiViewModel;

namespace Tachey001.APIController
{
    public class MemberActionController : ApiController
    {
        private MemberService _memberService;
        private CourseService _courseService;

        public MemberActionController()
        {
            _memberService = new MemberService();
            _courseService = new CourseService();
        }
        //收藏功能
        [HttpGet]
        public ApiResult Owner(string MemberId, string CourseID)
        {
            try
            {
                _memberService.CreateOwner(MemberId, CourseID);
                return new ApiResult(ApiStatus.Success, CourseID, null);
            }
            catch(Exception ex)
            {
                return new ApiResult(ApiStatus.Fail, ex.Message, null);
            }
        }
        //問題點讚功能
        [HttpGet]
        public ApiResult QLike(string MemberId, string CourseID, int QuestionID)
        {
            try
            {
                _courseService.CreateQLike(MemberId, CourseID, QuestionID);
                return new ApiResult(ApiStatus.Success, CourseID, null);
            }
            catch (Exception ex)
            {
                return new ApiResult(ApiStatus.Fail, ex.Message, null);
            }
        }
        //回答點讚功能
        [HttpGet]
        public ApiResult ALike(string MemberId, string CourseID, int QuestionID, int AnswerID)
        {
            try
            {
                _courseService.CreateALike(MemberId, CourseID, QuestionID, AnswerID);
                return new ApiResult(ApiStatus.Success, CourseID, null);
            }
            catch (Exception ex)
            {
                return new ApiResult(ApiStatus.Fail, ex.Message, null);
            }
        }
    }
}
