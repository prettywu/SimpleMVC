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
        /// <summary>
        /// 分页搜索
        /// </summary>
        public List<T> getPageDate<T, TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order, int ordertype, int pageIndex, int pageSize, out int Total)
        where T : class
        {
            IQueryable<T> list;
            List<T> result;
            using (var context = new EFDbContext())
            {
                if (where != null)
                {
                    Total = context.Set<T>().Where(where).Count();
                    if (order != null)
                    {
                        if (ordertype == 0)
                            list = context.Set<T>().Where(where).OrderByDescending(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                        else
                            list = context.Set<T>().Where(where).OrderBy(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    }
                    else
                    {
                        list = context.Set<T>().Where(where).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    }
                }
                else
                {
                    Total = context.Set<T>().Count();
                    if (order != null)
                    {
                        if (ordertype == 0)
                            list = context.Set<T>().OrderByDescending(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                        else
                            list = context.Set<T>().OrderBy(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    }
                    else
                    {
                        list = context.Set<T>().Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    }
                }
                result = list.ToList();
            }
            return result;
        }

        public List<T> GetPageList<T>(Expression<Func<T, bool>> condition, int pageIndex, int pageSize, out int total, params OrderModelField[] orders) where T : class
        {
            using (var context = new EFDbContext())
            {
                //条件过滤
                IQueryable<T> query;

                if (condition == null)
                    query = context.Set<T>();
                else
                    query = context.Set<T>().Where(condition);

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

        public List<T> GetEntitys<T>(Expression<Func<T, bool>> where) where T : class
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

        public T GetEntity<T>(Expression<Func<T, bool>> where) where T : class
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
