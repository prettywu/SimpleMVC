﻿using System;
using System.ComponentModel;

namespace SimpleMvc.Entitys
{
    public class UserRole
    {
        public UserRole()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public int RoleId { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}