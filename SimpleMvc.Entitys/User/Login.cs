using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Entitys
{
    public class Login
    {
        public Login()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string IP { get; set; }

        [DefaultValue(0)]
        public int AuthType { get; set; }

        [DefaultValue(0)]
        public int DeviceType { get; set; }

        public DateTime LoginTime { get; set; }

        public User User { get; set; }
    }
}
