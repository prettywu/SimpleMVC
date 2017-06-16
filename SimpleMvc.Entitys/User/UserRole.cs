using System;
using System.ComponentModel;

namespace SimpleMvc.Entitys
{
    public class UserRole
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public int RoleId { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public  Role Role { get; set; }

        public  User User { get; set; }
    }
}
