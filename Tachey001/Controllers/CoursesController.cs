using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Tachey001.Models;

namespace Tachey001.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private TacheyContext tacheyDb = new TacheyContext();

        [AllowAnonymous]
        public ActionResult All()
        {
            var UserId = User.Identity.GetUserId();
            ViewBag.UserId = UserId;

            var allCourse = tacheyDb.Course.ToList();

            return View(allCourse);
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
        public ActionResult Main(int? id)
        {
            if (id == null)
            {
                id = 1;
            }
            ViewBag.Id = id;
            return View();
        }
        public ActionResult Step(int? id, string CourseId)
        {
            ViewBag.UserId = User.Identity.GetUserId();

            ViewBag.Id = id;
            ViewBag.CourseId = CourseId;

            var result = tacheyDb.Course.Find(CourseId);

            return View(result);
        }
        public string GetRandomId(int Length)
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
        public ActionResult DeleteCourse(string id)
        {
            var result = tacheyDb.Course.Find(id);

            tacheyDb.Course.Remove(result);

            tacheyDb.SaveChanges();

            tacheyDb.Dispose();

            return RedirectToAction("Console", "Member");
        }
        public ActionResult Step0()
        {
            var CourseId = GetRandomId(12);
            var currentUserId = User.Identity.GetUserId();

            while(tacheyDb.Course.Find(CourseId) != null)
            {
                CourseId = GetRandomId(12);
            }

            Course newCourse = new Course { CourseID = CourseId, MemberID = currentUserId };

            tacheyDb.Course.Add(newCourse);

            tacheyDb.SaveChanges();

            tacheyDb.Dispose();

            return RedirectToAction("Step", "Courses", new { id = 0, CourseId = CourseId });
        }
        public ActionResult Step1(string CourseId)
        {
            return RedirectToAction("Step", "Courses", new { id = 1, CourseId = CourseId });
        }
        [HttpPost]
        public ActionResult Step2(string Title, string Description, string TitlePageImageURL, string MarketingImageURL, string CourseId)
        {
            var result = tacheyDb.Course.Find(CourseId);

            result.Title = Title;
            result.Description = Description;
            result.TitlePageImageURL = TitlePageImageURL;
            result.MarketingImageURL = MarketingImageURL;

            tacheyDb.SaveChanges();

            return RedirectToAction("Step", "Courses", new { id = 2, CourseId = CourseId });
        }
        [HttpPost]
        public ActionResult Step3(string Tool, string CourseLevel, string Effect, string CoursePerson, string CourseId)
        {
            var result = tacheyDb.Course.Find(CourseId);

            result.Tool = Tool;
            result.CourseLevel = CourseLevel;
            result.Effect = Effect;
            result.CoursePerson = CoursePerson;

            tacheyDb.SaveChanges();

            return RedirectToAction("Step", "Courses", new { id = 3, CourseId = CourseId });
        }
        [HttpPost]
        public ActionResult Step4(FormCollection course, string CourseId)
        {
            var chapter = tacheyDb.CourseChapter;
            var unit = tacheyDb.CourseUnit;

            ViewBag.count = course.AllKeys;

            return View();

            //if(chapter.Any(x=>x.CourseID == CourseId))
            //{
            //    var chapterList = chapter.Where(x => x.CourseID == CourseId).Select(x => x);
            //    var unitList = unit.Where(x => x.CourseID == CourseId).Select(x => x);
            //}
            //else
            //{
            //    var newChapter = new CourseChapter { CourseID = CourseId , ChapterID = }
            //}

            //return RedirectToAction("Step", "Courses", new { id = 4, CourseId = CourseId });
        }
        [HttpPost]
        public ActionResult Step5(decimal OriginalPrice, decimal PreOrderPrice, int TotalMinTime, int CategoryDetailsID, string CourseId)
        {
            var result = tacheyDb.Course.Find(CourseId);

            var detail = tacheyDb.CategoryDetail.Find(CategoryDetailsID);

            result.OriginalPrice = OriginalPrice;
            result.PreOrderPrice = PreOrderPrice;
            result.TotalMinTime = TotalMinTime;
            result.CategoryID = detail.CategoryID;
            result.CategoryDetailsID = CategoryDetailsID;

            tacheyDb.SaveChanges();

            return RedirectToAction("Step", "Courses", new { id = 5, CourseId = CourseId });
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Step6(string Introduction, string CourseId)
        {
            var result = tacheyDb.Course.Find(CourseId);

            result.Introduction = Introduction;

            tacheyDb.SaveChanges();

            return RedirectToAction("Step", "Courses", new { id = 6, CourseId = CourseId });
        }
        [HttpPost]
        public ActionResult Step7(string CourseId)
        {

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
        public ActionResult StepFinish(string CourseId)
        {
            var result = tacheyDb.Course.Find(CourseId);

            result.CreateDate = DateTime.Now;
            result.CreateFinish = true;

            tacheyDb.SaveChanges();

            return RedirectToAction("Console", "Member");
        }
    }
}