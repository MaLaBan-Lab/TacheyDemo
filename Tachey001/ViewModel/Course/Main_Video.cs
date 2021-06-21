using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;

namespace Tachey001.ViewModel.Course
{
    public class Main_Video
    {
        public string CourseID { get; set; }
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int? ChapterID { get; set; }
        public string ChapterName { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string UnitUrl { get; set; }
    }
}