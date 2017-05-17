using SimpleMVC.App_Start;
using SimpleMVC.BLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SimpleMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<EFDbContext>(new DbInitilize());
            //using (var context = new EFDbContext())
            //{
            //    context.Database.Initialize(true);
            //}
        }

        // 1 在 ASP.NET 响应请求时作为 HTTP 执行管线链中的第一个事件发生
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 2 当安全模块已建立用户标识时发生。注：AuthenticateRequest事件发出信号表示配置的身份验证机制已对当前请求进行了身份验证。
        //   预订 AuthenticateRequest 事件可确保在处理附加的模块或事件处理程序之前对请求进行身份验证
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            AuthManager.ReadAuthInfo();
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 3 当安全模块已建立用户标识时发生。PostAuthenticateRequest事件在 AuthenticateRequest 事件发生之后引发。
        //   预订PostAuthenticateRequest 事件的功能可以访问由PostAuthenticateRequest 处理的任何数据
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 4 当安全模块已验证用户授权时发生。AuthorizeRequest 事件发出信号表示 ASP.NET 已对当前请求进行了授权。
        //   预订AuthorizeRequest 事件可确保在处理附加的模块或事件处理程序之前对请求进行身份验证和授权
        protected void Application_AuthorizeRequest(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 5 在当前请求的用户已获授权时发生。PostAuthorizeRequest 事件发出信号表示 ASP.NET 已对当前请求进行了授权。
        //   预订PostAuthorizeRequest 事件可确保在处理附加的模块或处理程序之前对请求进行身份验证和授权
        protected void Application_PostAuthorizeRequest(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 6 当 ASP.NET 完成授权事件以使缓存模块从缓存中为请求提供服务时发生，从而跳过事件处理程序（例如某个页或 XML Web services）的执行
        protected void Application_ResolveRequestCache(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 7 在 ASP.NET 跳过当前事件处理程序的执行并允许缓存模块满足来自缓存的请求时发生。）在 PostResolveRequestCache 事件之后、PostMapRequestHandler 事件之前创建一个事件处理程序（对应于请求 URL 的页
        protected void Application_PostResolveRequestCache(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 8 在 ASP.NET 已将当前请求映射到相应的事件处理程序时发生。
        protected void Application_PostMapRequestHandler(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 9 当 ASP.NET 获取与当前请求关联的当前状态（如会话状态）时发生。
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 10 在已获得与当前请求关联的请求状态（例如会话状态）时发生。
        protected void Application_PostAcquireRequestState(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 11 恰好在 ASP.NET 开始执行事件处理程序（例如，某页或某个 XML Web services）前发生。
        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 12 在 ASP.NET 事件处理程序（例如，某页或某个 XML Web service）执行完毕时发生。
        protected void Application_PostRequestHandlerExecute(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 13 在 ASP.NET 执行完所有请求事件处理程序后发生。该事件将使状态模块保存当前状态数据。
        protected void Application_ReleaseRequestState(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 14 在 ASP.NET 已完成所有请求事件处理程序的执行并且请求状态数据已存储时发生。
        protected void Application_PostReleaseRequestState(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 15 当 ASP.NET 执行完事件处理程序以使缓存模块存储将用于从缓存为后续请求提供服务的响应时发生。
        protected void Application_UpdateRequestCache(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 16 在 ASP.NET 完成缓存模块的更新并存储了用于从缓存中为后续请求提供服务的响应后，发生此事件。
        protected void Application_PostUpdateRequestCache(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 17 在 ASP.NET 完成缓存模块的更新并存储了用于从缓存中为后续请求提供服务的响应后，发生此事件。
        //    仅在 IIS 7.0 处于集成模式并且.NET Framework 至少为 3.0 版本的情况下才支持此事件
        protected void Application_LogRequest(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 18 在 ASP.NET 处理完 LogRequest 事件的所有事件处理程序后发生。
        //    仅在 IIS 7.0 处于集成模式并且.NET Framework 至少为 3.0 版本的情况下才支持此事件。
        protected void Application_PostLogRequest(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }

        // 19 在 ASP.NET 响应请求时作为 HTTP 执行管线链中的最后一个事件发生。
        //    在调用 CompleteRequest 方法时始终引发 EndRequest 事件。
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            var user = this.User;
            var user2 = ((HttpApplication)sender).User;
        }
    }
}
