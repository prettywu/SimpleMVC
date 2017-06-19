using SimpleMvc.Common;
using SimpleMvc.DAL;
using SimpleMvc.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleMvc.Business
{
    public class LawsuitService : BaseService
    {
        public List<Lawsuit> GetLawsuitList(int pageindex, int pagesize, out int total, string no, string title, int sorttype, int state = -1, string sort = "CreateTime")
        {
            Expression<Func<Lawsuit, bool>> where = l => 1 == 1;
            List<Lawsuit> list;
            //if (!string.IsNullOrEmpty(no))
            //{
            //    where.And(l => l.LawsuitNo.Contains(no));
            //}
            //if (!string.IsNullOrEmpty(title))
            //{
            //    where.And(l => l.Title.Contains(title));
            //}
            //if (state != -1)
            //{
            //    where.And(l => l.State == state);
            //}
            
            var orders = new OrderModelField[]
            {
                new OrderModelField
                {
                     propertyName=sort,
                     isDesc=sorttype==0?true:false
                }
            };

            list = GetPageList(where, orders, null, pageindex, pagesize, out total);
            return list;
        }

        public bool AddLawsuit(Lawsuit lawsuit)
        {
            return Add<Lawsuit>(lawsuit) > 0;
        }

        public bool UpdateLawsuit(Lawsuit lawsuit)
        {
            return Update<Lawsuit>(lawsuit) > 0;
        }

        public List<Opinion> GetOpinionList(Guid awsuitId)
        {
            return GetEntitys<Opinion>(o => o.Lawsuit.Id == awsuitId);
        }


    }


}
