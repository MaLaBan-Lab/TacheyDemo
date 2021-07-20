using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tachey001.ViewModel.ApiViewModel
{
    public class OperationResult
    {
        public bool IsSuccessful { get; set; }
        public string Exception { get; set; }
    }
}