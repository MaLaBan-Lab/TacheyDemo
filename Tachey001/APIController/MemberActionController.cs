using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Tachey001.Service;
using Tachey001.ViewModel;
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
        //取得購物車小卡資料
        [HttpGet]
        public ApiResult GetCartData(string MemberId)
        {
            try
            {
                var getcartcardviewmodels = _memberService.GetCartPartialViewModel(MemberId);
                var getcaltotal = _memberService.Caltotal(MemberId);

                var result = new Cart_GroupViewModel
                {
                    cartpartialViewModels = getcartcardviewmodels,
                    total = getcaltotal
                };

                return new ApiResult(ApiStatus.Success, "成功!", result);
            }
            catch (Exception ex)
            {
                return new ApiResult(ApiStatus.Fail, ex.Message, null);
            }
        }
        //檢查客製url是否重複
        [HttpGet]
        public ApiResult CheckUrl(string Url, string CourseId)
        {
            try
            {
                var result = _courseService.CheckUrl(Url, CourseId);
                if (result)
                {
                    //有重複
                    return new ApiResult(ApiStatus.Fail, "網址已重複，無法使用", null);
                }
                //無重複
                return new ApiResult(ApiStatus.Success, "網址可使用", null);
            }
            catch (Exception ex)
            {
                return new ApiResult(ApiStatus.Fail, ex.Message, null);
            }
        }
    }
}
