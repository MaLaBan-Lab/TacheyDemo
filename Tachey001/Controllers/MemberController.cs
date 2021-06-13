using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tachey001.Models;

namespace Tachey001.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {

        // GET: Member
        public ActionResult Console()
        {
            return View();
        }

        public ActionResult Point()
        {
            var UserId = User.Identity.GetUserId();

            using (TacheyContext context = new TacheyContext())
            {
                var result = context.Member.Find(User.Identity.GetUserId());

                if (result.Point == null)
                {
                    ViewBag.totalPoint = 0;
                }
                else
                {
                    ViewBag.totalPoint = result.Point;
                }

                //var point_histo = context.Point.Where(x => x.MemberID);

                //ViewBag.point_histo = point_histo;
            }

            return View();
        }

        public ActionResult Setting()
        {
            var UserId = User.Identity.GetUserId();

            using (TacheyContext context = new TacheyContext())
            {
                var result = context.Member.Find(User.Identity.GetUserId());

                if (result.Photo != null)
                {
                    ViewBag.photoUrl = result.Photo;
                }
                else
                {
                    ViewBag.photoUrl = "/Assets/img/photo.png";
                }
                if (result.Name != null)
                {
                    ViewBag.name = result.Name;
                }
                else
                {
                    ViewBag.name = "無名氏";
                }

                List<string> linek_lists = new List<string>();

                if (result.Sex == null || result.Birthday == null) 
                {
                    linek_lists.Add("性別生日");
                }
                if (result.Profession == null)
                {
                    linek_lists.Add("職業行業");
                }
                //if (result.EmailStatus == null)
                //{
                //    linek_lists.Add("信箱驗證");
                //}
                if (result.Interest == null)
                {
                    linek_lists.Add("個人興趣");
                }
                if (result.Like == null)
                {
                    linek_lists.Add("個人喜好");
                }
                if (result.CountryRegion == null)
                {
                    linek_lists.Add("所在地區");
                }
                ViewBag.link = linek_lists;

                ViewBag.account = result.Account;
                ViewBag.fb = result.Facebook;
                ViewBag.google = result.Google;
                ViewBag.Line = result.Line;

                ViewBag.email = result.Email;
                ViewBag.emailStatus = result.EmailStatus;
                ViewBag.sex = result.Sex;

                if (result.Birthday != null)
                {
                    DateTime birthday = DateTime.ParseExact(result.Birthday.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    ViewBag.birthday = birthday;
                }
                else
                {
                    ViewBag.birthday = null;
                }
                

                //context.SaveChanges();
            }

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