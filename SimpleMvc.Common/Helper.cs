

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SimpleMvc.Common
{
    public static class Helper
    {
        public static string MD5encryption(string input)
        {

            /// <summary>
            /// MD5加密算法
            /// </summary>

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public static string GetHostAddress()
        {
            try
            {
                string userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(userHostAddress))
                {
                    userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (string.IsNullOrEmpty(userHostAddress))
                {
                    userHostAddress = HttpContext.Current.Request.UserHostAddress;
                }
                if (string.IsNullOrEmpty(userHostAddress))
                {
                    userHostAddress = string.Empty;
                }
                return userHostAddress;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static IList<T> Merge<T>(this IEnumerable<IList<T>> list) where T : class
        {
            IList<T> result = null;
            foreach (IList<T> item in list)
            {
                if (result == null) result = item;
                else
                {
                    foreach (T t in item)
                    {
                        result.Add(t);
                    }
                }
            }
            return result;
        }

        public static void Set(this Dictionary<string, List<string>> list, string key, string item)
        {
            if (string.IsNullOrEmpty(key)) return;
            if (list.ContainsKey(key))
            {
                var itemlist = list[key] ?? new List<string>();
                if (!itemlist.Contains(item))
                    itemlist.Add(item);
            }
            else
            {
                list.Add(key, new List<string> { item });
            }
        }

        public static void Delete(this Dictionary<string, List<string>> list, string key, string item)
        {
            if (string.IsNullOrEmpty(key)) return;
            if (list.ContainsKey(key) && list[key].Contains(item))
            {
                list[key].Remove(item);
            }
        }

        public static void DeleteListItem(this Dictionary<string, List<string>> list, string item)
        {
            if (string.IsNullOrEmpty(item)) return;
            if (list == null) return;
            foreach (var itemlist in list)
            {
                if (itemlist.Value.Contains(item))
                {
                    itemlist.Value.Remove(item);
                    if (itemlist.Value.Count <= 0) list.Remove(itemlist.Key);
                    break;
                }
            }

        }
        
    }

    /// <summary>
    /// lambda表达扩展方法
    /// </summary>
    public static class ExpressionExtend
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2) where T : class
        {
            if (expr1 == null) return expr2;
            if (expr2 == null) return expr1;
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null) return expr2;
            if (expr2 == null) return expr1;
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}
