using SimpleMVC.DAL;
using SimpleMVC.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SimpleMVC.BLL
{
    public class UserManager
    {
        public static User LoginPasswordCheck(string username, string password)
        {
            var user = UserService.GetUserByUserName(username);
            if (user == null) return null;
            if (Common.MD5encryption(password) == user.PasswordHash)
            {
                return user;
            }
            return null;
        }
        
        public static bool WriteLoginInfo(Login login)
        {
            return LoginService.AddLogin(login) > 0;
        }

        public static List<Role> GetUserRoles(Guid userId)
        {
            return RoleService.GetUserRoles(userId);
        }
    }
}