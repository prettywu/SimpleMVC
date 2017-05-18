
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


        public static void Create(Func<string, object> func_getUser)
        {
            getUsercallback = func_getUser;
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

                Principal principal = new Principal(token, user);
                context.Context.User = principal;

            }
        }
    }

}
