using SimpleMVC.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVC.DAL
{
    public class LoginService
    {
        public static int AddLogin(Login entity)
        {
            if (entity == null) return -1;
            using(var context=new EFDbContext())
            {
                context.Logins.Add(entity);
                return context.SaveChanges();
            }
        }
    }
}