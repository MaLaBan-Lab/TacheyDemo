using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tachey001.Controllers
{
    public class StepController : Controller
    {
        // GET: Step
        public ActionResult Index(int? id)
        {


            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}