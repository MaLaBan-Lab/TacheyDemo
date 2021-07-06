using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tachey001.ViewModel.Course
{
    public class CourseCateDet
    {
        public string CourseID { get; set; }
        public int? CategoryID { get; set; }
        public int? DetailID { get; set; }
        public string CategoryName { get; set; }
        public string DetailName { get; set; }
    }
}