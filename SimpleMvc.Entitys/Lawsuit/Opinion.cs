using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Entitys
{
    public class Opinion
    {
        public Opinion()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        
        public string Content { get; set; }

        /// <summary>
        /// 意见类型 1:受害人意见,2:受害人律师意见,3:犯罪嫌疑人律师意见,4:听证员意见,5:行政主管机关意见
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 关联案件
        /// </summary>
        public Lawsuit Lawsuit { get; set; }

        public Participant Participant { get; set; }

        public DateTime CreateTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}
