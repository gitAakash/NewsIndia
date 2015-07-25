using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;
using EntityProject;

namespace NewsIndiaDAL
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
                using (var nie = new NewsIndiaTVEntities())
                {
                    return
                        nie.UserRoles.Where(m => m.Id != 1 && m.IsActive)
                            .Select(m => new RoleInformation() { RoleId = m.Id, RoleName = m.Name }).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<RoleInformation>();
            }
        }
    }
}
