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
            list = new DbService().getPageDate(where, order, ordertype, pageindex, pagesize, out total).ToList();
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
            return new DbService().GetEntitys<Opinion>(o=>o.)
        }
    }

    public static class ExpressionExtend
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2) where T : class
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}
