using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessClasses;
using EntityProject;
using NewsIndia.Models;
using NewsIndiaBAL;

namespace NewsIndia
{
    public class CookieManager
    {
        /// <summary>
        /// Used to set the User Information in cookie
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static HttpCookie SetUserInfoCookie(Login userInfo)
        {
            var userInfoCookie = new HttpCookie("USER_INFORMATION");
            userInfoCookie["LoginUserName"] = userInfo.UserName;
            userInfoCookie["LoginPassword"] = userInfo.Password;
            userInfoCookie["RememberMe"] = userInfo.RememberMe ? "true" : "false";
            userInfoCookie.Expires = DateTime.Now.Add(new TimeSpan(10, 0, 0, 0));
            return userInfoCookie;
        }



        /// <summary>
        /// Used to set the User Information from cookie
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Login GetUserInfoFromCookie(HttpRequestBase request)
        {
            HttpCookie userInfoCookie = request.Cookies["USER_INFORMATION"];

            if (userInfoCookie != null)
                return new Login()
                {
                    UserName = userInfoCookie["LoginUserName"],
                    Password = userInfoCookie["LoginPassword"],
                    RememberMe = userInfoCookie["RememberMe"] == "true"
                };

            return null;

        }

        /// <summary>
        /// Used to clear cookie
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static void ClearCookie(HttpRequestBase request, HttpCookieCollection collection)
        {
            HttpCookie userInfoCookie = request.Cookies["USER_INFORMATION"];

            if (userInfoCookie != null)
                collection["USER_INFORMATION"].Expires = DateTime.Now.AddDays(-1);

        }


    }
}