using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;
using NewsIndiaDAL;

namespace NewsIndiaBAL
{
    public class SubCategoryData
    {
        public static SubCategoryModel GetSubCategoryData(int subCategoryId)
        {
            try
            {
                return NewsIndiaDAL.SubCategoryData.GetSubCategoryData(subCategoryId);
            }
            catch (Exception ex)
            {
                return new SubCategoryModel();
            }
        }

    }
}
