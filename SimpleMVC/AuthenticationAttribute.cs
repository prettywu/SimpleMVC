using SimpleMVC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace SimpleMVC
{
    public class AuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter,IAuthorizationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            //IPrincipal identity = filterContext.Principal;
            //if (!identity.Identity.IsAuthenticated)
            //{
            //    filterContext.Result= new HttpUnauthorizedResult();
            //}
            

            if (!AuthManager.ReadAuthInfo())
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