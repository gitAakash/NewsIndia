using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;
using EntityProject;
using NewsIndiaDAL;

namespace NewsIndiaBAL
{
    public class UserLayout
    {
        /// <summary>
        /// Used to get the list of Menu for Normal/Regular User
        /// </summary>
        /// <returns></returns>
        public static List<Category> GetUserMenuList()
        {
            try
            {

                return LayoutMethods.GetMenuList();

            }
            catch (Exception ex)
            {
                return new List<Category>();
            }
        }


        /// <summary>
        /// Used to get the list of Menu for Normal/Regular User
        /// </summary>
        /// <returns></returns>
        public static List<SideBannerMaster> GetSideBannerList()
        {
            try
            {

                return LayoutMethods.GetSideBanner();

            }
            catch (Exception ex)
            {
                return new List<SideBannerMaster>();
            }
        }
    }
}
