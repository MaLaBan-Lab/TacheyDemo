using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tachey001.ViewModel.Course
{
    public class AnswerCard
    {
        public string CourseID { get; set; }
        public int QuestionID { get; set; }
        public int AnswerID { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        [StringLength(4000)]
        public string AnswerContent { get; set; }
        public List<string> AllMemberId { get; set; }
        public DateTime? AnswerDate { get; set; }
    }
}