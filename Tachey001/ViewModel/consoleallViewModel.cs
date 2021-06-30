using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.ViewModel.Course;
using PagedList;

namespace Tachey001.ViewModel
{
    public class consoleallViewModel
    {
        public List<consoleViewModel> consoleViews { get; set; }
        public List<AllCourse> allCourses { get; set; }

        public IPagedList<consoleViewModel> pageConsole { get; set; }

    }
}