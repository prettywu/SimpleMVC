using System;
using System.Collections.Generic;

namespace SimpleMvc.Entitys
{
    public class Lawsuit
    {
        public Lawsuit()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        /// <summary>
        /// 案件编号
        /// </summary>
        public string LawsuitNo { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
        /// <summary>
        /// 案件类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 案件状态 0:未审核,1:未启动,2:听证中,3:已过期
        /// </summary>
        public int State { get; set; }

        public User Creater { get; set; }

        public DateTime CreateTime { get; set; }

        public virtual List<Participant> Participants { get; set; }

        public virtual List<Application> Applications { get; set; }

        public bool IsDeleted { get; set; }
    }
}
