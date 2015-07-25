using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;
using EntityProject;

namespace NewsIndiaDAL
{
    public class SubCategoryData
    {
        public static SubCategoryModel GetSubCategoryData(int subCategoryDataId = 0)
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {

                    //var subCategoryData = nie.SubCategoryDataMasters.FirstOrDefault(m => m.ID == subCategoryDataId && m.IsVisible);
                    //var categoryDataModel = new SubCategoryModel()
                    //{
                    //    Title = subCategoryData.Title,
                    //    Description = subCategoryData.Description,
                    //    SubCategoryDataId = subCategoryData.ID,
                    //    Attachments = new List<SubCategoryAttachment>()
                    //};



                    //foreach (var attachment in nie.SubCategoryDataAttachments.Where(m => m.SubCategoryDataID == subCategoryData.ID))
                    //{
                    //    var attachmentInfo= new SubCategoryAttachment()
                    //    {
                    //        FileName = attachment.FileName,
                    //        FileType = (AttachmentType)attachment.AttachmentID,
                    //        SubCategoryAttachmentId = attachment.ID
                    //    };
                    //    categoryDataModel.Attachments.Add(attachmentInfo);

                    //}
                    //return categoryDataModel;
                    var subcategoryInfo =
                                         nie.SubCategoryDataMasters.FirstOrDefault(m => m.ID == subCategoryDataId && m.IsVisible);
                    return new SubCategoryModel()
                    {
                        Description = subcategoryInfo.Description,
                        SubCategoryDataId = subcategoryInfo.ID,
                        Title = subcategoryInfo.Title,
                        Attachments = subcategoryInfo.SubCategoryDataAttachments.Select(m => new SubCategoryAttachment()
                        {
                            FileName = m.FileName,
                            FileType = (AttachmentType)m.AttachmentID,
                            SubCategoryAttachmentId = m.ID
                        }).ToList()
                    };


                }
            }
            catch (Exception ex)
            {
                return new SubCategoryModel();
            }
        }
    }
}
