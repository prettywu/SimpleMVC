using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SimpleMvc.Identity;
using SimpleMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using SimpleMvc.Entitys;

namespace SimpleMVC.Hubs
{
    [HubName("chat")]
    public class MessageHub : Hub
    {
        /// <summary>
        /// 用户的connectionID与用户名对照表
        /// </summary>
        private readonly static Dictionary<string, List<string>> _connections = new Dictionary<string, List<string>>();
        private string userid;

        public MessageHub()
        {
            if (HttpContext.Current != null&& HttpContext.Current.User != null)
                this.userid = HttpContext.Current.User.GetUser<User>().Id.ToString();
        }

        public void SendAll(string name, string message)
        {

            Clients.All.receive(name, message);
        }

        public void SendTo(string user, string message, int type)
        {
            Clients.Clients(_connections[user]).receive(message, type);
        }

        public void SendToGroup(List<string> users, string message, int type)
        {
            var receivers = _connections.Where(c => users.Contains(c.Key)).Select(kv => kv.Value).Merge();
            Clients.Clients(receivers).receive(message, type);
        }

        public override Task OnConnected()
        {
            // 在这添加你的代码.   
            // 例如:在一个聊天程序中,记录当前连接的用户ID和名称,并标记用户在线.
            // 在该方法中的代码完成后,通知客户端建立连接,客户端代码
            // start().done(function(){//你的代码});
            _connections.Set(userid, Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            _connections.Set(userid, Context.ConnectionId);
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _connections.DeleteListItem(Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }


    }
}