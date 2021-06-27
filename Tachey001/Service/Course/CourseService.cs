using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tachey001.Models;
using Tachey001.Repository;
using Tachey001.Repository.Course;
using Tachey001.ViewModel.Course;

namespace Tachey001.Service.Course
{
    public class CourseService
    {
        //宣告資料庫邏輯
        private TacheyRepository _tacheyRepository;
        private CourseRepository _courseRepository;

        //初始化資料庫邏輯
        public CourseService()
        {
            _tacheyRepository = new TacheyRepository(new TacheyContext());
            _courseRepository = new CourseRepository();
        }
        //取得渲染課程卡片所需資料欄位
        public List<AllCourse> GetCourseData()
        {
            var course = _tacheyRepository.GetAll<Models.Course>();
            var member = _tacheyRepository.GetAll<Models.Member>();

            var result = from c in course
                         join m in member on c.MemberID equals m.MemberID
                         select new AllCourse
                         {
                             CourseID = c.CourseID,
                             Title = c.Title,
                             Description = c.Description,
                             TitlePageImageURL = c.TitlePageImageURL,
                             OriginalPrice = c.OriginalPrice,
                             TotalMinTime = c.TotalMinTime,
                             MemberID = m.MemberID,
                             Photo = m.Photo
                         };

            return result.ToList();
        }
        //取得會員所開課程的渲染課程卡片所需資料欄位(多載+1)
        public List<AllCourse> GetCourseData(string MemberId)
        {
            var course = _tacheyRepository.GetAll<Models.Course>();
            var member = _tacheyRepository.GetAll<Models.Member>(x => x.MemberID == MemberId);

            var result = from c in course
                         join m in member on c.MemberID equals m.MemberID
                         select new AllCourse
                         {
                             CourseID = c.CourseID,
                             Title = c.Title,
                             Description = c.Description,
                             TitlePageImageURL = c.TitlePageImageURL,
                             OriginalPrice = c.OriginalPrice,
                             TotalMinTime = c.TotalMinTime,
                             MemberID = m.MemberID,
                             Photo = m.Photo
                         };

            return result.ToList();
        }
        //刪除指定課程資料
        public void DeleteCurrentIdCourseData(string id)
        {
            var result = _tacheyRepository.Get<Models.Course>(x => x.CourseID == id);

            _tacheyRepository.Delete(result);

            _tacheyRepository.SaveChanges();
        }
        //取得開課View渲染資料
        public StepGroup GetStepGroup(string CourseId)
        {
            var currentCourse = _tacheyRepository.Get<Models.Course>(x => x.CourseID == CourseId);

            var chapterList = _tacheyRepository.GetAll<CourseChapter>(x => x.CourseID == CourseId);

            var unitList = _tacheyRepository.GetAll<CourseUnit>(x => x.CourseID == CourseId);

            var categoryList = _tacheyRepository.GetAll<CourseCategory>();

            var detailList = _tacheyRepository.GetAll<CategoryDetail>();

            var result = new StepGroup
            {
                course = currentCourse,
                courseChapter = chapterList,
                courseUnit = unitList,
                courseCategory = categoryList,
                categoryDetails = detailList
            };

            return result;
        }
        //取得課程影片所需欄位
        public Main_Video GetCourseVideoData(string CourseId)
        {
            var course = _tacheyRepository.Get<Models.Course>(x => x.CourseID == CourseId);

            var category = _tacheyRepository.GetAll<CourseCategory>().FirstOrDefault(x => x.CategoryID == course.CategoryID);

            var detail = _tacheyRepository.GetAll<CategoryDetail>().FirstOrDefault(x => x.DetailID == course.CategoryDetailsID);

            var chapter = _tacheyRepository.GetAll<CourseChapter>(x => x.CourseID == CourseId);
            var unit = _tacheyRepository.GetAll<CourseUnit>(x => x.CourseID == CourseId);

            var result = new Main_Video
            {
                CourseID = course.CourseID,
                CourseTitle = course.Title,
                CategoryName = category.CategoryName,
                DetailName = detail.DetailName,
                courseChapters = chapter,
                courseUnits = unit
            };

            return result;
        }
        //開新課程
        public string NewCourseStep(string currentUserId)
        {
            //取得12位數亂碼課程ID
            var CourseId = GetRandomId(12);

            //檢查是否重複課程ID
            while (_tacheyRepository.Get<Models.Course>(x => x.CourseID == CourseId) != null)
            {
                CourseId = GetRandomId(12);
            }

            var result = new Models.Course 
            { 
                CourseID = CourseId, 
                MemberID = currentUserId, 
                CategoryID = 1, 
                CategoryDetailsID = 10001,
                Title = "無標題"
            };

            _tacheyRepository.Create(result);

            _tacheyRepository.SaveChanges();

            return CourseId;
        }
        // 更新開課步驟
        public void UpdateStep(int? id, StepGroup group, FormCollection formCollection, string CourseId)
        {
            var result = _tacheyRepository.Get<Models.Course>(x => x.CourseID == CourseId);

            if (id == 1)
            {
                result.Title = group.course.Title;
                result.Description = group.course.Description;
                result.TitlePageImageURL = group.course.TitlePageImageURL;
                result.MarketingImageURL = group.course.MarketingImageURL;
            }
            else if (id == 2)
            {
                result.Tool = group.course.Tool;
                result.CourseLevel = group.course.CourseLevel;
                result.Effect = group.course.Effect;
                result.CoursePerson = group.course.CoursePerson;
            }
            else if (id == 3 || id == 6)
            {
                group.formCollection = formCollection;
                this.UpdateStepUnit(group.formCollection, CourseId);
            }
            else if (id == 4)
            {
                var detail = _tacheyRepository.Get<CategoryDetail>(x => x.DetailID == group.course.CategoryDetailsID);

                result.OriginalPrice = group.course.OriginalPrice;
                result.PreOrderPrice = group.course.PreOrderPrice;
                result.TotalMinTime = group.course.TotalMinTime;
                if (detail != null)
                {
                    result.CategoryID = detail.CategoryID;
                }
                result.CategoryDetailsID = group.course.CategoryDetailsID;
            }
            else if (id == 5)
            {
                result.Introduction = group.course.Introduction;
            }
            _tacheyRepository.SaveChanges();
        }
        //課程章節新增修改
        public void UpdateStepUnit(FormCollection courseStep, string CourseId)
        {
            var chResult = _tacheyRepository.GetAll<CourseChapter>(x => x.CourseID == CourseId).Select(x => x).ToList();
            var unResult = _tacheyRepository.GetAll<CourseUnit>(x => x.CourseID == CourseId).Select(x => x).ToList();

            _tacheyRepository.DeleteRange(chResult);
            _tacheyRepository.DeleteRange(unResult);

            var count = courseStep.AllKeys.Count();
            for (int i = 1; i < count; i++)
            {
                int chapterCount = 0;
                var arr = courseStep[$"{i}"].Split(',');

                var newUnit = new CourseUnit()
                {
                    CourseID = CourseId
                };

                foreach (var item in arr)
                {
                    var newChapter = new CourseChapter();

                    if (chapterCount == 0)
                    {
                        newChapter.CourseID = CourseId;
                        newChapter.ChapterID = i;
                        newChapter.ChapterName = item;
                        _tacheyRepository.Create(newChapter);
                    }
                    else
                    {
                        newUnit.ChapterID = i;
                        newUnit.UnitID = $"{i}-{chapterCount}";
                        if (chapterCount % 2 == 0)
                        {
                            newUnit.UnitName = item;
                        }
                        else
                        {
                            newUnit.CourseURL = item;
                        }
                    }
                    if (chapterCount != 0 && chapterCount % 2 == 0)
                    {
                        _tacheyRepository.Create(newUnit);
                        newUnit = new CourseUnit();
                    }
                    chapterCount++;
                }
            };
        }
        //取得當前課程評價
        public List<ScoreCard> GetAllScore(string CourseId)
        {
            var score = _tacheyRepository.GetAll<CourseScore>(x=>x.CourseID == CourseId);
            var member = _tacheyRepository.GetAll<Models.Member>();

            var result = from s in score
                         join m in member on s.MemberID equals m.MemberID
                         select new ScoreCard
                         {
                             Name = m.Name,
                             Photo = m.Photo,
                             Score = s.Score,
                             Title = s.Title,
                             ScoreContent = s.ScoreContent,
                             ScoreDate = s.ScoreDate
                         };

            return result.ToList();
        }
        //判斷是否評價過
        public bool Scored(string MemberId, string CourseId)
        {
            return _tacheyRepository.Get<CourseScore>(x => x.MemberID == MemberId && x.CourseID == CourseId) == null ? true : false;
        }
        //Create創建課程評價
        public void CreateScore(CourseScore courseScore, string CourseId, string MemberId)
        {
            var result = new CourseScore()
            {
                CourseID = CourseId,
                MemberID = MemberId,
                Score = courseScore.Score,
                Title = courseScore.Title,
                ScoreContent = courseScore.ScoreContent,
                ToTeacher = courseScore.ToTeacher,
                ToTachey = courseScore.ToTachey,
                ScoreDate = DateTime.Now
            };

            _tacheyRepository.Create(result);
            _tacheyRepository.SaveChanges();
        }
        //取得當前課程發問
        public List<QuestionCard> GetAllQuestions(string MemberId, string CourseId)
        {
            var currentMember = _tacheyRepository.Get<Models.Member>(x => x.MemberID == MemberId);
            var ques = _tacheyRepository.GetAll<Question>(x => x.CourseID == CourseId);
            var ans = _tacheyRepository.GetAll<Answer>(x => x.CourseID == CourseId);
            var member = _tacheyRepository.GetAll<Models.Member>();
            var AnsAmount = from all in ans
                            group all by all.QuestionID into g
                            select new { id = g.Key, amount = g.Count() };

            var allLike = _tacheyRepository.GetAll<QuestionLike>(x => x.CourseID == CourseId);
            var countLike = from all in allLike
                            group all by all.CourseID into g
                            select new { ID = g.Key, Count = g.Count() };

            var allA = from a in ans
                       join q in ques on a.QuestionID equals q.QuestionID
                       join m in member on a.MemberID equals m.MemberID
                       select new AnswerCard
                       {
                           CourseID = a.CourseID,
                           QuestionID = a.QuestionID,
                           Name = m.Name,
                           Photo = m.Photo,
                           AnswerContent = a.AnswerContent,
                           AnswerDate = a.AnswerDate,
                           LikeAmount = 0
                       };
            var allQ = from q in ques
                       join m in member on q.MemberID equals m.MemberID
                       select new QuestionCard
                       {
                           CourseID = q.CourseID,
                           QuestionID = q.QuestionID,
                           Name = m.Name,
                           Photo = m.Photo,
                           CurrentName = currentMember.Name,
                           CurrentPhoto = currentMember.Photo,
                           QuestionContent = q.QuestionContent,
                           QuestionDate = q.QuestionDate,
                           LikeAmount = 0,
                           AnsAmount = 0
                         };
            var result = allQ.ToList();
            var allAList = allA.ToList();

            foreach (var Q in result)
            {
                foreach (var all in AnsAmount)
                {
                    if(all.id == Q.QuestionID)
                    {
                        Q.AnsAmount = all.amount;
                    }
                }
                var list = new List<AnswerCard>();
                foreach (var A in allAList)
                {
                    if(Q.QuestionID == A.QuestionID)
                    {
                        list.Add(A);
                    }
                }
                Q.GetAnswerCards = list;
            }

            return result;
        }
        //Create創建課程發問
        public void CreateQuestion(Question question, string CourseId, string MemberId)
        {
            var result = new Question()
            {
                CourseID = CourseId,
                MemberID = MemberId,
                QuestionContent = question.QuestionContent,
                QuestionDate = DateTime.Now
            };

            _tacheyRepository.Create(result);
            _tacheyRepository.SaveChanges();
        }
        //Create創建課程回答
        public void CreateAnswer(QuestionCard questionCard, string CourseId, int QuestionId, string MemberId)
        {

            var result = new Answer()
            {
                CourseID = CourseId,
                QuestionID = QuestionId,
                MemberID = MemberId,
                AnswerContent = questionCard.PostAnswer.AnswerContent,
                AnswerDate = DateTime.Now
            };

            _tacheyRepository.Create(result);
            _tacheyRepository.SaveChanges();
        }
        //取得自訂位數的亂數方法
        private string GetRandomId(int Length)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            int passwordLength = Length;
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            return new string(chars);
        }
    }
}