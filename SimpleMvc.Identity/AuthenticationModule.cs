using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SimpleMvc.Identity
{
    public class AuthenticationModule : IHttpModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public virtual void Init(HttpApplication application)
        {

            application.AuthenticateRequest += new EventHandler(OnAuthenticateRequest);
            application.PostRequestHandlerExecute += new EventHandler(OnPostRequestHandlerExecute);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAuthenticateRequest(object sender, EventArgs e)
        {
            SimpleAuthentication.ReadAuthenticateInfo((HttpApplication)sender);
        }

        private void OnPostRequestHandlerExecute(object sender, EventArgs e)
        {
            SimpleAuthentication.WriteToken((HttpApplication)sender);
        }
    }
}
