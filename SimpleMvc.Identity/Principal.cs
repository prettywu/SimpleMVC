
using System.Security.Principal;

namespace SimpleMvc.Identity
{
    public class Principal : IPrincipal
    {
        public SimpleIdentity _identity;

        public Principal(SimpleIdentity identity)
        {
            _identity = identity;
        }

        public Principal(string token, object user)
        {
            _identity = new SimpleIdentity(token, user);
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
            return true;
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
