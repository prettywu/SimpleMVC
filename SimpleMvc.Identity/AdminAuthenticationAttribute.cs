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
    class AdminAuthenticationAttribute : AuthorizeAttribute, IAuthenticationFilter
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
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if(filterContext.Result!=null&&filterContext.Result)
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            
            if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                if (AuthorizeCore(filterContext.HttpContext))
                {
                    //HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
                    //cache.SetProxyMaxAge(new TimeSpan(0L));
                    //cache.AddValidationCallback(new HttpCacheValidateHandler(this.CacheValidateHandler), null);
                }
                else
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
        }
    }
}
