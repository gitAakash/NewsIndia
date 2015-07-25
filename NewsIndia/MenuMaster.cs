using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;
using NewsIndiaBAL;

namespace NewsIndia
{
    /// <summary>
    /// Used to handle the Menu Master
    /// </summary>
    public static class MenuMaster
    {
        private static List<Category> _menuInfo;

        static MenuMaster()
        {
            _menuInfo = new List<Category>();
        }

        /// <summary>
        /// Used to load the Menu
        /// </summary>
        public static void LoadMenu()
        {
            try
            {
                _menuInfo = UserLayout.GetUserMenuList();
            }
            catch (Exception)
            {
                
            }
        }

        /// <summary>
        /// Used to get the Menu for the role
        /// </summary>
        /// <returns></returns>
        public static List<Category> GetMenuForLayout()
        {
            try
            {
                if (_menuInfo == null)
                    LoadMenu();

                if (_menuInfo != null)
                    return _menuInfo;

                return new List<Category>();

            }
            catch (Exception)
            {
                return new List<Category>();
            }

        }

    }
}
