using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVC.Entitys
{
    public class Enums
    {
        public enum AuthType
        {
            站内=0,
            QQ=1,
            微信=2,
            微博=3
        }

        public enum DeviceType
        {
            WebBrowser=0,
            MobileBrowser=1,
        }
    }
}