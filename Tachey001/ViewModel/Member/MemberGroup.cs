using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;
using Tachey001.ViewModel.Course;

namespace Tachey001.ViewModel.Member
{
    public class MemberGroup
    {
        public MemberViewModel member { get; set; }
        public List<MemberViewModel> memberViewModels { get; set; }
        public List<PointViewModel> pointViewModels { get; set; }
        public List<PointViewModel> usedpointViewModels { get; set; }
        public List<PointViewModel> getpointViewModels { get; set; }
        public StepGroup courseViewModels { get; set; }
        public List<CourseCateDet> courseCateDet { get; set; }
    }
}