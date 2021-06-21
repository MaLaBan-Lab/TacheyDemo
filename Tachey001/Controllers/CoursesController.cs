using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Tachey001.Models;
using Tachey001.Service.Course;
using Tachey001.ViewModel.Course;

namespace Tachey001.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private TacheyContext tacheyDb;
        //宣告CourseService
        private CourseService _courseService;

        //初始化CourseService
        public CoursesController()
        {
            tacheyDb = new TacheyContext();
            _courseService = new CourseService();
        }

        [AllowAnonymous]
        public ActionResult All()
        {
            var result = _courseService.GetCourseData();

            return View(result);
        }

        [AllowAnonymous]
        public ActionResult Group()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Main(int? id, string CourseId)
        {
            if (id == null)
            {
                id = 1;
            }
            ViewBag.Id = id;
            var result = _courseService.GetCourseVideoData(CourseId);

            //if (result.FirstOrDefault() == null)
            //    return RedirectToAction("Index", "Home");

            return View(result);
        }
        //開課10步驟 GET
        public ActionResult Step(int? id, string CourseId)
        {
            //取得當前登入會員ID
            ViewBag.UserId = User.Identity.GetUserId();
            //取得當前開課步驟
            ViewBag.Id = id;
            //取得當前開課的課程ID
            ViewBag.CourseId = CourseId;

            
            var currentCourse = tacheyDb.Course.Find(CourseId);

            var chapterList = tacheyDb.CourseChapter.Where(x => x.CourseID == CourseId).Select(x => x);
            var unitList = tacheyDb.CourseUnit.Where(x => x.CourseID == CourseId).Select(x => x);

            var categoryList = tacheyDb.CourseCategory;
            var detailList = tacheyDb.CategoryDetail;

            var result = new StepGroup 
            {   
                courseChapter = chapterList, 
                courseUnit = unitList, 
                course = currentCourse, 
                courseCategory = categoryList, 
                categoryDetails = detailList 
            };

            return View(result);
        }
        //開課10步驟 POST
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Step(int? id, StepGroup group, Course course, string CourseId)
        {
            var result = tacheyDb.Course.Find(CourseId);
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
            else if (id == 4)
            {
                var detail = tacheyDb.CategoryDetail.Find(course.CategoryDetailsID);

                result.OriginalPrice = course.OriginalPrice;
                result.PreOrderPrice = course.PreOrderPrice;
                result.TotalMinTime = course.TotalMinTime;
                result.CategoryID = detail.CategoryID;
                result.CategoryDetailsID = course.CategoryDetailsID;
            }
            else if (id == 5)
            {
                result.Introduction = group.course.Introduction;
            }

            tacheyDb.SaveChanges();

            return RedirectToAction("Step", "Courses", new { id = (id + 1), CourseID = CourseId });
        }
        //創新課程，加入課程ID
        public ActionResult NewCourseStep()
        {
            var CourseId = GetRandomId(12);
            var currentUserId = User.Identity.GetUserId();

            while (tacheyDb.Course.Find(CourseId) != null)
            {
                CourseId = GetRandomId(12);
            }

            Course newCourse = new Course { CourseID = CourseId, MemberID = currentUserId };

            tacheyDb.Course.Add(newCourse);

            tacheyDb.SaveChanges();

            tacheyDb.Dispose();

            return RedirectToAction("Step", "Courses", new { id = 0, CourseId = CourseId });
        }
        //課程章節新增修改
        [HttpPost]
        public ActionResult StepUnit(int? id, FormCollection course, string CourseId)
        {
            var chResult = tacheyDb.CourseChapter.Where(x => x.CourseID == CourseId).Select(x => x).ToList();
            var unResult = tacheyDb.CourseUnit.Where(x => x.CourseID == CourseId).Select(x => x).ToList();

            tacheyDb.CourseChapter.RemoveRange(chResult);
            tacheyDb.CourseUnit.RemoveRange(unResult);

            var count = course.AllKeys.Count();
            for (int i = 1; i < count; i++)
            {
                int chapterCount = 0;
                var arr = course[$"{i}"].Split(',');

                var newUnit = new CourseUnit();

                foreach (var item in arr)
                {
                    var newChapter = new CourseChapter();

                    if (chapterCount == 0)
                    {
                        newChapter.CourseID = CourseId;
                        newChapter.ChapterID = i;
                        newChapter.ChapterName = item;
                        tacheyDb.CourseChapter.Add(newChapter);
                    }
                    else
                    {
                        newUnit.CourseID = CourseId;
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
                    if (chapterCount % 2 == 0)
                    {
                        tacheyDb.CourseUnit.Add(newUnit);
                        newUnit = new CourseUnit();
                    }
                    chapterCount++;
                }
            };

            tacheyDb.SaveChanges();

            if (id == 3)
            {
                return RedirectToAction("Step", "Courses", new { id = 4, CourseId = CourseId });
            }
            return RedirectToAction("Step", "Courses", new { id = 7, CourseId = CourseId });
        }
        [HttpPost]
        public ActionResult Step8(string CourseId)
        {

            return RedirectToAction("Step", "Courses", new { id = 8, CourseId = CourseId });
        }
        [HttpPost]
        public ActionResult Step9(string CourseId)
        {

            return RedirectToAction("Step", "Courses", new { id = 9, CourseId = CourseId });
        }
        //完成課程，送出審核
        public ActionResult StepFinish(string CourseId)
        {
            var result = tacheyDb.Course.Find(CourseId);

            result.CreateDate = DateTime.Now;
            result.CreateFinish = true;

            tacheyDb.SaveChanges();

            return RedirectToAction("Console", "Member");
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