using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Entitys
{
    public class Notice
    {
        public Guid Id { get; set; }

        public Guid ReceiverId { get; set; }

        /// <summary>
        /// 通知=10,事件=20
        /// </summary>
        public int Type { get; set; }

        public string Content { get; set; }

        public string Link { get; set; }

        public bool IsReaded { get; set; }

        public DateTime CreateTime { get; set; }

        public bool IsDeleted { get; set; }
        
    }
}
