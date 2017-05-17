using SimpleMvc.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SimpleMvc.Identity
{
    class AuthenticationManager
    {
        private const string auth_key = "spmt";
        private static Func<string, IUser<string>> getUsercallback;


        public static void Create(Func<string, IUser<string>> func_getUser)
        {
            getUsercallback = func_getUser;
        }
        public static void Create<TKey>()
        {

        }


        public void SignIn()
        {

        }

        public static void ReadAuthenticateInfo(HttpContext context)
        {
            HttpCookie cookie = context.Request.Cookies[auth_key];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                string token = cookie.Value;
                IUser<string> user = getUsercallback(token);
                
            }
        }
    }
    
}
