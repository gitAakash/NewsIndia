using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;
using EntityProject;

namespace NewsIndiaDAL
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
                using (var nie = new NewsIndiaTVEntities())
                {
                    return
                        nie.SubCategoryMasters.Where(m => m.IsActive && (categoryId == 0 || m.CategoryID == categoryId))
                            .Select(m => new SubCategoryTableModel()
                                {
                                    SubCategoryName = m.SubCategoryName,
                                    IsVisible = m.IsVisible,
                                    SubCategoryId = m.ID,
                                    CategoryName = m.CategoryMaster.IsActive?m.CategoryMaster.CategoryName:"-",
                                    SubCategoryShowing = (m.IsVisible && m.CategoryMaster.IsVisible && m.CategoryMaster.IsActive),
                                    IsCategoryVisible = m.CategoryMaster.IsVisible,
                                    IsCategoryActive = m.CategoryMaster.IsActive
                                }
                            ).ToList();
                }
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
                using (var nie = new NewsIndiaTVEntities())
                {
                    var subCategoryInfo = nie.SubCategoryMasters.FirstOrDefault(m => m.ID == subCategoryId);
                    if (subCategoryInfo != null)
                        subCategoryInfo.IsActive = false;
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
        /// Used to get the information of the SubCategory
        /// </summary>
        /// <returns></returns>
        public static SubCategoryMaster GetSubCategoryInfo(int subCategoryId = 0)
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    var subCategoryInfo= nie.SubCategoryMasters.FirstOrDefault(m => m.ID == subCategoryId && m.IsActive);
                    if (subCategoryInfo != null && !subCategoryInfo.CategoryMaster.IsActive)
                        subCategoryInfo.CategoryID = 0;
                    return subCategoryInfo;
                }
            }
            catch (Exception ex)
            {
                return new SubCategoryMaster();
            }
        }

        /// <summary>
        /// Used to get the active categories
        /// </summary>
        /// <returns></returns>
        public static
        List<ActiveCategory> GetActiveCategories()
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    return
                        nie.CategoryMasters.Where(m => m.IsActive)
                            .Select(m => new ActiveCategory() { CategoryName = m.CategoryName, CategoryId = m.ID })
                            .ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<ActiveCategory>();
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
                using (var nie = new NewsIndiaTVEntities())
                {
                    return
                        nie.SubCategoryMasters.Any(
                            m =>
                                m.SubCategoryName == subCategoryName && (subCategoryId == 0 || m.ID != subCategoryId) &&
                                m.CategoryMaster.ID == categoryId && m.IsActive);
                }
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
                using (var nie = new NewsIndiaTVEntities())
                {
                    var subCategoryMaster = nie.SubCategoryMasters.FirstOrDefault(m => m.ID == subCategoryId);

                    if (subCategoryMaster != null)
                    {
                        subCategoryMaster.SubCategoryName = subCategoryName;
                        subCategoryMaster.IsVisible = isVisible;
                        subCategoryMaster.CategoryID = categoryId;
                    }
                    else
                    {
                        subCategoryMaster = new SubCategoryMaster()
                            {
                                SubCategoryName = subCategoryName,
                                IsVisible = isVisible,
                                IsActive = true,
                                CategoryID = categoryId
                            };
                        nie.SubCategoryMasters.Add(subCategoryMaster);
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
    }
}
