using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SimpleMvc.Identity
{
    class AuthenticationModule : IHttpModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public virtual void Init(HttpApplication application)
        {

            application.AuthenticateRequest += new EventHandler(this.OnAuthenticateRequest);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAuthenticateRequest(object sender, EventArgs e)
        {

        }

        
    }
}
