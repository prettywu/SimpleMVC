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
                    Id=Guid.NewGuid(),
                    UserName = "yoyo@qq.com",
                    PasswordHash = Helper.MD5encryption("528888"),
                    NickName = "yoyo",
                    Birthday = new DateTime(1990, 12, 14),
                    Email="yoyo@qq.com",
                    Phone="13813516387",
                    Location="上海",
                    Gender = (int)Gender.Female,
                    RegistTime = DateTime.Now.AddDays(-5),
                    LastUpdateTime = DateTime.Now,
                    State=1,
                    HeadImage = "http://img3.imgtn.bdimg.com/it/u=2277472016,2846573143&fm=214&gp=0.jpg"
                },
                new User
                {
                    Id=Guid.NewGuid(),
                    UserName = "xiayanhan@qq.com",
                    PasswordHash = Helper.MD5encryption("123456"),
                    NickName = "夏颜涵",
                    Birthday = new DateTime(1996, 10, 17),
                    Email="xiayanhan@qq.com",
                    Phone="13817516387",
                    Location="北京",
                    Gender = (int)Gender.Female,
                    RegistTime = DateTime.Now,
                    LastUpdateTime = DateTime.Now,
                    State=0,
                    HeadImage = "http://img4.imgtn.bdimg.com/it/u=2115793746,1560654875&fm=214&gp=0.jpg"
                },
                new User
                {
                    Id=Guid.NewGuid(),
                     UserName = "hui@qq.com",
                    PasswordHash = Helper.MD5encryption("654321"),
                    NickName = "huihui",
                    Birthday = new DateTime(1991, 11, 14),
                    Email="hui@qq.com",
                    Phone="15140499494",
                    Location="深圳",
                    Gender = (int)Gender.Male,
                    RegistTime = DateTime.Now.AddHours(-3),
                    LastUpdateTime = DateTime.Now,
                    State=2,
                    HeadImage = "http://img.qqbody.com/uploads/allimg/201502/05-171941_803.jpg"
                },
                new User
                {
                    Id=Guid.NewGuid(),
                     UserName = "pretty@qq.com",
                    PasswordHash = Helper.MD5encryption("000000"),
                    NickName = "pretty boy",
                    Birthday = new DateTime(1992, 3, 5),
                    Email="pretty@qq.com",
                    Phone="13813516364",
                    Location="厦门",
                    Gender = (int)Gender.Male,
                    RegistTime = DateTime.Now.AddMinutes(-12),
                    LastUpdateTime = DateTime.Now,
                    State=1,
                    HeadImage = "http://p1.qqyou.com/touxiang/UploadPic/2014-5/23/2014052311412225651.jpg"
                },
                new User
                {
                    Id=Guid.NewGuid(),
                    UserName = "happy@qq.com",
                    PasswordHash = Helper.MD5encryption("888888"),
                    NickName = "happy girl",
                    Birthday = new DateTime(1995, 5, 20),
                    Email="happy@qq.com",
                    Phone="13513546337",
                    Location="成都",
                    Gender = (int)Gender.Female,
                    RegistTime = DateTime.Now,
                    LastUpdateTime = DateTime.Now,
                    State=10,
                    HeadImage = "http://www.feizl.com/upload2007/2015_07/150724123232506.jpg"
                }
            };
            context.Users.AddRange(userlist);

            var rolelist = new List<Role>()
            {
                new Role{Id=1, RoleName="超级管理员", CreateTime=DateTime.Now,IsDeleted=false},
                new Role{Id=2, RoleName="管理员", CreateTime=DateTime.Now,IsDeleted=false},
                new Role{Id=3, RoleName="总经理", CreateTime=DateTime.Now,IsDeleted=false},
                new Role{Id=4, RoleName="部门经理", CreateTime=DateTime.Now,IsDeleted=false},
                new Role{Id=5, RoleName="客服", CreateTime=DateTime.Now,IsDeleted=false}
            };
            context.Roles.AddRange(rolelist);

            var userrolelist = new List<UserRole>()
            {
                new UserRole
            {
                UserId = userlist[0].Id,
                RoleId = 1,
            },
                new UserRole
            {
                UserId = userlist[1].Id,
                RoleId = 2,
            },
                new UserRole
            {
                UserId = userlist[2].Id,
                RoleId = 3,
            },
                new UserRole
            {
                UserId = userlist[3].Id,
                RoleId = 4,
            },
                new UserRole
            {
                UserId = userlist[4].Id,
                RoleId = 5,
            }
            };
            context.UserRoles.AddRange(userrolelist);
        }
    }
}
