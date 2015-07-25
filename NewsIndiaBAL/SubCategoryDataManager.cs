using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;

namespace NewsIndiaBAL
{
    public class SubCategoryDataManager
    {

        /// <summary>
        /// Used to get the list of all the SubCategories
        /// </summary>
        /// <param name="subCategoryId"></param>
        public static List<SubCategoryDataTableModel> GetSubCategoryDataList(int subCategoryId = 0)
        {
            try
            {
                return NewsIndiaDAL.SubCategoryDataManager.GetSubCategoryDataList(subCategoryId);
            }
            catch (Exception ex)
            {
                return new List<SubCategoryDataTableModel>();
            }
        }

        /// <summary>
        /// Used to Add or Update Sub Category Data
        /// </summary>
        /// <param name="subCategoryDataInfoModel"></param>
        /// <param name="subCategoryDataId"></param>
        /// <returns></returns>
        public static bool AddEditSubCategoryData(
           SubCategoryDataInfoModel subCategoryDataInfoModel, int subCategoryDataId = 0)
        {
            try
            {
                return 
                    NewsIndiaDAL.SubCategoryDataManager.AddEditSubCategoryData(subCategoryDataInfoModel,
                        subCategoryDataId);
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Used to get the sub category data information
        /// </summary>
        /// <param name="subCategoryId"></param>
        /// <returns></returns>
        public static SubCategoryDataInfoModel GetSubCategoryDataInformation(int subCategoryId = 0)
        {
            try
            {
                return NewsIndiaDAL.SubCategoryDataManager.GetSubCategoryDataInformation(subCategoryId);
            }
            catch (Exception ex)
            {
                return new SubCategoryDataInfoModel();
            }
        }
        

        /// <summary>
        /// Used to get the list of the subcategory present under the category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static List<SubCategoryInformation> GetSubCategories(int categoryId)
        {
            try
            {
                return NewsIndiaDAL.SubCategoryDataManager.GetSubCategories(categoryId);
            }
            catch (Exception ex)
            {
                return new List<SubCategoryInformation>();
            }
        }

        /// <summary>
        /// Used to remove the SubCategory
        /// </summary>
        /// <param name="subCategoryDataId"></param>
        /// <returns></returns>
        public static bool RemoveSubCategoryData(int subCategoryDataId)
        {
            try
            {
                return NewsIndiaDAL.SubCategoryDataManager.RemoveSubCategoryData(subCategoryDataId);
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


       /* /// <summary>
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
        }*/

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
