using SimpleMVC.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVC.DAL
{
    public class RoleService
    {
        public static List<Role> GetUserRoles(Guid userId)
        {
            if (userId == null) return null;
            using(var context=new EFDbContext())
            {
                var userroles = context.UserRoles.Where(ur => userId == ur.UserId && !ur.IsDeleted);
                var roles = context.Roles.Where(r => userroles.Select(ur => ur.RoleId).Contains(r.Id));
                return roles.ToList();
            }
        }
    }
}