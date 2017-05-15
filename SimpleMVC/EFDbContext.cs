using SimpleMVC.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using static SimpleMVC.Entitys.Enums;

namespace SimpleMVC
{
    public class EFDbContext : DbContext
    {
        public DbSet<User> Users;
        public DbSet<Role> Roles;
        public DbSet<Login> Logins;
        public DbSet<UserRole> UserRoles;
    }

    public class DbInitilize : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            var user = new User
            {
                UserName = "yoyo@qq.com",
                PasswordHash = "528888",
                NickName = "yoyo",
                Birthday = new DateTime(1990, 12, 14),
                Gender = (int)Gender.Female,
                RegistTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                HeadImage = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1495443056&di=49d076e75e1e4b4b6e5aaf2977e7626c&imgtype=jpg&er=1&src=http%3A%2F%2Fv1.qzone.cc%2Favatar%2F201303%2F18%2F17%2F14%2F5146daf314dfa660.jpg%21180x180.jpg",
            };
            context.Users.Add(user);

            var role = new Role
            {
                Id = 1,
                RoleName = "Admin",
                CreateTime = DateTime.Now
            };
            context.Roles.Add(role);

            var userole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
            };
            context.UserRoles.Add(userole);
        }
    }
}