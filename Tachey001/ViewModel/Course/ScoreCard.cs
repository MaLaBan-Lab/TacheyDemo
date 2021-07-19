using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tachey001.ViewModel.Course
{
    public class ScoreCard
    {
        public string MemberID { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public int? Score { get; set; }
        public string Title { get; set; }
        public string ScoreContent { get; set; }
        public DateTime? ScoreDate { get; set; }
    }
}