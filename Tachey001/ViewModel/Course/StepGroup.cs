using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tachey001.Models;

namespace Tachey001.ViewModel.Course
{
    public class StepGroup
    {
        public Models.Course course { get; set; }
        public IEnumerable<CourseCategory> courseCategory { get; set; }
        public IEnumerable<CategoryDetail> categoryDetails { get; set; }
        public IEnumerable<CourseChapter> courseChapter { get; set; }
        public IEnumerable<CourseUnit> courseUnit { get; set; }
        public FormCollection formCollection { get; set; }
    }
}