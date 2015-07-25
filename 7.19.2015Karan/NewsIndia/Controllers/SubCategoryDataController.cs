using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsIndiaBAL;

namespace NewsIndia.Controllers
{
    public class SubCategoryDataController : Controller
    {
        //
        // GET: /SubCategoryData/
        public ActionResult Details(int subCategoryId = 0)
        {
            var subCategoryData = SubCategoryData.GetSubCategoryData(subCategoryId);
            //  ViewBag.SubCategoryName = SubCategoryList.GetSubCategoryInformation(subCategoryId);
            return View(subCategoryData);
        }
    }
}