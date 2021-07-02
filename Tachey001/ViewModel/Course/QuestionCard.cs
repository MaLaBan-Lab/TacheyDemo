using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;

namespace Tachey001.ViewModel.Course
{
    public class QuestionCard
    {
        public string CourseID { get; set; }
        public int QuestionID { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string CurrentName { get; set; }
        public string CurrentPhoto { get; set; }
        public string QuestionContent { get; set; }
        public DateTime? QuestionDate { get; set; }
        public int AnsAmount { get; set; }
        public List<string> AllMemberId { get; set; }
        public Answer PostAnswer { get; set; }
        public List<AnswerCard> GetAnswerCards { get; set; }
    }
}