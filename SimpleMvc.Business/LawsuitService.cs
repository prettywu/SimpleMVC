using SimpleMvc.Common;
using SimpleMvc.DAL;
using SimpleMvc.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleMvc.Business
{
    public class LawsuitService
    {
        public List<Lawsuit> GetLawsuitList(int pageindex, int pagesize, out int total, string no = "", string title = "", int state = -1, string sort = "CreateTime", string sorttype = "desc")
        {
            Expression<Func<Lawsuit, bool>> where = null;
            Expression<Func<Lawsuit, string>> order = null;
            List<Lawsuit> list;
            if (!string.IsNullOrEmpty(no))
            {
                where = l => l.LawsuitNo.Contains(no);
            }
            if (!string.IsNullOrEmpty(title))
            {
                where.And(l => l.Title.Contains(title));
            }
            if (state != -1)
            {
                where.And(l => l.State == state);
            }

            order = l => typeof(Lawsuit).GetProperty(sort).GetValue(l).ToString();

            int ordertype = 0;
            if (sorttype == "desc")
            {
                ordertype = 1;
            }
            list = new DbService().getPageDate(where, order, ordertype, pageindex, pagesize, out total);
            return list;
        }

        public bool AddLawsuit(Lawsuit lawsuit)
        {
            return new DbService().Add<Lawsuit>(lawsuit) > 0;
        }

        public bool UpdateLawsuit(Lawsuit lawsuit)
        {
            return new DbService().Update<Lawsuit>(lawsuit) > 0;
        }

        public List<Opinion> GetOpinionList(Guid awsuitId)
        {
            return new DbService().GetEntitys<Opinion>(o => o.Lawsuit.Id == awsuitId);
        }
    }


}
