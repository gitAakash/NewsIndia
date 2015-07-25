using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityProject;

namespace BusinessClasses
{
    /// <summary>
    /// Used to Save the Category
    /// </summary>
    public class Category
    {
        public string CategoryName { set; get; }
        public int CategoryId { set; get; }
        public List<SubCategory> SubCategory{set;get;}
    }

    /// <summary>
    /// Used to save the subcategory
    /// </summary>
    public class SubCategory
    {
        public string SubCategoryName { set; get; }
        public int SubCategoryId { set; get; }
    }
}
