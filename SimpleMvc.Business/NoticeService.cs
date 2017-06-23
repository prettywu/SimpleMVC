using SimpleMvc.Common;
using SimpleMvc.DAL;
using SimpleMvc.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Business
{
    public class NoticeService : BaseService
    {
        /// <summary>
        /// 通知列表
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="type"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<Notice> GetUserNoticeList(Guid userid, int? type, int pageindex, int pagesize, out int total)
        {
            Expression<Func<Notice, bool>> where = n => n.ReceiverId == userid;
            if (type != null)
                where = where.And<Notice>(n => n.Type == type);

            OrderModelField[] order = new OrderModelField[]
            {
                new OrderModelField
                {
                    propertyName ="CreateTime",
                    isDesc =true
                }
            };

            var list = GetPageList<Notice>(where, order, null, pageindex, pagesize, out total);
            return list;
        }

        /// <summary>
        /// 获取未读消息数
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetUnReadCounts(Guid userid, int? type)
        {
            using (var context = new EFDbContext())
            {
                if (type != null)
                    return context.Notices.Where(n => n.ReceiverId == userid && n.IsReaded == false && n.Type == type).Count();
                else
                    return context.Notices.Where(n => n.ReceiverId == userid && n.IsReaded == false).Count();
            }
        }

        /// <summary>
        /// 设置消息已读
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        public Task ReadNotice(Guid noticeId)
        {
            return Task.Run(() =>
            {
                using(var context=new EFDbContext())
                {
                    UpdateEntityFields<Notice>(new Notice { Id = noticeId, IsReaded = true }, new List<string> { "IsReaded" });
                }
            });
        }
    }
}
