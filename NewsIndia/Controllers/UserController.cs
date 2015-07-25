using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessClasses;
using Common;
using NewsIndia.AuthFilters;
using NewsIndiaBAL;
using Newtonsoft.Json;

namespace NewsIndia.Controllers
{
 
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ActiveUserInfo = NewsIndiaBAL.SubCategoryManager.GetActiveCategories();
            return View();
        }

      
    }
}