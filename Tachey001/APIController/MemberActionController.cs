using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Tachey001.Service.Member;
using Tachey001.ViewModel.ApiViewModel;

namespace Tachey001.APIController
{
    public class MemberActionController : ApiController
    {
        private MemberService _memberService;

        public MemberActionController()
        {
            _memberService = new MemberService();
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
        [HttpGet]
        public ApiResult Test()
        {
            try
            {
                return new ApiResult(ApiStatus.Success, "成功!9527", null);
            }
            catch (Exception ex)
            {
                return new ApiResult(ApiStatus.Fail, ex.Message, null);
            }
        }
    }
}
