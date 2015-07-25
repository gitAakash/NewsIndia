using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessClasses
{
    public class SubCategoryDataInfoModel
    {
        public int SubCategoryDataId { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public List<HttpPostedFileBase> Attachments { set; get; }
        public int SubCategoryId { set; get; }
        public int CategoryId { set; get; }
        public string UploadedFileNames { get; set; }
        public bool IsVisible { set; get; }
        public string TimeStamp { set; get; }
        public List<string> Files { set; get; }

        //public SubCategoryDataInfoModel()
        //{
        // //   Attachments = new List<HttpPostedFileBase>();
        //}

    }
}
