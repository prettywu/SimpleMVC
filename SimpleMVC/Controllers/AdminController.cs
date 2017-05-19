using SimpleMVC.BLL;
using SimpleMVC.Common;
using SimpleMVC.Entitys;
using SimpleMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static SimpleMVC.Entitys.Enums;

namespace SimpleMVC.Controllers
{
    public class AdminController : Controller
    {
        public UserManager userManager = new UserManager();

        #region Views
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult Login()
        {
            return View();
        }

        public ViewResult Elements()
        {
            return View();
        }

        public ViewResult Panels()
        {
            return View();
        }

        public ViewResult Notifications()
        {
            return View();
        }

        public ViewResult Tables() { return View(); }
        
        public ViewResult Typography() { return View(); }

        public ViewResult Icons() { return View(); }

        public ViewResult UserManagement()
        {
            return View();
        }
        #endregion

        #region Actions
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                var user = userManager.LoginPasswordCheck(model.UserName, model.Password);
                if (user != null)
                {
                    var login = new Login
                    {
                        UserId = user.Id,
                        LoginTime = DateTime.Now,
                        IP = MvcHelper.GetHostAddress(),
                        AuthType = (int)AuthType.站内,
                        DeviceType = (int)DeviceType.WebBrowser
                    };
                    userManager.WriteLoginInfo(login);
                    AuthManager.SignIn(user, login.Id.ToString());
                    return RedirectToAction("Admin", "Index");
                }
                ModelState.AddModelError("", "用户名或密码错误");
            }
            catch (Exception e)
            {
                AddActionException(e);
            }
            return View(model);
        }


        #endregion
        
        

        private void AddActionException(Exception e)
        {
            Response.Cookies.Add(new HttpCookie("error") { Value = HttpUtility.UrlEncode(e.Message, Encoding.Default) });
            ModelState.AddModelError("", e);
        }
    }
}