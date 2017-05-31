using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Identity
{
    public class SimpleIdentity : IIdentity
    {
        private object _user;
        private string _token;

        public SimpleIdentity(string token, object user)
        {
            _token = token;
            _user = user;
        }
        
        public string AuthenticationType
        {
            get
            {
                return "spmt";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return !string.IsNullOrEmpty(_token);
            }
        }

        public string Name
        {
            get
            {
                return _token;
            }
        }

        public object User { get { return _user; } }

        public T GetUser<T>() where T : class
        {
            return _user as T;
        }
    }
}
