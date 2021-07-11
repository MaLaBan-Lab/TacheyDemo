using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.ViewModel.Course;

namespace Tachey001.ViewModel
{
    public class consoleViewModel
    {
        public string CourseID { get; set; }
        public string Title { get; set; }
        public string TitlePageImageURL { get; set; }
        public decimal OriginalPrice { get; set; }
        public int? TotalMinTime { get; set; }
        public string MemberID { get; set; }
        public string Photo { get; set; }
        public int AvgScore { get; set; }
        public int TotalScore { get; set; }
        public bool favorite { get; set; }
        public int? CategoryID { get; set; }
        public int? DetailID { get; set; }
        public string CategoryName { get; set; }
        public string DetailName { get; set; }
        public DateTime? CreateDate { get; set; }
        public int CountBuyCourse { get; set; }
        public int? MainClick { get; set; }
    }
}