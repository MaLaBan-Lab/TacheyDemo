using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;

namespace Tachey001.ViewModel.Course
{
    public class Main_Video
    {
        public Models.Course Course { get; set; }
        public string CategoryName { get; set; }
        public string DetailName { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal? PreOrderPrice { get; set; }
        public List<CourseChapter> courseChapters { get; set; }
        public List<CourseUnit> courseUnits { get; set; }
    }
}