using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace SimpleMvc.Identity
{
    public class AdminAuthenticationAttribute : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                IPrincipal identity = filterContext.Principal;
                if (!identity.Identity.IsAuthenticated)
                {
                    filterContext.Result = new RedirectResult("/Admin/Login");
                }
            }

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
        }

    }
}
