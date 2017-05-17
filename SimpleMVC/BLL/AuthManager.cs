using SimpleMVC.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SimpleMVC.BLL
{
    public class AuthManager
    {
        private const string cookie_name = "sptk";
        private const string cookie_domain = "";
        private const string cookie_path = "/";
        private const int cookie_expires = 1;//单位：小时

        public static void Singout()
        {
            HttpContext.Current.Response.Cookies.Remove(cookie_name);
        }

        public static bool SignIn(User user, string token)
        {
            if (user != null)
            {
                MvcIdentity mvcIdentity = new MvcIdentity(user, token);
                HttpContext.Current.User = new MvcPrincipal(mvcIdentity);
                HttpCookie cookie = new HttpCookie(cookie_name)
                {
                    Domain = cookie_domain,
                    Expires = DateTime.Now.AddHours(cookie_expires),
                    Path = cookie_path,
                    Value = token
                };
                HttpContext.Current.Response.Cookies.Add(cookie);
                return true;
            }
            return false;
        }

        public static bool ReadAuthInfo()
        {
            var cookie = HttpContext.Current.Request.Cookies[cookie_name];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                var userManager = new UserManager();

                var loign = userManager.GetLoginByLoginId(cookie.Value);
                if (loign != null)
                {
                    var user = userManager.GetUserByUserId(loign.UserId);
                    if (user != null)
                    {
                        MvcIdentity mvcIdentity = new MvcIdentity(user, loign.Id.ToString());
                        HttpContext.Current.User = new MvcPrincipal(mvcIdentity);
                        HttpContext.Current.Response.Cookies.Add(new HttpCookie(cookie_name)
                        {
                            Domain = cookie_domain,
                            Expires = DateTime.Now.AddHours(cookie_expires),
                            Path = cookie_path,
                            Value = loign.Id.ToString()
                        });
                        return true;
                    }
                }
            }
            HttpContext.Current.Response.Cookies.Remove(cookie_name);
            return false;
        }
    }
}