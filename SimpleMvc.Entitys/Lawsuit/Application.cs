using System;

namespace SimpleMvc.Entitys
{
    public class Application
    {
        public Application()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        /// <summary>
        /// 申请编号
        /// </summary>
        public string ApplyNo { get; set; }

        /// <summary>
        /// 申请角色类型 1:受害人,2:受害人律师,3:犯罪嫌疑人律师,4:行政主管机构,5:听证员
        /// </summary>
        public int ApplyRole { get; set; }

        /// <summary>
        /// 申请状态 0:未审核,1:审核通过,-1:审核否决
        /// </summary>
        public int State { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime AuditTime { get; set; }

        public bool IsDeleted { get; set; }
        public Guid LawsuitId { get; set; }
        public Lawsuit Lawsuit { get; set; }

        public User ApplyUser { get; set; }

        public User AuditUser { get; set; }
    }
}
