
using System;
using System.Web;

namespace SimpleMvc.Identity
{
    public class SimpleAuthentication
    {
        private const string auth_key = "sptk";
        private const string lock_key = "splk";
        private const string cookie_domain = "";
        private const string cookie_path = "/";
        private const int auth_expires = 1;//单位：小时
        private const int lock_expires = 5;//单位：分钟

        private static Func<string, object> getUsercallback;
        private static Func<string, object, bool> isInRole;

        public static void Regist(Func<string, object> func_getUser, Func<string, object, bool> func_isInrole = null)
        {
            getUsercallback = func_getUser;
            isInRole = func_isInrole;
        }


        public static void WriteToken(HttpApplication context)
        {
            if (context.User != null && context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {

                HttpCookie cookie_auth = context.Response.Cookies[auth_key];
                if (cookie_auth != null && !string.IsNullOrEmpty(cookie_auth.Value))
                {
                    if (cookie_auth.Value != context.User.Identity.Name)
                    {
                        cookie_auth.Value = context.User.Identity.Name;
                        cookie_auth.Expires = DateTime.Now.AddDays(auth_expires);
                        context.Response.Cookies.Set(cookie_auth);
                    }
                }
                else
                {
                    cookie_auth = new HttpCookie(auth_key)
                    {
                        Value = context.User.Identity.Name,
                        Domain = cookie_domain,
                        Path = cookie_path,
                        Expires = DateTime.Now.AddDays(auth_expires)
                    };
                    context.Response.Cookies.Add(cookie_auth);
                }

                if (((SimpleIdentity)context.User.Identity).IsLocked)
                {
                    RemoveCookie(context.Context, lock_key);
                }
                else
                {
                    HttpCookie cookie_lock;

                    if (context.Response.Cookies[lock_key] != null)
                    {
                        cookie_lock = context.Response.Cookies[lock_key];
                        cookie_lock.Expires = DateTime.Now.AddMinutes(lock_expires);
                        context.Response.Cookies.Set(cookie_lock);
                    }
                    else
                    {
                        cookie_lock = new HttpCookie(lock_key)
                        {
                            Value = "unlock",
                            Domain = cookie_domain,
                            Path = cookie_path,
                            Expires = DateTime.Now.AddMinutes(lock_expires)
                        };
                        context.Response.AppendCookie(cookie_lock);
                    }
                }
            }
            else
            {
                RemoveCookie(context.Context, auth_key);
                RemoveCookie(context.Context, lock_key);
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
                    bool islock = !(context.Request.Cookies[lock_key] != null && context.Request.Cookies[lock_key].Value == "unlock");
                    SimpleIdentity identity = new SimpleIdentity(token, user, islock);
                    SimplePrincipal principal = new SimplePrincipal(identity, isInRole);

                    context.Context.User = principal;
                }
                else
                {
                    Singout(context.Context);
                }
            }
        }

        public static void Singout(HttpContext context)
        {
            context.User = null;
            RemoveCookie(context, auth_key);
            RemoveCookie(context, lock_key);
        }

        public static bool SignIn(object user, string token)
        {
            if (user != null)
            {
                HttpContext.Current.User = new SimplePrincipal(token, user);
                var cookie_auth = new HttpCookie(auth_key)
                {
                    Value = HttpContext.Current.User.Identity.Name,
                    Domain = cookie_domain,
                    Path = cookie_path,
                    Expires = DateTime.Now.AddDays(auth_expires)
                };
                var cookie_lock = new HttpCookie(lock_key)
                {
                    Value = "unlock",
                    Domain = cookie_domain,
                    Path = cookie_path,
                    Expires = DateTime.Now.AddMinutes(lock_expires)
                };
                HttpContext.Current.Response.AppendCookie(cookie_auth);
                HttpContext.Current.Response.AppendCookie(cookie_lock);
                return true;
            }
            return false;
        }

        public static void LockOut()
        {
            var identity = HttpContext.Current.User.GetSimpleInstance().SimpleIdentity;
            identity.IsLocked = true;
        }

        public static void Unlock()
        {
            var identity = HttpContext.Current.User.GetSimpleInstance().SimpleIdentity;
            identity.IsLocked = false;
        }

        private static void RemoveCookie(HttpContext context, string key)
        {
            if (context.Response.Cookies[key] != null)
            {
                context.Response.Cookies[key].Expires = DateTime.Now.AddDays(-1);
            }
            else
            {
                var cookie = new HttpCookie(key)
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                context.Response.Cookies.Add(cookie);
            }
        }
    }

}
