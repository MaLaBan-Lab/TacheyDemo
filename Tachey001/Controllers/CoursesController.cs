using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tachey001.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        [AllowAnonymous]
        public ActionResult All()
        {
            return View();
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

        public ActionResult Step(int? id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}