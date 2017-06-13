using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SimpleMvc.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVC.Hubs
{
    [Authentication(DisLock = true)]
    [HubName("chat")]
    public class MessageHub : Hub
    {
      
        

        public void SendAll(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.receiveMessage(name, message);
        }

        public void SendGroup(string group,string message)
        {
           
        }
    }
}