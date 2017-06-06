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
        private bool _islock;

        public SimpleIdentity(string token, object user,bool islock=false)
        {
            _token = token;
            _user = user;
            _islock = islock;
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

        public bool IsLocked
        {
            get { return _islock; }
            internal set { _islock = value; }
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
