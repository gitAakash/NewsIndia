using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityProject;

namespace NewsIndiaDAL
{
    public class CategoryManager
    {
        #region ManageCategory
        /// <summary>
        /// Used to get the information of all the Category present
        /// </summary>
        /// <returns></returns>
        public static List<CategoryMaster> GetAllCategory()
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    return nie.CategoryMasters.Where(m => m.IsActive).ToList();
                }
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
                using (var nie = new NewsIndiaTVEntities())
                {
                    var categoryInfo = nie.CategoryMasters.FirstOrDefault(m => m.ID == categoryId);
                    if (categoryInfo != null)
                        categoryInfo.IsActive = false;
                    nie.SaveChanges();

                }
                return true;
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
        public static bool CheckCategoryName(string categoryName,int categoryId)
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    return nie.CategoryMasters.Any(m => m.IsActive && m.CategoryName == categoryName && m.ID != categoryId);
                }
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
                using (var nie = new NewsIndiaTVEntities())
                {
                    return nie.CategoryMasters.FirstOrDefault(m => m.IsActive && m.ID == categoryId);
                }
            }
            catch (Exception ex)
            {
                return new CategoryMaster();
            }
        }

        /// <summary>
        /// used to save the category info in the database
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="categoryName"></param>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        public static bool SaveCategoryInfo(int categoryId, string categoryName, bool isVisible)
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    var categoryMaster = nie.CategoryMasters.FirstOrDefault(m => m.ID == categoryId);

                    if (categoryMaster != null)
                    {
                        categoryMaster.CategoryName = categoryName;
                        categoryMaster.IsVisible = isVisible;
                    }
                    else
                    {
                        categoryMaster = new CategoryMaster()
                            {
                                CategoryName = categoryName,
                                IsVisible = isVisible,
                                IsActive = true
                            };
                        nie.CategoryMasters.Add(categoryMaster);
                    }
                    nie.SaveChanges();
                  
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
