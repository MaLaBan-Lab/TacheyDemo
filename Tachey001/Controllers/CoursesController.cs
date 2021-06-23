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

            var video = _courseService.GetCourseVideoData(CourseId);

            var result = new MainGroup()
            {
                Main_Video = video
            };

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

            var result = _courseService.GetStepGroup(CourseId);

            return View(result);
        }
        //開課10步驟 POST
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Step(int? id, StepGroup group, FormCollection formCollection, string CourseId)
        {
            _courseService.UpdateStep(id, group, formCollection, CourseId);

            return RedirectToAction("Step", "Courses", new { id = (id + 1), CourseID = CourseId });
        }
        //創新課程，加入課程ID
        public ActionResult NewCourseStep()
        {
            //取得當前會員ID
            var currentUserId = User.Identity.GetUserId();

            //創建課程，並回傳課程ID
            var returnCourseId = _courseService.NewCourseStep(currentUserId);

            //導向開課步驟，並傳入課程ID路由
            return RedirectToAction("Step", "Courses", new { id = 0, CourseId = returnCourseId });
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
    }
}