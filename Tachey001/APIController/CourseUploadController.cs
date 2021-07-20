using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Tachey001.AccountModels;
using Tachey001.ViewModel.ApiViewModel;
using Tachey001.ViewModel.Course;

namespace Tachey001.APIController
{
    public class CourseUploadController : ApiController
    {
        [HttpGet]
        public ApiResult StepCheck(StepGroup group)
        {
            try
            {
                return new ApiResult(ApiStatus.Success, group.course.CourseID, null);
            }
            catch (Exception ex)
            {
                return new ApiResult(ApiStatus.Fail, ex.Message, null);
            }
        }
    }
}
