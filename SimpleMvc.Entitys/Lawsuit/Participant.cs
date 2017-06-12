using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Entitys
{
    public class Participant
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 参与者在案件中的角色
        /// </summary>
        public int Role { get; set; }

        public User User { get; set; }

        public Lawsuit Lawsuit { get; set; }

        public DateTime CreateTime { get; set; }

        public virtual List<Opinion> Opinions { get; set; }

        public bool IsDeleted { get; set; }
    }
}
