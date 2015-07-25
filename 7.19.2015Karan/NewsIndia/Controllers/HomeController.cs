using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityProject;
using NewsIndia.AuthFilters;
using NewsIndiaBAL;

namespace NewsIndia.Controllers
{
    [UserAuth]
    public class HomeController : Controller
    {
        /// <summary>
        /// Used to show the Home page of the website
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
           return View(NewsIndiaBAL.SubCategoryList.GetLatestNews());
        }
        public ActionResult Details(int Id = 0)
        {
            if (Id == 0)
                RedirectToAction("Index");

            var data = SubCategoryData.GetSubCategoryData(Id);
            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}