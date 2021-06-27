﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tachey001.ViewModel.Course
{
    public class AnswerCard
    {
        public string CourseID { get; set; }
        public int QuestionID { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string AnswerContent { get; set; }
        public DateTime? AnswerDate { get; set; }
        public int LikeAmount { get; set; }
    }
}