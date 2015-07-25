using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessClasses
{
    public class SubCategoryTableModel
    {
        public int SubCategoryId { set; get; }
        public string SubCategoryName { set; get; }
        public bool IsVisible { set; get; }
        public string CategoryName { set; get; }
        public bool IsCategoryVisible { set; get; }
        public bool IsCategoryActive { set; get; }
        public bool SubCategoryShowing { set; get; }
    }
}
