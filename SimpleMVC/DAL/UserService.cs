using SimpleMVC.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVC.DAL
{
    public class UserService
    {
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static User GetUserByUserName(string username)
        {
            if (string.IsNullOrEmpty(username)) return null;
            using (var context = new EFDbContext())
            {
                return context.Users.Where(u => username == u.UserName && !u.IsDeleted).FirstOrDefault();
            }
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int AddUser(User user)
        {
            if (user == null) return -1;
            using (var context = new EFDbContext())
            {
                context.Users.Add(user);
                return context.SaveChanges();
            }
        }
    }
}