

using System.Collections.Generic;
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
            if (list.ContainsKey(key) && list[key].Contains(item))
            {
                list[key].Remove(item);
            }
        }
    }
}
