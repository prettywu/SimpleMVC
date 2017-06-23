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

        /// <summary>
        /// 用户状态
        /// </summary>
        public enum UserState
        {
            未审核=0,
            正常=1,
            冻结=2,
            未通过=10
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

        public enum LawsuitType
        {
            刑事责任案件=0,
            经济纠纷案件=1
        }

        /// <summary>
        /// 案件状态 0:未审核,1:未启动,2:听证中,3:已过期
        /// </summary>
        public enum LawsuitState
        {
            未审核=0,
            未启动=1,
            听证中=2,
            已过期=3
        }

        /// <summary>
        /// 参与者角色 0:管理员,1:受害人,2:受害人律师,3:犯罪嫌疑人律师,4:听证员,5:行政主管机关
        /// </summary>
        public enum ParticipantRole
        {
            管理员=0,
            受害人=1,
            受害人律师=2,
            犯罪嫌疑人律师=3,
            听证员=4,
            行政主管机关=5
        }

        /// <summary>
        /// 意见类型 0:管理员意见,1:受害人意见,2:受害人律师意见,3:犯罪嫌疑人律师意见,4:听证员无罪意见,5:听证员有罪意见,6:行政主管机关意见
        /// </summary>
        public enum OpinionType
        {
            管理员意见=0,
            受害人意见=1,
            受害人律师意见=2,
            犯罪嫌疑人律师意见=3,
            听证员无罪意见=4,
            听证员有罪意见=5,
            行政主管机关意见=6
        }
        /// <summary>
        /// 申请审核状态 0:未审核,1:审核通过,-1:审核否决
        /// </summary>
        public enum ApplicationState
        {
            未审核=0,
            审核通过=1,
            审核否决=-1
        }

        /// <summary>
        /// 消息类型
        /// </summary>
        public enum MessageType
        {
            通知 = 10,
            消息 = 20
        }
    }
}
