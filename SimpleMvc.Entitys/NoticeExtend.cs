using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Entitys
{
    public class NoticeExtend
    {
        public Guid UserId { get; set; }

        public Guid NoticeId { get; set; }

        public int ReadState { get; set; }
    }
}
