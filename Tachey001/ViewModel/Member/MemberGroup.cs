using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tachey001.ViewModel.Member
{
    public class MemberGroup
    {
        public MemberViewModel member { get; set; }
        public List<MemberViewModel> memberViewModels { get; set; }
        public List<PointViewModel> pointViewModels { get; set; }
    }
}