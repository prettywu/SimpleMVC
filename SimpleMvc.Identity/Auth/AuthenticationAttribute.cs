using System;
using System.Configuration;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace SimpleMvc.Identity
{
    public class AuthenticationAttribute : FilterAttribute, IAuthenticationFilter
    {
        public bool DisLock { get; set; }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                IPrincipal iprincipal = filterContext.Principal;
                if (!iprincipal.Identity.IsAuthenticated)
                {
                    filterContext.Result = new RedirectResult("/Test/Login");
                }
                else if (!DisLock && iprincipal.GetSimpleInstance().SimpleIdentity.IsLocked)
                {
                    filterContext.Result = new RedirectResult("/Test/Lock");
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
