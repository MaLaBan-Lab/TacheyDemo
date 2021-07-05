using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.ViewModel;

namespace Tachey001.ViewModel
{
    public class Card_highlight_Group
    {
        //大籃子裝三個小包
        public List<HighlightCourseViewModel> highlightViewModels { get; set; }
        public List<CourseCardViewModel> courseCardViewModels { get; set; }
        public List<CommentViewModel> commentViewModels { get; set; }
        public List<CartPartialCardViewModel> cartPartialCardViewModels { get; set; }
    }
}