using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityProject;

namespace NewsIndiaDAL
{
    public static class AuthenticationLinkManager
    {
        /// <summary>
        /// used to create authentication link for the authentication
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="authenticationTypeId"></param>
        /// <returns></returns>
        public static bool CreateAuthenticationLink(string uid, int authenticationTypeId, int userId)
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    nie.AuthenticationLinks.Add(new AuthenticationLink()
                    {
                        IsUsed = false,
                        UnqiueLink = uid,
                        UserId = userId,
                        VerificationTypeId = authenticationTypeId
                    });
                    nie.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
