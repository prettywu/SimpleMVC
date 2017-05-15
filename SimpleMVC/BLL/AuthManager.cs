using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVC.BLL
{
    public class AuthManager
    {
        public static void Singout()
        {
            HttpContext.Current.Response.Cookies.Remove("sptk");
        }

        public static bool SignIn(string token)
        {
            return true;
        }
    }
}