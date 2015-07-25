using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsIndia.AuthFilters;
using NewsIndia.Models;

namespace NewsIndia.Controllers
{
    [UserAuth]
    [AdminAuth]
    public class SubCategoryManagerController : Controller
    {
        // GET: SubCategoryManager
        public ActionResult SubCategoryMaster()
        {
            ViewBag.ActiveCategoryInfo = NewsIndiaBAL.SubCategoryManager.GetActiveCategories();
            return View();
        }

        /// <summary>
        /// Used to display the table of the SubCategory
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowSubCategoryTable()
        {
            var subCategories = NewsIndiaBAL.SubCategoryManager.GetSubCategoryList();
            return View(subCategories);
        }

        /// <summary>
        /// Used to check if the category Name is Already present
        /// </summary>
        /// <param name="subCategoryName"></param>
        /// <param name="subCategoryId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckSubCategoryName(string subCategoryName, int subCategoryId, int categoryId)
        {
            return Json(new { IsSubCategoryPresent = NewsIndiaBAL.SubCategoryManager.CheckSubCategoryName(subCategoryName, subCategoryId, categoryId) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to save the Subcategory Information
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="subCategoryName"></param>
        /// <param name="isVisible"></param>
        /// <param name="subCategoryId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveSubCategoryInfo(int subCategoryId, string subCategoryName, bool isVisible, int categoryId)
        {
            var saveSubCategoryData = NewsIndiaBAL.SubCategoryManager.SaveSubCategoryInfo(subCategoryId, subCategoryName,
                isVisible, categoryId);
            MenuMaster.LoadMenu();
            return Json(new { SubCategorySaved = saveSubCategoryData }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to get information about the SubCategory
        /// </summary>
        /// <param name="subCategoryId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSubCategoryInfo(int subCategoryId)
        {
            //return Json(new { CategoryInformation = NewsIndiaBAL.AdminMethods.GetCategoryInfo(Convert.ToInt32(categoryId)) },
            //    JsonRequestBehavior.AllowGet);
            var subCategoryInfo = NewsIndiaBAL.SubCategoryManager.GetSubCategoryInfo(subCategoryId);

            var subCategoryInformation = new SubCategoryMasterModel()
            {
                SubCategoryName = subCategoryInfo.SubCategoryName,
                IsVisible = subCategoryInfo.IsVisible,
                CategoryId = subCategoryInfo.CategoryID
            };

            return Json(new { SubCategoryInformation = subCategoryInformation }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// used to remove category
        /// </summary>
        /// <param name="subCategoryId"></param>
        /// <returns></returns>
        public ActionResult RemoveSubCategory(int subCategoryId)
        {
            var removeSubCategoryData = NewsIndiaBAL.SubCategoryManager.RemoveSubCategory(subCategoryId);
            MenuMaster.LoadMenu();
            return Json(new { IsSubCategoryRemoved = removeSubCategoryData }, JsonRequestBehavior.AllowGet);
        }
    }
}