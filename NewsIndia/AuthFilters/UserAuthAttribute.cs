using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace NewsIndia.AuthFilters
{
    public class UserAuthAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            SessionManager.GetMenuInSessionUser();
        }
    }

    public class AdminAuth : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (SessionManager.GetSessionInfo() == null || SessionManager.GetSessionInfo().RoleId == 3)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                HttpContext.Current.Response.Redirect("~/Account/NewsIndiaAdminLogin");
            }

        }
    }

}
