using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;
using EntityProject;
using NewsIndiaDAL;

namespace NewsIndiaBAL
{
    public class SubCategoryList
    {


        /// <summary>
        /// Used to get the latest News to showing in breaking news
        /// </summary>
        /// <returns></returns>
        public static List<SubCategoryModel> GetLatestNews()
        {
            try
            {

                return SubCategoryListDisplay.GetLatestNews();

            }
            catch (Exception ex)
            {
                return new List<SubCategoryModel>();
            }
        }
        /// <summary>
        /// Used to get List of the subCategory(Data)
        /// </summary>
        /// <returns></returns>
        public static List<SubCategoryModel> GetSubCategoryListView(int subCategory = 0)
        {
            try
            {

                return SubCategoryListDisplay.GetSubCategoryListView(subCategory);

            }
            catch (Exception ex)
            {
                return new List<SubCategoryModel>();
            }
        }


        /// <summary>
        /// Used to get Information about SubCategory
        /// </summary>
        /// <param name="subCategoryId"></param>
        /// <returns></returns>
        public static string GetSubCategoryInformation(int subCategoryId = 0)
        {
            try
            {

                return SubCategoryListDisplay.GetSubCategoryInformation(subCategoryId);

            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
