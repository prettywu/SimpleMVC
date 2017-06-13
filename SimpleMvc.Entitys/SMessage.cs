using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Entitys
{
    public class SMessage
    {
        public Guid Id { get; set; }

        public Guid ReceiverId { get; set; }

        public int Type { get; set; }

        public string Content { get; set; }

        public string Reltive { get; set; }

        public int ReadState { get; set; }

        public bool IsDeleted { get; set; }
    }
}
