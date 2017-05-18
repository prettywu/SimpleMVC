using System;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace SimpleMvc.Identity
{
    public class AuthenticationAttribute :ActionFilterAttribute, IAuthenticationFilter, IAuthorizationFilter
    {
        
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            IPrincipal identity = filterContext.Principal;
            if (!identity.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            
        }
    }
}
