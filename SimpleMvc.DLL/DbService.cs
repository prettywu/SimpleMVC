using SimpleMvc.Common;
using SimpleMvc.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.DAL
{
    public class DbService
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

        public List<T> GetPageList<T>(Expression<Func<T, bool>> condition, int pageIndex, int pageSize, out int total, params OrderModelField[] orders)where T:class
        {
            using(var context=new EFDbContext())
            {
                //条件过滤
                IQueryable< T > query;
                
                if(condition==null)
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

        public User GetUserByUserId(Guid userId)
        {
            if (userId.Equals(Guid.Empty)) return null;
            using (var context = new EFDbContext())
            {
                return context.Users.Where(u => u.Id == userId).FirstOrDefault();
            }
        }

        public User GetUserByUserName(string username)
        {
            if (string.IsNullOrEmpty(username)) return null;
            using (var context = new EFDbContext())
            {
                return context.Users.Where(u => username == u.UserName && !u.IsDeleted).FirstOrDefault();
            }
        }

        public User GetUserByToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;
            using (var context = new EFDbContext())
            {
                var login = context.Logins.Where(l => l.Id == new Guid(token)).FirstOrDefault();
                if (login != null)
                {
                    return context.Users.Where(u => u.Id == login.UserId).FirstOrDefault();
                }
                return null;
            }
        }

        public Login GetLoginByLoginId(string loginId)
        {
            if (string.IsNullOrEmpty(loginId)) return null;
            using (var context = new EFDbContext())
            {
                return context.Logins.Where(l => l.Id == new Guid(loginId)).FirstOrDefault();
            }
        }

        public User LoginPasswordCheck(string username, string password)
        {
            if (string.IsNullOrEmpty(username)) return null;
            User user = null;
            using (var context = new EFDbContext())
            {
                user = context.Users.Where(u => username == u.UserName && !u.IsDeleted).FirstOrDefault();
            }
            if (user == null) return null;
            if (Helper.MD5encryption(password) == user.PasswordHash)
            {
                return user;
            }
            return null;
        }

        /// <summary>
        /// 写入登录信息
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool WriteLoginInfo(Login login)
        {
            if (login == null) return false;
            using (var context = new EFDbContext())
            {
                context.Logins.Add(login);
                return context.SaveChanges() > 0;
            }
        }

        public List<Role> GetUserRoles(Guid userId)
        {
            if (userId == null) return null;
            using (var context = new EFDbContext())
            {
                var userroles = context.UserRoles.Where(ur => userId == ur.UserId && !ur.IsDeleted);
                var roles = context.Roles.Where(r => userroles.Select(ur => ur.RoleId).Contains(r.Id));
                return roles.ToList();
            }
        }

        public bool IsInRole(string roles, object user)
        {
            User _user = user as User;
            var _roles = GetUserRoles(_user.Id);
            if (_roles.Any())
            {
                var intersect = _roles.Select(r => r.RoleName).Intersect(roles.Split(new char[] { ',' }));
                if (intersect.Any())
                    return true;
            }
            return false;
        }

        public bool AddUser(User user)
        {
            if (user == null) return false;
            using (var context = new EFDbContext())
            {
                context.Users.Add(user);
                return context.SaveChanges() > 0;
            }
        }

        #region Lawsuit
        public int Add<T>(T entity) where T : class
        {
            if (entity == null) return 0;
            using (var context = new EFDbContext())
            {
                context.Set<T>().Add(entity);
                return context.SaveChanges();
            }
        }

        public int Update<T>(T entity) where T : class
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
        #endregion
    }

    public struct OrderModelField
    {
        public string propertyName { get; set; }
        public bool isDesc { get; set; }
    }
}
