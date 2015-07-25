using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsIndia.AuthFilters;
using NewsIndia.Models;

namespace NewsIndia.Controllers
{

    /// <summary>
    /// Implemented for the various funtionality of Admin
    /// </summary>
    /// 
    [UserAuth]
    [AdminAuth]
    public class CategoryManagerController : Controller
    {
        #region ManageCategory
        /// <summary>
        /// Used to Manager the Category
        /// </summary>
        /// <returns></returns>
        public ActionResult CategoryMaster()
        {
            return View();
        }

        /// <summary>
        /// Used to get the detail of a category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCategoryInfo(int categoryId)
        {
            //return Json(new { CategoryInformation = NewsIndiaBAL.AdminMethods.GetCategoryInfo(Convert.ToInt32(categoryId)) },
            //    JsonRequestBehavior.AllowGet);
            var categoryInfo = NewsIndiaBAL.CategoryManager.GetCategoryInfo(categoryId);
            var categoryInformation = new CategoryMasterModel()
            {
                CategoryName = categoryInfo.CategoryName,
                IsVisible = categoryInfo.IsVisible
            };
            return Json(new { CategoryInformation = categoryInformation }, JsonRequestBehavior.AllowGet);
        }

        //Used to Display the CategoryTable
        public ActionResult ShowCategoryTable()
        {
            var categories = NewsIndiaBAL.CategoryManager.GetAllCategory();
            return View(categories.Select(m => new CategoryMasterModel() { CategoryName = m.CategoryName, ID = m.ID, IsActive = m.IsActive, IsVisible = m.IsVisible }).ToList());
        }

        /// <summary>
        /// Used to save the category Information
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="categoryName"></param>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveCategoryInfo(int categoryId, string categoryName, bool isVisible)
        {
            var saveCategoryData = NewsIndiaBAL.CategoryManager.SaveCategoryInfo(categoryId, categoryName, isVisible);
            MenuMaster.LoadMenu();
            return Json(new { CategorySaved = saveCategoryData }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// used to remove category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public ActionResult RemoveCategory(int categoryId)
        {
            var removeCategoryData = NewsIndiaBAL.CategoryManager.RemoveCategory(categoryId);
            MenuMaster.LoadMenu();
            return Json(new { IsCategoryRemoved = removeCategoryData }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to check if the category Name is Already present
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckCategoryName(string categoryName, int categoryId)
        {
            return Json(new { IsCategoryPresent = NewsIndiaBAL.CategoryManager.CheckCategoryName(categoryName, categoryId) }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}