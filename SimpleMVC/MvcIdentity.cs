using SimpleMVC.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SimpleMVC
{
    public class MvcIdentity : IIdentity
    {
        private User _user;
        private string _token;
        
        public MvcIdentity(User user, string token)
        {
            _user = user;
            _token = token;
        }

        public string AuthenticationType
        {
            get
            {
                return "simplemvc";
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

        public User User { get { return _user; } }

        //public Guid Id { get { return _id; } }
        //public string Token { get { return _token; } }
        //public string UserName { get { return _username; } }
        //public string NickName { get { return _nickname; } }
        //public string HeadImage { get { return _headimage; } }
        //public int Gender { get { return _gender; } }
    }

    public class MvcPrincipal : IPrincipal
    {
        private MvcIdentity _identity;//用户标识  

        public MvcPrincipal(MvcIdentity identity)
        {
            _identity = identity;
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}