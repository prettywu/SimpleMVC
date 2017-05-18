using SimpleMVC.Common;
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
        public User GetUserByUserId(Guid userId)
        {
            if (userId.Equals(Guid.Empty)) return null;
            using (var context = new EFDbContext())
            {
                return context.Users.Where(u => u.Id == userId).FirstOrDefault();
            }
        }

        public User GetUserByUserName(string username)
        {
            return UserService.GetUserByUserName(username);
        }

        public static User GetTokenByToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;
            using (var context = new EFDbContext())
            {
                var login= context.Logins.Where(l => l.Id == new Guid(token)).FirstOrDefault();
                if (login != null)
                {
                    return context.Users.Where(u => u.Id == login.UserId).FirstOrDefault();
                }
                return null;
            }
            
        }

        public Login GetLoginByLoginId(string loginId)
        {
            if (string.IsNullOrEmpty(loginId)) return null;
            using (var context=new EFDbContext())
            {
                return context.Logins.Where(l => l.Id == new Guid(loginId)).FirstOrDefault();
            }
        }

        public User LoginPasswordCheck(string username, string password)
        {
            var user = UserService.GetUserByUserName(username);
            if (user == null) return null;
            if (MvcHelper.MD5encryption(password) == user.PasswordHash)
            {
                return user;
            }
            return null;
        }
        
        public bool WriteLoginInfo(Login login)
        {
            return LoginService.AddLogin(login) > 0;
        }

        public List<Role> GetUserRoles(Guid userId)
        {
            return RoleService.GetUserRoles(userId);
        }

        public bool RegistNewUser(User user)
        {
            return UserService.AddUser(user) > 0;
        }


    }
}