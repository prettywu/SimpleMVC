
using System;
using System.Web;

namespace SimpleMvc.Identity
{
    public class SimpleAuthentication
    {
        private const string auth_key = "sptk";
        private const string cookie_domain = "";
        private const string cookie_path = "/";
        private const int cookie_expires = 1;//单位：小时

        private static Func<string, object> getUsercallback;
        private static Func<string,object, bool> isInRole;


        public static void Create(Func<string, object> func_getUser, Func<string, object, bool> func_isInrole=null)
        {
            getUsercallback = func_getUser;
            isInRole = func_isInrole;
        }


        public static void WriteToken(HttpApplication context)
        {
            if (context.User != null && context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                HttpCookie cookie = context.Request.Cookies[auth_key];
                if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
                {
                    if (cookie.Value != context.User.Identity.Name)
                    {
                        cookie.Value = context.User.Identity.Name;

                    }
                }
                else
                {
                    cookie = new HttpCookie(auth_key)
                    {
                        Value = context.User.Identity.Name
                    };
                }
                context.Response.AppendCookie(cookie);
            }
            else
            {
                if (context.Request.Cookies[auth_key] != null)
                    context.Response.Cookies.Remove(auth_key);
            }
        }

        public static void ReadAuthenticateInfo(HttpApplication context)
        {
            HttpCookie cookie = context.Request.Cookies[auth_key];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                string token = cookie.Value;
                object user = null;

                if (getUsercallback != null)
                    user = getUsercallback(token);

                if (user != null)
                {
                    Principal principal = new Principal(token, user, isInRole);
                    context.Context.User = principal;
                }
            }
        }

        public static void Singout()
        {
            HttpContext.Current.User = null;
            HttpContext.Current.Response.Cookies.Remove(auth_key);
        }

        public static bool SignIn(object user, string token)
        {
            if (user != null)
            {
                HttpContext.Current.User = new Principal(token, user);
                var cookie = new HttpCookie(auth_key)
                {
                    Value = HttpContext.Current.User.Identity.Name
                };
                HttpContext.Current.Response.AppendCookie(cookie);
                return true;
            }
            return false;
        }
    }

}
