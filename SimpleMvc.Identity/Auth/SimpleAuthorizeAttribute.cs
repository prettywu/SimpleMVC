using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SimpleMvc.Identity
{
    public class SimpleAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                IPrincipal user = filterContext.HttpContext.User;
                if (!user.Identity.IsAuthenticated) //未认证
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Admin/Unauthenticate.cshtml",
                    };
                    filterContext.HttpContext.Response.StatusCode = 401;
                }
                else if (!AuthorizeCore(filterContext.HttpContext)) //未授权
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Admin/Unauthorized.cshtml",
                    };
                    filterContext.HttpContext.Response.StatusCode = 403;
                }
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            IPrincipal user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }
            //if ((this._usersSplit.Length > 0) && !this._usersSplit.Contains<string>(user.Identity.Name, StringComparer.OrdinalIgnoreCase))
            //{
            //    return false;
            //}
            if (!string.IsNullOrEmpty(Roles) && !user.IsInRole(Roles))
            {
                return false;
            }
            return true;
        }
    }
}
