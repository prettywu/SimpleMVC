using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVC.Entitys
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public DateTime CreateTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}