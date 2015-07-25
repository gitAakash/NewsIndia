using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;
using EntityProject;

namespace NewsIndiaDAL
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
                using (var nie = new NewsIndiaTVEntities())
                {
                
                    var userInfo=
                        nie.UserMasters.FirstOrDefault(
                            m => m.Email == loginInfo.UserName && m.Password == loginInfo.Password && m.IsActive);
                    
                    return userInfo;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
