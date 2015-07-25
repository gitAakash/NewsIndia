using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;

namespace NewsIndiaBAL
{
    public class RoleManager
    {
        /// <summary>
        /// Used to Get all the roles available except super admin
        /// </summary>
        /// <returns></returns>
        public static List<RoleInformation> GetRoles()
        {
            try
            {
                return NewsIndiaDAL.RoleManager.GetRoles();
            }
            catch (Exception ex)
            {
                return new List<RoleInformation>();
            }
        }
    }
}
