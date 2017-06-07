using SimpleMvc.Common;
using SimpleMvc.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using static SimpleMvc.Entitys.Enums;

namespace SimpleMvc.DAL
{
    public class EFModelChangeInitilize : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        public override void InitializeDatabase(EFDbContext context)
        {
            base.InitializeDatabase(context);
        }

        protected override void Seed(EFDbContext context)
        {
            var userlist = new List<User>()
            {
                new User
                {
                     UserName = "yoyo@qq.com",
                PasswordHash = Helper.MD5encryption("528888"),
                NickName = "yoyo",
                Birthday = new DateTime(1990, 12, 14),
                Gender = (int)Gender.Female,
                RegistTime = DateTime.Now.AddDays(-5),
                LastUpdateTime = DateTime.Now,
                State=1,
                HeadImage = "http://img5.imgtn.bdimg.com/it/u=2056231335,4212597522&fm=23&gp=0.jpg"
                },
                new User
            {
                UserName = "xiayanhan@qq.com",
                PasswordHash = Helper.MD5encryption("123456"),
                NickName = "夏颜涵",
                Birthday = new DateTime(1996, 10, 17),
                Gender = (int)Gender.Female,
                RegistTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                State=1,
                HeadImage = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1495443056&di=49d076e75e1e4b4b6e5aaf2977e7626c&imgtype=jpg&er=1&src=http%3A%2F%2Fv1.qzone.cc%2Favatar%2F201303%2F18%2F17%2F14%2F5146daf314dfa660.jpg%21180x180.jpg",
            },

                new User
                {
                     UserName = "hui@qq.com",
                PasswordHash = Helper.MD5encryption("654321"),
                NickName = "huihui",
                Birthday = new DateTime(1991, 11, 14),
                Gender = (int)Gender.Male,
                RegistTime = DateTime.Now.AddHours(-3),
                LastUpdateTime = DateTime.Now,
                State=2,
                HeadImage = "http://img.qqbody.com/uploads/allimg/201502/05-171941_803.jpg"
                },
                new User
                {
                     UserName = "pretty@qq.com",
                PasswordHash = Helper.MD5encryption("000000"),
                NickName = "pretty boy",
                Birthday = new DateTime(1992, 3, 5),
                Gender = (int)Gender.Male,
                RegistTime = DateTime.Now.AddMinutes(-12),
                LastUpdateTime = DateTime.Now,
                State=3,
                HeadImage = "http://p1.qqyou.com/touxiang/UploadPic/2014-5/23/2014052311412225651.jpg"
                },
                new User
                {
                     UserName = "happy@qq.com",
                PasswordHash = Helper.MD5encryption("888888"),
                NickName = "happy girl",
                Birthday = new DateTime(1995, 5, 20),
                Gender = (int)Gender.Female,
                RegistTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                State=0,
                HeadImage = "http://www.feizl.com/upload2007/2015_07/150724123232506.jpg"
                }
            };
            context.Users.AddRange(userlist);
            var role = new Role
            {
                Id = 1,
                RoleName = "Admin",
                CreateTime = DateTime.Now
            };
            context.Roles.Add(role);
            
            var userrolelist = new List<UserRole>()
            {
                new UserRole
            {
                UserId = userlist[0].Id,
                RoleId = role.Id,
            },
                new UserRole
            {
                UserId = userlist[1].Id,
                RoleId = role.Id,
            }
            };
            context.UserRoles.AddRange(userrolelist);
        }
    }
}
