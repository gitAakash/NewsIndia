using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityProject;

namespace BusinessClasses
{
    /// <summary>
    /// Used to Get the SubCategoryList
    /// </summary>
    public class SubCategoryModel
    {
        public int SubCategoryDataId { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public List<SubCategoryAttachment> Attachments { set; get; }
    }

    /// <summary>
    /// Used to Get the subcategory Attachments
    /// </summary>
    public class SubCategoryAttachment
    {
        public int SubCategoryAttachmentId { set; get; }
        public string FileName { set; get; }
        public AttachmentType FileType { set; get; }
    }
}
