using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;
using Common;
using EntityProject;
using NewsIndiaDAL;

namespace NewsIndiaBAL
{
    public class Account
    {
        /// <summary>
        /// Used to get List of the subCategory
        /// </summary>
        /// <returns></returns>
        public static UserMaster GetLoginInfo(LoginInfo loginInfo)
        {
            try
            {
                loginInfo.Password = new EncryptionHelper().Encrypt(loginInfo.Password);
                loginInfo.UserName = loginInfo.UserName.ToUpper();
                var userInfo= NewsIndiaDAL.Account.GetLoginInfo(loginInfo);
                userInfo.FirstName = new CultureInfo("en-US", false).TextInfo.ToTitleCase(userInfo.FirstName);
                return userInfo;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
