using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessClasses
{
    public class SubCategoryDataTableModel
    {
        public int SubCategoryDataId { set; get; }
        public string SubCategoryDataTitle { set; get; }
        public bool IsVisible { set; get; }
        public string CategoryName { set; get; }
        public string SubCategoryName { set; get; }
        public bool IsCategoryVisible { set; get; }
        public bool IsCategoryActive { set; get; }
        public bool IsSubCategoryVisible { set; get; }
        public bool IsSubCategoryActive { set; get; }
        public bool SubCategoryDataShowing { set; get; }
    }
}
