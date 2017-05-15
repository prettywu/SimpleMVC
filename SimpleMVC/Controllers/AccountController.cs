using SimpleMVC.BLL;
using SimpleMVC.Entitys;
using SimpleMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SimpleMVC.Entitys.Enums;

namespace SimpleMVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult Login()
        {
            return View();
        }

        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = UserManager.LoginPasswordCheck(model.UserName, model.Password);
            if (user != null)
            {
                var login = new Login
                {
                    UserId = user.Id,
                    LoginTime = DateTime.Now,
                    IP = Common.GetHostAddress(),
                    AuthType = (int)AuthType.站内,
                    DeviceType = (int)DeviceType.WebBrowser
                };
                UserManager.WriteLoginInfo(login);
                AuthManager.Singout();
                AuthManager.SignIn(login.Id.ToString());
            }

            return null;
        }
    }
}