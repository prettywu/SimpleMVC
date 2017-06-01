
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SimpleMvc.Common
{
    public class Helper
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
    }
}
