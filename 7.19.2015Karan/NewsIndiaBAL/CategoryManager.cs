using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityProject;

namespace NewsIndiaBAL
{
    public class CategoryManager
    {
        /// <summary>
        /// Used to get the information of all the Category present
        /// </summary>
        /// <returns></returns>
        public static List<CategoryMaster> GetAllCategory()
        {
            try
            {
                return NewsIndiaDAL.CategoryManager.GetAllCategory();
            }
            catch (Exception ex)
            {
                return new List<CategoryMaster>();
            }
        }

        /// <summary>
        /// Used to remove the Category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static bool RemoveCategory(int categoryId)
        {
            try
            {
                return NewsIndiaDAL.CategoryManager.RemoveCategory(categoryId);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Used to check if the category Name is Already present
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static bool CheckCategoryName(string categoryName, int categoryId)
        {
            try
            {
                return NewsIndiaDAL.CategoryManager.CheckCategoryName(categoryName, categoryId);

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Used to get the information of the Category
        /// </summary>
        /// <returns></returns>
        public static CategoryMaster GetCategoryInfo(int categoryId = 0)
        {
            try
            {
                return NewsIndiaDAL.CategoryManager.GetCategoryInfo(categoryId);
            }
            catch (Exception ex)
            {
                return new CategoryMaster();
            }
        }

        /// <summary>
        /// Used to save the Category Information
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="categoryName"></param>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        public static bool SaveCategoryInfo(int categoryId, string categoryName, bool isVisible)
        {
            try
            {
                return NewsIndiaDAL.CategoryManager.SaveCategoryInfo(categoryId, categoryName, isVisible);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
