using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SimpleMvc.Identity
{
    
    public sealed class Startup
    {

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(AuthenticationModule));
        }
    }
}
