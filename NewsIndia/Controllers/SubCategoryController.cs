using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using NewsIndia.AuthFilters;
using NewsIndiaBAL;

namespace NewsIndia.Controllers
{
    [UserAuth]
    public class SubCategoryController : Controller
    {
        /// <summary>
        /// Used to display the SubCategory Data List
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int subCategoryId = 0)
        {
            var subCategoryList = SubCategoryList.GetSubCategoryListView(subCategoryId);
            ViewBag.SubCategoryName = SubCategoryList.GetSubCategoryInformation(subCategoryId);
            return View(subCategoryList);
        }
    }
}