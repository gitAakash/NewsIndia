using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsIndiaBAL
{
    public static class AuthenticationLinkManager
    {
        /// <summary>
        /// Used to create authentication link
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="authenticationType"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool CreateAuthenticationLink(string uid, int authenticationType, int userId)
        {
            try
            {

                return NewsIndiaDAL.AuthenticationLinkManager.CreateAuthenticationLink(uid, authenticationType, userId);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
