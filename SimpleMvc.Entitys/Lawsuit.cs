using System;

namespace SimpleMvc.Entitys
{
    public class Lawsuit
    {
        public Lawsuit()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Type { get; set; }

        /// <summary>
        /// 案件状态 0:未审核,1:未启动,2:听证中,3:过期
        /// </summary>
        public int State { get; set; }

        public Guid CreateUserId { get; set; }

        public DateTime CreateTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}
