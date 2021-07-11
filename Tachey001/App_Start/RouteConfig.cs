using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tachey001
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //講師行銷Url
            routes.MapRoute(
                name: "FindCustomCourseUrl",
                url: "Course/cm/{id}",
                defaults: new { controller = "Courses", action = "Custom", id = "Index" }
            );

            //課程影片Url
            routes.MapRoute(
                name: "FindCourseVideoUrl",
                url: "Course/mn/{id}",
                defaults: new { controller = "Courses", action = "Main", id = "Index" }
            );

            //課程分類Url
            routes.MapRoute(
                name: "FindCategoryCourse",
                url: "Course/Category/{category}",
                defaults: new { controller = "Courses", action = "FindCategory", category = "All" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
