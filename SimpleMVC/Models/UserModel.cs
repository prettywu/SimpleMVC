using SimpleMvc.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVC.Models
{
    public class UserModel
    {

        public Guid Id { get; set; }

        public string UserName { get; set; }
        
        public string NickName { get; set; }

        public string HeadImage { get; set; }

        public string Birthday { get; set; }

        public string Gender { get; set; }

        public string State { get; set; }

        public string LastUpdateTime { get; set; }

        public string RegistTime { get; set; }
        
        public bool IsDeleted { get; set; }
    }

    public static class EntityConvert
    {
        public static UserModel ConvertToModel(this User user)
        {
            var model = new UserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                NickName = user.NickName,
                HeadImage = user.HeadImage,
                Birthday = user.Birthday.ToString("yyyy-MM-dd"),
                Gender = Enum.GetName(typeof(Enums.Gender), user.Gender),
                State=Enum.GetName(typeof(Enums.UserState),user.State),
                LastUpdateTime = user.LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                RegistTime = user.RegistTime.ToString("yyyy-MM-dd HH:mm:ss"),
                IsDeleted = user.IsDeleted
            };
            return model;
        }
    }
}