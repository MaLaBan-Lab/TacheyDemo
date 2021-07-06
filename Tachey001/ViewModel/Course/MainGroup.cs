using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;

namespace Tachey001.ViewModel.Course
{
    public class MainGroup
    {
        public Main_Video Main_Video { get; set; }
        public CourseScore PostCourseScore { get; set; }
        public List<ScoreCard> GetCourseScore { get; set; }
        public Question PostCourseQuestion { get; set; }
        public List<QuestionCard> GetQuestions { get; set; }
        public bool GetOwner { get; set; }
    }
}