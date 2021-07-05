using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tachey001.ViewModel.ApiViewModel
{
    public class ApiResult
    {
        public ApiResult(int status, string errMsg, object result)
        {
            Status = status;
            ErrMsg = errMsg;
            Result = result;
        }

        public int Status { get; set; }
        public string ErrMsg { get; set; }
        public object Result { get; set; }

    }
    public static class ApiStatus
    {
        public const int Success = 0;
        public const int Fail = 1;
    }
}