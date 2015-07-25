using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;
using EntityProject;

namespace NewsIndiaBAL
{
    public class SubCategoryManager
    {
        /// <summary>
        /// Used to get the list of all the SubCategories
        /// </summary>
        /// <param name="categoryId"></param>
        public static List<SubCategoryTableModel> GetSubCategoryList(int categoryId = 0)
        {
            try
            {
                return NewsIndiaDAL.SubCategoryManager.GetSubCategoryList(categoryId);
            }
            catch (Exception ex)
            {
                return new List<SubCategoryTableModel>();
            }
        }

        /// <summary>
        /// Used to remove the SubCategory
        /// </summary>
        /// <param name="subCategoryId"></param>
        /// <returns></returns>
        public static bool RemoveSubCategory(int subCategoryId)
        {
            try
            {
                return NewsIndiaDAL.SubCategoryManager.RemoveSubCategory(subCategoryId);
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        /// <summary>
        /// Used to save the subCategory Information
        /// </summary>
        /// <param name="subCategoryId"></param>
        /// <param name="subCategoryName"></param>
        /// <param name="isVisible"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static bool SaveSubCategoryInfo(int subCategoryId, string subCategoryName, bool isVisible, int categoryId)
        {

            try
            {
                return NewsIndiaDAL.SubCategoryManager.SaveSubCategoryInfo(subCategoryId, subCategoryName, isVisible, categoryId);
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// Used to get the information of the SubCategory
        /// </summary>
        /// <returns></returns>
        public static SubCategoryMaster GetSubCategoryInfo(int subCategoryId = 0)
        {
            try
            {
                return NewsIndiaDAL.SubCategoryManager.GetSubCategoryInfo(subCategoryId);
            }
            catch (Exception ex)
            {
                return new SubCategoryMaster();
            }
        }

        /// <summary>
        /// Used to check if the Subcategory Already exist
        /// </summary>
        /// <param name="subCategoryName"></param>
        /// <param name="subCategoryId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static bool CheckSubCategoryName(string subCategoryName, int subCategoryId, int categoryId)
        {
            try
            {
                return NewsIndiaDAL.SubCategoryManager.CheckSubCategoryName(subCategoryName, subCategoryId, categoryId);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Used to get the active categories
        /// </summary>
        /// <returns></returns>
        public static List<ActiveCategory> GetActiveCategories()
        {
            try
            {
                return NewsIndiaDAL.SubCategoryManager.GetActiveCategories();
            }
            catch (Exception ex)
            {
                return new List<ActiveCategory>();
            }
        }
    }
}
