using System;
using System.Configuration;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace SimpleMvc.Identity
{
    public class AuthenticationAttribute : FilterAttribute, IAuthenticationFilter
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
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Admin/Unauthenticate.cshtml",
                    };
                    filterContext.HttpContext.Response.StatusCode = 401;
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
