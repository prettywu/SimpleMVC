using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using static SimpleMVC.Entitys.Enums;

namespace SimpleMVC.Entitys
{
    public class Login
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string IP { get; set; }

        [DefaultValue(0)]
        public int AuthType { get; set; }

        [DefaultValue(0)]
        public int DeviceType { get; set; }

        public DateTime LoginTime { get; set; }
    }
}