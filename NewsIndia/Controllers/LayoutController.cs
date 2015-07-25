using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsIndia.AuthFilters;

namespace NewsIndia.Controllers
{
    [UserAuth]
    public class LayoutController : Controller
    {
        // GET: Layout
        public ActionResult LoadLayoutMenu()
        {
            return View();
        }
    }
}