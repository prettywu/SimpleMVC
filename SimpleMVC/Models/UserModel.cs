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

        public string PasswordHash { get; set; }

        public string NickName { get; set; }

        public string HeadImage { get; set; }

        public string Birthday { get; set; }

        public int Gender { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public DateTime RegistTime { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}