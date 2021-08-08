using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;

namespace Tachey001.ViewModel
{
    public class HighlightCourseViewModel
    {
        public string CourseID { get; set; }
        public string Title { get; set; }
        public string Effect { get; set; }
        public int? TotalMinTime { get; set; }
        public string Description { get; set; }
        public string TitlePageImageURL { get; set; }
        public int chapterCount { get; set; }
        public int unitCount { get; set; }
    }
}