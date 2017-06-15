using SimpleMvc.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Business
{
    public class BaseService
    {
        protected List<T> GetPageList<T>(Expression<Func<T, bool>> condition, int pageIndex, int pageSize, out int total,string includes, params OrderModelField[] orders) where T : class
        {
            using (var context = new EFDbContext())
            {
                IQueryable<T> query ;
                if (!string.IsNullOrEmpty(includes))
                    query = context.Set<T>().Include(includes);
                else
                    query = context.Set<T>();

                if (condition != null)
                {
                    query = query.Where(condition);
                }
                    

                //创建表达式变量参数
                var parameter = Expression.Parameter(typeof(T), "p");

                if (orders != null && orders.Length > 0)
                {
                    for (int i = 0; i < orders.Length; i++)
                    {
                        //根据属性名获取属性
                        var property = typeof(T).GetProperty(orders[i].propertyName);
                        //创建一个访问属性的表达式
                        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                        var orderByExp = Expression.Lambda(propertyAccess, parameter);

                        string OrderName = orders[i].isDesc ? "OrderByDescending" : "OrderBy";

                        MethodCallExpression resultExp = Expression.Call(typeof(Queryable), OrderName, new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));
                        query = query.Provider.CreateQuery<T>(resultExp);
                    }

                }

                

                total = query.Count();
                return query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }

        }

        protected List<T> GetEntitys<T>(Expression<Func<T, bool>> where) where T : class
        {
            using (var context = new EFDbContext())
            {
                List<T> entitys;
                if (where != null)
                {
                    entitys = context.Set<T>().Where(where).ToList();
                }
                else
                {
                    entitys = context.Set<T>().ToList();
                }
                return entitys;
            }
        }

        protected T GetEntity<T>(Expression<Func<T, bool>> where) where T : class
        {
            using (var context = new EFDbContext())
            {
                T entity;
                if (where != null)
                {
                    entity = context.Set<T>().Where(where).FirstOrDefault();
                }
                else
                {
                    entity = context.Set<T>().FirstOrDefault();
                }
                return entity;
            }
        }

        
    }

    public struct OrderModelField
    {
        public string propertyName { get; set; }
        public bool isDesc { get; set; }
    }
}
