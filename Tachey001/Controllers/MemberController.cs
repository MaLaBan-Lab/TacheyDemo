using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Tachey001.Models;

namespace Tachey001.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private TacheyContext tacheyDb = new TacheyContext();
        // GET: Member
        public ActionResult Console()
        {
            var currentId = User.Identity.GetUserId();

            ViewBag.UserPhoto = tacheyDb.Member.Find(currentId).Photo;

            var courseList = tacheyDb.Course.Where(x => x.MemberID == currentId).Select(x => x).ToList();

            return View(courseList);
        }

        public ActionResult Point()
        {
            return View();
        }

        public ActionResult Setting()
        {
            return View();
        }

        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Coupons()
        {
            return View();
        }

        public ActionResult Invite()
        {
            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }
    }
}