﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Tachey001.AccountModels;
using Tachey001.Models;
using Tachey001.Repository;
using Tachey001.ViewModel.Course;

namespace Tachey001.Service
{
    public class CourseService
    {
        //宣告資料庫邏輯
        private TacheyRepository _tacheyRepository;

        //初始化資料庫邏輯
        public CourseService()
        {
            _tacheyRepository = new TacheyRepository(new TacheyContext());
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
            var course = _tacheyRepository.Get<Course>(x => x.CourseID == CourseId);

            var category = _tacheyRepository.Get<CourseCategory>(x => x.CategoryID == course.CategoryID);

            var detail = _tacheyRepository.Get<CategoryDetail>(x => x.DetailID == course.CategoryDetailsID);

            var chapter = _tacheyRepository.GetAll<CourseChapter>(x => x.CourseID == CourseId).ToList();

            var unit = _tacheyRepository.GetAll<CourseUnit>(x => x.CourseID == CourseId).ToList();

            var result = new Main_Video
            {
                Course = course,
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
                Title = "無標題",
                MainClick = 0,
                CustomClick = 0
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
                result.MarketingImageURL = group.course.MarketingImageURL;
            }
            else if (id == 2)
            {
                result.Tool = group.course.Tool;
                result.CourseLevel = group.course.CourseLevel;
                result.Effect = group.course.Effect;
                result.CoursePerson = group.course.CoursePerson;
            }
            else if (id == 3)
            {
                group.formCollection = formCollection;
                this.UpdateStepUnit(group.formCollection, CourseId);
            }
            else if (id == 4)
            {
                result.OriginalPrice = group.course.OriginalPrice;
                result.PreOrderPrice = group.course.PreOrderPrice;
                result.TotalMinTime = group.course.TotalMinTime;
            }
            else if (id == 5)
            {
                result.Introduction = group.course.Introduction;
            }
            else if(id == 7)
            {
                result.CustomUrl = group.course.CustomUrl;
            }
            else if (id == 8)
            {
                result.LecturerIdentity = group.course.LecturerIdentity;
            }
            _tacheyRepository.SaveChanges();
        }
        //取得對應課程分類名稱
        public List<CourseCateDet> courseCateDet(string CourseId)
        {
            var currCourse = _tacheyRepository.GetAll<Models.Course>(x => x.CourseID == CourseId);
            var category = _tacheyRepository.GetAll<CourseCategory>();
            var detail = _tacheyRepository.GetAll<CategoryDetail>();

            var result = from c in currCourse
                         join cat in category on c.CategoryID equals cat.CategoryID
                         join det in detail on c.CategoryDetailsID equals det.DetailID
                         select new CourseCateDet
                         {
                             CourseID = c.CourseID,
                             CategoryID = c.CategoryID,
                             DetailID = c.CategoryDetailsID,
                             CategoryName = cat.CategoryName,
                             DetailName = det.DetailName
                         };

            return result.ToList();
        }
        //修改課程分類
        public void ChangeCategory(string clickedOption, string CourseId)
        {
            var detail = _tacheyRepository.Get<CategoryDetail>(x => x.DetailName == clickedOption);
            var result = _tacheyRepository.Get<Models.Course>(x => x.CourseID == CourseId);
            result.CategoryID = detail.CategoryID;
            result.CategoryDetailsID = detail.DetailID;
            _tacheyRepository.Update(result);
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

                foreach (var item in arr)
                {
                    if (chapterCount == 0)
                    {
                        var newChapter = new CourseChapter()
                        {
                            CourseID = CourseId,
                            ChapterID = i,
                            ChapterName = item
                        };
                        _tacheyRepository.Create(newChapter);
                    }
                    else
                    {
                        var newUnit = new CourseUnit() 
                        { 
                            CourseID = CourseId,
                            ChapterID = i,
                            UnitID = $"{i}-{chapterCount}",
                            UnitName = item
                        };
                        _tacheyRepository.Create(newUnit);
                    }
                    chapterCount++;
                }
            };
        }
        //取得當前課程評價
        public List<ScoreCard> GetAllScore(string CourseId)
        {
            var score = _tacheyRepository.GetAll<CourseScore>(x => x.CourseID == CourseId);
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
            var currentMember = new Models.Member { Name = "匿名", Photo = "https://pbs.twimg.com/profile_images/1391260770864967680/ZqdvCZM7_400x400.jpg" };
            if (MemberId != null)
            {
                currentMember = _tacheyRepository.Get<Models.Member>(x => x.MemberID == MemberId);
            }
            var ques = _tacheyRepository.GetAll<Question>(x => x.CourseID == CourseId);
            var ans = _tacheyRepository.GetAll<Answer>(x => x.CourseID == CourseId);
            var member = _tacheyRepository.GetAll<Models.Member>();
            var AnsAmount = from all in ans
                            group all by all.QuestionID into g
                            select new { id = g.Key, amount = g.Count() };

            //取得問題喜歡數
            var allQLike = _tacheyRepository.GetAll<QuestionLike>(x => x.CourseID == CourseId);

            //取得回答喜歡數
            var allALike = _tacheyRepository.GetAll<AnswerLike>(x => x.CourseID == CourseId);

            var allA = from a in ans
                       join q in ques on a.QuestionID equals q.QuestionID
                       join m in member on a.MemberID equals m.MemberID
                       select new AnswerCard
                       {
                           CourseID = a.CourseID,
                           QuestionID = a.QuestionID,
                           AnswerID = a.AnswerID,
                           Name = m.Name,
                           Photo = m.Photo,
                           AnswerContent = a.AnswerContent,
                           AnswerDate = a.AnswerDate
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
                           AnsAmount = 0
                         };
            var result = allQ.ToList();
            var allAList = allA.ToList();

            foreach (var Q in result)
            {
                var Qlist = new List<string>();
                //問題按讚的所有會員
                foreach (var Ql in allQLike)
                {
                    if (Q.QuestionID == Ql.QuestionID)
                    {
                        Qlist.Add(Ql.MemberID);
                    }
                }
                Q.AllMemberId = Qlist;

                //回答的總人數
                foreach (var all in AnsAmount)
                {
                    if(all.id == Q.QuestionID)
                    {
                        Q.AnsAmount = all.amount;
                    }
                }
                var list = new List<AnswerCard>();
                //回答List塞入
                foreach (var A in allAList)
                {
                    if(Q.QuestionID == A.QuestionID)
                    {
                        list.Add(A);
                        var Alist = new List<string>();
                        //回答按讚的所有會員
                        foreach (var Al in allALike)
                        {
                            if (A.QuestionID == Al.QuestionID && A.AnswerID == Al.AnswerID)
                            {
                                Alist.Add(Al.MemberID);
                            }
                        }
                        A.AllMemberId = Alist;
                    }
                }
                Q.GetAnswerCards = list;
            }
            return result;
        }
        //Create課程問題點讚
        public void CreateQLike(string MemberId, string CourseId, int QuestionId)
        {
            var ToF = _tacheyRepository.Get<QuestionLike>(x => x.MemberID == MemberId && x.CourseID == CourseId && x.QuestionID == QuestionId);
            if (ToF == null)
            {
                var result = new QuestionLike { MemberID = MemberId, CourseID = CourseId, QuestionID = QuestionId };
                _tacheyRepository.Create(result);
            }
            else
            {
                _tacheyRepository.Delete(ToF);
            }
            _tacheyRepository.SaveChanges();
        }
        //Create課程回答點讚
        public void CreateALike(string MemberId, string CourseId, int QuestionId, int AnswerId)
        {
            var ToF = _tacheyRepository.Get<AnswerLike>(x => x.MemberID == MemberId && x.CourseID == CourseId && x.QuestionID == QuestionId && x.AnswerID == AnswerId);
            if (ToF == null)
            {
                var result = new AnswerLike { MemberID = MemberId, CourseID = CourseId, QuestionID=QuestionId, AnswerID = AnswerId };
                _tacheyRepository.Create(result);
            }
            else
            {
                _tacheyRepository.Delete(ToF);
            }
            _tacheyRepository.SaveChanges();
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
        //儲存雲端上傳課程封面圖片，並回傳網址
        public string PostFileStorage(string CourseId, HttpPostedFileBase file)
        {
            var _cloudinary = Credientials.Init();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription("TitlePageImage", file.InputStream),
                PublicId = "TitlePageImage",
                Folder = $"Course/{CourseId}"
            };

            var CallBackUrl = _cloudinary.Upload(uploadParams).SecureUrl.ToString();

            var result = _tacheyRepository.Get<Course>(x => x.CourseID == CourseId);
            result.TitlePageImageURL = CallBackUrl;
            _tacheyRepository.SaveChanges();

            return CallBackUrl;
        }
        //儲存雲端上傳影片，並回傳網址
        public string PostVideoStorage(string CourseId, HttpPostedFileBase file)
        {
            var _cloudinary = Credientials.Init();

            var uploadParams = new VideoUploadParams()
            {
                File = new FileDescription("PreviewVideo", file.InputStream),
                PublicId = "PreviewVideo",
                Folder = $"Course/{CourseId}"
            };

            var CallBackUrl = _cloudinary.UploadLarge(uploadParams).SecureUrl.ToString();

            var result = _tacheyRepository.Get<Models.Course>(x => x.CourseID == CourseId);
            result.PreviewVideo = CallBackUrl;
            _tacheyRepository.SaveChanges();

            return CallBackUrl;
        }
        //取得GetCourseId
        public string GetCourseId(string Id)
        {
            var result = _tacheyRepository.Get<Models.Course>(x => x.CustomUrl == Id);

            if (result == null)
            {
                return "Index";
            }
            return result.CourseID;
        }
        //從Custom進入，點擊率+1
        public void AddCustomClick(string CourseId)
        {
            var result = _tacheyRepository.Get<Models.Course>(x => x.CourseID == CourseId);
            result.CustomClick += 1;
            _tacheyRepository.Update(result);
            _tacheyRepository.SaveChanges();
        }
        //從Main進入，點擊率+1
        public void AddMainClick(string CourseId)
        {
            var result = _tacheyRepository.Get<Models.Course>(x => x.CourseID == CourseId);
            result.MainClick += 1;
            _tacheyRepository.Update(result);
            _tacheyRepository.SaveChanges();
        }
        //判斷客製網址是否重複
        public bool CheckUrl(string Url, string CourseId)
        {
            var result = _tacheyRepository.Get<Models.Course>(x => x.CustomUrl == Url && x.CourseID != CourseId);
            return result == null ? false : true;
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
        //取得是否擁有課程
        public bool GetOwner(string MemberId, string CourseId)
        {
            var result = _tacheyRepository.Get<Course>(x => x.CourseID == CourseId);
            if (result.MemberID == MemberId) return true;
            var oID = _tacheyRepository.Get<Order>(x => x.MemberID == MemberId);
            if (oID == null) return false;
            var oD = _tacheyRepository.Get<Order_Detail>(x => x.OrderID == oID.OrderID && x.CourseID == CourseId); ;
            return oD == null ? false : true;
        }
        //取得是否加入購物車
        public bool GetCarted(string MemberId, string CourseId)
        {
            var ToF = _tacheyRepository.Get<ShoppingCart>(x => x.MemberID == MemberId && x.CourseID == CourseId);
           
            return ToF == null ? false : true;
        }
    }
}