using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tachey001.ViewModel.Course
{
    public class AllCourse
    {
        public string CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TitlePageImageURL { get; set; }
        public decimal? OriginalPrice { get; set; }
        public int? TotalMinTime { get; set; }
        public string MemberID { get; set; }
        public string Photo { get; set; }
    }
}