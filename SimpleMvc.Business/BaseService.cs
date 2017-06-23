using SimpleMvc.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleMvc.Business
{
    public class BaseService
    {
        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="orders"></param>
        /// <param name="includes"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        protected List<T> GetPageList<T>(Expression<Func<T, bool>> condition, OrderModelField[] orders, string[] includes, int pageIndex, int pageSize, out int total) where T : class
        {
            using (var context = new EFDbContext())
            {
                DbQuery<T> query_in = context.Set<T>();
                if (includes != null && includes.Any())
                {
                    for (int i = 0; i < includes.Length; i++)
                    {
                        query_in = query_in.Include(includes[i]);
                    }
                }

                IQueryable<T> query = query_in;
                if (condition != null)
                {
                    query = query_in.Where(condition);
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

        /// <summary>
        /// 过滤查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
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

        protected int Add<T>(T entity) where T : class
        {
            if (entity == null) return 0;
            using (var context = new EFDbContext())
            {
                context.Set<T>().Add(entity);
                return context.SaveChanges();
            }
        }

        protected int Update<T>(T entity) where T : class
        {
            if (entity == null) return 0;
            using (var context = new EFDbContext())
            {
                var dbset = context.Set<T>();
                var dbentity = context.Entry(entity);
                dbentity.State = System.Data.Entity.EntityState.Modified;
                foreach (var preperty in typeof(T).GetProperties())
                {
                    if (dbentity.Property(preperty.Name).CurrentValue != preperty.GetValue(entity))
                    {
                        dbentity.Property(preperty.Name).CurrentValue = preperty.GetValue(entity);
                        dbentity.Property(preperty.Name).IsModified = true;
                    }
                    else
                    {
                        dbentity.Property(preperty.Name).IsModified = false;
                    }
                }
                return context.SaveChanges();
            }
        }

        /// <summary>  
        /// 更新指定字段,必须要给主键和更新字段赋值
        /// </summary>  
        /// <param name="entity">实体</param>  
        /// <param name="fileds">更新字段数组</param>  
        public void UpdateEntityFields<T>(T entity, List<string> fileds) where T:class
        {
            if (entity != null && fileds != null)
            {
                using (var context = new EFDbContext())
                {
                    context.Set<T>().Attach(entity);
                    var SetEntry = ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity);
                    foreach (var t in fileds)
                    {
                        SetEntry.SetModifiedProperty(t);
                    }
                    context.SaveChanges();
                }
            }
        }
    }

    public struct OrderModelField
    {
        public string propertyName { get; set; }
        public bool isDesc { get; set; }
    }
}
