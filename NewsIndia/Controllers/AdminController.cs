using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsIndia.AuthFilters;

namespace NewsIndia.Controllers
{

    /// <summary>
    /// Implemented for the various funtionality of Admin
    /// </summary>
    /// 
    [UserAuth]
    [AdminAuth]
    public class AdminController : Controller
    {
        /// <summary>
        /// Used to Manager the Category
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowCategory()
        {
           // return View();
            return null;
        }
    }
}