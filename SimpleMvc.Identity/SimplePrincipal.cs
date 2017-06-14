
using System;
using System.Security.Principal;

namespace SimpleMvc.Identity
{
    public class SimplePrincipal : IPrincipal
    {
        private Func<string,object, bool> _IsInRole;

        private SimpleIdentity _identity;

        public SimplePrincipal(SimpleIdentity identity,Func<string, object, bool> _isInRole = null)
        {
            _identity = identity;
            _IsInRole = _isInRole;
        }

        public SimplePrincipal(string token, object user, Func<string,object, bool> _isInRole = null)
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

        public SimpleIdentity SimpleIdentity
        {
            get { return _identity; }
        }

        public T GetUser<T>() where T : class
        {
            if (_identity != null)
                return _identity.User as T;
            else
                return null;
        }

        public bool IsInRole(string role)
        {
            if (_IsInRole == null)
            {
                if (!string.IsNullOrEmpty(role)) return false;
                else return true;
            }
            else
            {
                return _IsInRole(role, _identity.User);
            }
        }

    }

    public static class IPrincipalExtense
    {
        public static SimplePrincipal GetSimpleInstance(this IPrincipal iprincipal)
        {
            return iprincipal as SimplePrincipal;
        }

        public static T GetUser<T>(this IPrincipal iprincipal) where T:class
        {
            SimplePrincipal principal = iprincipal as SimplePrincipal;
            return principal.GetUser<T>();
        }
    }
}
