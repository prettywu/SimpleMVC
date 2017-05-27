using System;
using System.Configuration;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace SimpleMvc.Identity
{
    public class AuthenticationAttribute : FilterAttribute, IAuthenticationFilter, IAuthorizationFilter
    {
        //认证
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            if(!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                IPrincipal identity = filterContext.Principal;
                if (!identity.Identity.IsAuthenticated)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
            
        }

        //质询
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            
        }

        //授权
        public void OnAuthorization(AuthorizationContext filterContext)
        {
           
        }

       
        
        
    }
}
