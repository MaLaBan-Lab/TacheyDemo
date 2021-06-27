using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tachey001.ViewModel.Member
{
    public class CourseViewModel
    {
        public string MemberID { get; set; }
        public string CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TitlePageImageURL { get; set; }
        public decimal? OriginalPrice { get; set; }
        public int? TotalMinTime { get; set; }
        public string Photo { get; set; }
        public string Hwkchapter { get; set; }
        public string Hwkname { get; set; }
        public string Hwkdescription { get; set; }
        public string HwkPhoto { get; set; }
        public string HwkLike { get; set; }
        public string HwkUrl { get; set; }
    }
}