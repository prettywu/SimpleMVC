
using System;
using System.Security.Principal;

namespace SimpleMvc.Identity
{
    public class Principal : IPrincipal
    {
        private Func<string, bool> _IsInRole;

        private SimpleIdentity _identity;

        public Principal(SimpleIdentity identity)
        {
            _identity = identity;
        }

        public Principal(string token, object user, Func<string, bool> _isInRole=null)
        {
            _identity = new SimpleIdentity(token, user);
            _IsInRole = _isInRole;
        }

        public IIdentity Identity
        {
            get
            {
                return _identity;
            }
        }

        public T GetUserInfo<T>() where T : class
        {
            if (_identity != null)
                return _identity.User as T;
            else
                return null;
        }

        public bool IsInRole(string role)
        {
            if (_IsInRole == null) return true;
            return _IsInRole(role);
        }

    }

    public static class IPrincipalExtense
    {
        public static T GetEntity<T>(this IPrincipal iprincipal) where T : class
        {
            return iprincipal as T;
        }

        public static Principal GetEntity(this IPrincipal iprincipal) 
        {
            return iprincipal as Principal;
        }
    }
}
