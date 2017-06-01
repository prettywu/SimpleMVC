using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Entitys
{
    public class Enums
    {
        public enum Gender
        {
            Male = 1,
            Female = 2
        }

        public enum AuthType
        {
            站内 = 0,
            QQ = 1,
            微信 = 2,
            微博 = 3
        }

        public enum DeviceType
        {
            WebBrowser = 0,
            MobileBrowser = 1,
        }

        public enum ReturnCode
        {
            参数错误 = 100,
            业务异常 = 200,
            系统异常 = 500
        }
    }
}
