using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;
using EntityProject;

namespace NewsIndiaDAL
{
    public class SubCategoryDataManager
    {

        /// <summary>
        /// Used to get the list of all the SubCategory Data
        /// </summary>
        /// <param name="subCategoryDataId"></param>
        public static List<SubCategoryDataTableModel> GetSubCategoryDataList(int subCategoryDataId = 0)
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    return
                        nie.SubCategoryDataMasters.Where(m => m.IsActive && (subCategoryDataId == 0 || m.SubCategoryId == subCategoryDataId))
                            .Select(m => new SubCategoryDataTableModel()
                                {
                                    CategoryName = m.SubCategoryMaster.CategoryMaster.CategoryName,
                                    IsCategoryActive = m.SubCategoryMaster.CategoryMaster.IsActive,
                                    IsCategoryVisible = m.SubCategoryMaster.CategoryMaster.IsVisible,
                                    IsVisible = m.IsVisible,
                                    IsSubCategoryActive = m.SubCategoryMaster.IsActive,
                                    IsSubCategoryVisible = m.SubCategoryMaster.IsVisible,
                                    SubCategoryName = m.SubCategoryMaster.SubCategoryName,
                                    SubCategoryDataId = m.ID,
                                    SubCategoryDataTitle = m.Title,
                                    SubCategoryDataShowing = (m.IsVisible && m.SubCategoryMaster.IsActive && m.SubCategoryMaster.IsVisible && m.SubCategoryMaster.CategoryMaster.IsVisible && m.SubCategoryMaster.CategoryMaster.IsActive)

                                }
                            ).ToList();
                }
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
                using (var nie = new NewsIndiaTVEntities())
                {
                    var subCategoryData = new SubCategoryDataMaster();
                    if (subCategoryDataId != 0)
                        subCategoryData = nie.SubCategoryDataMasters.FirstOrDefault(m => m.ID == subCategoryDataId);

                    subCategoryData.IsActive = true;
                    subCategoryData.Title = subCategoryDataInfoModel.Title;
                    subCategoryData.Description = subCategoryDataInfoModel.Description;
                    subCategoryData.IsVisible = subCategoryDataInfoModel.IsVisible;
                    subCategoryData.SubCategoryId = subCategoryDataInfoModel.SubCategoryId;
                    subCategoryData.IsSuperAdminApproved = subCategoryDataInfoModel.IsVisible;


                    if (subCategoryDataId == 0)
                    {
                        subCategoryData.SubmittedDate = DateTime.Now;
                        subCategoryData.SavedTimeStamp = subCategoryDataInfoModel.TimeStamp;
                        nie.SubCategoryDataMasters.Add(subCategoryData);
                    }
                    nie.SaveChanges();


                    if (subCategoryDataInfoModel.UploadedFileNames != null)
                    {
                        var uploadedFiles = new List<SubCategoryDataAttachment>();
                        if (subCategoryDataId != 0)
                        {
                            uploadedFiles =
                                nie.SubCategoryDataAttachments.Where(m => m.SubCategoryDataID == subCategoryDataId && m.IsActive).ToList();
                            foreach (var uploadedFile in uploadedFiles)
                            {
                                uploadedFile.IsActive = false;
                                nie.SaveChanges();
                            }

                        }
                        if(subCategoryDataInfoModel.UploadedFileNames!="")
                        foreach (var fileInfo in subCategoryDataInfoModel.UploadedFileNames.Split(','))
                        {

                            // var fileType=
                            var fileInformation = uploadedFiles.FirstOrDefault(m => m.FileName == fileInfo);
                            if (fileInformation != null)
                                fileInformation.IsActive = true;
                            else
                            {
                                nie.SubCategoryDataAttachments.Add(new SubCategoryDataAttachment()
                                {
                                    FileName = fileInfo,
                                    SubCategoryDataID = subCategoryData.ID,
                                    AttachmentID = Convert.ToInt32(GetAttachmentType(fileInfo)),
                                    IsActive = true,
                                });
                            }
                            nie.SaveChanges();
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Used to get the attachment type of the file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static BusinessClasses.AttachmentType GetAttachmentType(string fileName)
        {
            try
            {
                string fileExt = fileName.Split('.').Last();
                if (fileExt == "jpg" || fileExt == "jpeg" || fileExt == "png")
                    return AttachmentType.Image;

                else if (fileExt == "mp4" || fileExt == "avi" || fileExt == "mov")
                    return AttachmentType.Video;

                else if (fileExt == "mp3")
                    return AttachmentType.Audio;
                else
                    return AttachmentType.Document;


            }
            catch (Exception ex)
            {
                return AttachmentType.Image;
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
                using (var nie = new NewsIndiaTVEntities())
                {
                    var info = nie.SubCategoryDataMasters.FirstOrDefault(m => m.ID == subCategoryId);
                    var subCategoryData = new SubCategoryDataInfoModel()
                    {
                        Description = info.Description,
                        SubCategoryDataId = info.ID,
                        Title = info.Title,
                        IsVisible = info.IsVisible,
                        SubCategoryId = info.SubCategoryId,
                        TimeStamp = info.SavedTimeStamp,
                        CategoryId=info.SubCategoryMaster.CategoryID,
                        Files = info.SubCategoryDataAttachments.Where(m=>m.IsActive).Select(c=>c.FileName).ToList()
                        //Attachments = 
                        // UploadedFileNames = 

                    };
                    foreach (var attachment in info.SubCategoryDataAttachments.Where(m=>m.IsActive))
                        subCategoryData.UploadedFileNames += attachment.FileName.Split('\\')[1] + ",";

                    return subCategoryData;

                }

            }
            catch (Exception ex)
            {
                return new SubCategoryDataInfoModel();
            }
        }


        /// <summary>
        /// Used to get the list of all the subCatgories present under the category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static List<SubCategoryInformation> GetSubCategories(int categoryId)
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    return
                        nie.SubCategoryMasters.Where(m => m.CategoryID == categoryId && m.IsActive)
                            .Select(m => new SubCategoryInformation()
                            {
                                SubCategoryId = m.ID,
                                SubCategoryName = m.SubCategoryName
                            }).ToList();
                }
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
                using (var nie = new NewsIndiaTVEntities())
                {
                    var subCategoryDataInfo = nie.SubCategoryDataMasters.FirstOrDefault(m => m.ID == subCategoryDataId);
                    if (subCategoryDataInfo != null)
                        subCategoryDataInfo.IsActive = false;
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
