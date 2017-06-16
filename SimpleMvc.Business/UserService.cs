using SimpleMvc.DAL;
using SimpleMvc.Entitys;
using SimpleMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace SimpleMvc.Business
{
    public class UserService : BaseService
    {
        public static User GetUserByToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;
            using (var context = new EFDbContext())
            {
                var login = context.Logins.Where(l => l.Id == new Guid(token)).FirstOrDefault();
                if (login != null)
                {
                    return context.Users.Where(u => u.Id == login.UserId).FirstOrDefault();
                }
                return null;
            }
        }

        public static bool IsInRole(string roles, object user)
        {
            User _user = user as User;
            List<Role> _roles;
            using (var context = new EFDbContext())
            {
                var userroles = context.UserRoles.Where(ur => _user.Id == ur.UserId && !ur.IsDeleted);
                _roles = context.Roles.Where(r => userroles.Select(ur => ur.RoleId).Contains(r.Id)).ToList();
            }
            if (_roles.Any())
            {
                var intersect = _roles.Select(r => r.RoleName).Intersect(roles.Split(new char[] { ',' }));
                if (intersect.Any())
                    return true;
            }
            return false;
        }

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
            if (string.IsNullOrEmpty(username)) return null;
            using (var context = new EFDbContext())
            {
                return context.Users.Where(u => username == u.UserName && !u.IsDeleted).FirstOrDefault();
            }
        }


        public Login GetLoginByLoginId(string loginId)
        {
            if (string.IsNullOrEmpty(loginId)) return null;
            using (var context = new EFDbContext())
            {
                return context.Logins.Where(l => l.Id == new Guid(loginId)).FirstOrDefault();
            }
        }

        public User LoginPasswordCheck(string username, string password)
        {
            if (string.IsNullOrEmpty(username)) return null;
            User user = null;
            using (var context = new EFDbContext())
            {
                user = context.Users.Where(u => username == u.UserName && !u.IsDeleted).FirstOrDefault();
            }
            if (user == null) return null;
            if (Helper.MD5encryption(password) == user.PasswordHash)
            {
                return user;
            }
            return null;
        }

        /// <summary>
        /// 写入登录信息
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool WriteLoginInfo(Login login)
        {
            if (login == null) return false;
            using (var context = new EFDbContext())
            {
                context.Logins.Add(login);
                return context.SaveChanges() > 0;
            }
        }

        public List<Role> GetUserRoles(Guid userId)
        {
            if (userId == null) return null;
            using (var context = new EFDbContext())
            {
                var userroles = context.UserRoles.Where(ur => userId == ur.UserId && !ur.IsDeleted);
                var roles = context.Roles.Where(r => userroles.Select(ur => ur.RoleId).Contains(r.Id));
                return roles.ToList();
            }
        }

        public bool AddUser(User user)
        {
            if (user == null) return false;
            using (var context = new EFDbContext())
            {
                context.Users.Add(user);
                return context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="nickname"></param>
        /// <param name="role"></param>
        /// <param name="state"></param>
        /// <param name="sortname"></param>
        /// <param name="sorttype"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<User> GetUserList(string username, string nickname, int state,string role, string sortname, int sorttype, int pageindex, int pagesize, out int total)
        {
            Expression<Func<User, bool>> where = u => 1 == 1;
            if (!string.IsNullOrEmpty(username))
            {
                where = where.And(u => u.UserName.Contains(username));
            }
            if (!string.IsNullOrEmpty(nickname))
            {
                where = where.And(u => u.NickName.Contains(nickname));
            }
            if (state != -1)
            {
                where = where.And(u => u.State == state);
            }
            if (!string.IsNullOrEmpty(role))
            {
                where = where.And(u => u.UserRoles.Select(ur => ur.Role).Select(r => r.RoleName).Any(n => n.Contains(role)));
            }

            OrderModelField[] order = new OrderModelField[]
            {
                new OrderModelField
                {
                     propertyName=sortname,
                      isDesc=sorttype==0?true:false
                }
            };

            return GetPageList(where, order, new string[] { "UserRoles.Role" }, pageindex, pagesize, out total);


        }


    }
}
