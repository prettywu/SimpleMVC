using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Entitys
{
    public class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string NickName { get; set; }

        public string HeadImage { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Location { get; set; }

        public DateTime Birthday { get; set; }

        public int Gender { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public DateTime RegistTime { get; set; }

        public int State { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        
    }
}
