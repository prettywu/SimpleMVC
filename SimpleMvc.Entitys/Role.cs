using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Entitys
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public DateTime CreateTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}
