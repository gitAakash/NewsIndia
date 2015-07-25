using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessClasses;
using EntityProject;
using NewsIndiaBAL;

namespace NewsIndia
{
    public class SessionManager
    {

        /// <summary>
        /// Used to checkif 
        /// </summary>
        /// <returns></returns>
        public static bool IsAdminLoggedIn()
        {
            try
            {
                var sessionInfo = GetSessionInfo() ?? new UserMaster() { RoleId = 3 };
                return sessionInfo.RoleId == 2 || sessionInfo.RoleId == 1;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Used to Set The User Information in the Session
        /// </summary>
        /// <param name="usreInfo"></param>
        /// <returns></returns>
        public static bool SetSessionInfo(UserMaster usreInfo)
        {
            if (HttpContext.Current.Session["USER_INFORMATION"] == null)
            {
                HttpContext.Current.Session.Add("USER_INFORMATION", usreInfo);
            }
            return true;
        }

        /// <summary>
        /// Used to Set The User Information in the Session
        /// </summary>
        /// <returns></returns>
        public static bool RemoveSessionInfo()
        {
            HttpContext.Current.Session["USER_INFORMATION"] = null;

            return true;
        }

        /// <summary>
        /// Used to Get The User Information from the Session
        /// </summary>
        /// <returns></returns>
        public static UserMaster GetSessionInfo()
        {
            return HttpContext.Current.Session["USER_INFORMATION"] == null ? null : (UserMaster)HttpContext.Current.Session["USER_INFORMATION"];
        }

        /// <summary>
        /// Used to get the logged in UserName
        /// </summary>
        /// <returns></returns>
        public static string GetLoggedInUserName()
        {
            var userInfo = GetSessionInfo();
            if (userInfo == null)
                return "";
            return userInfo.FirstName;
        }


        /// <summary>
        /// Used to load the Menu In the Session
        /// </summary>
        public static void GetMenuInSessionUser()
        {
            if (HttpContext.Current.Session["MENU_INFORMATION"] == null)
            {
                HttpContext.Current.Session.Add("MENU_INFORMATION", UserLayout.GetUserMenuList());
                HttpContext.Current.Session.Add("MENU_SIDEBANNER_INFORMATION", UserLayout.GetSideBannerList());
            }
            else
            {
                HttpContext.Current.Session["MENU_INFORMATION"] = UserLayout.GetUserMenuList();
                HttpContext.Current.Session.Add("MENU_SIDEBANNER_INFORMATION", UserLayout.GetSideBannerList());
            }
        }

        /// <summary>
        /// Used to get the Menu Items from Session
        /// </summary>
        /// <returns></returns>
        public static List<Category> GetMenuItemFromSession()
        {
            return (List<Category>)HttpContext.Current.Session["MENU_INFORMATION"];
        }

        /// <summary>
        /// Used to get the Menu Items from Session
        /// </summary>
        /// <returns></returns>
        public static List<SideBannerMaster> GetSideBarMenuItemFromSession()
        {
            return (List<SideBannerMaster>)HttpContext.Current.Session["MENU_SIDEBANNER_INFORMATION"];
        }

    }
}