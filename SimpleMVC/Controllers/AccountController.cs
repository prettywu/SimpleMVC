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
using SimpleMvc.Identity;

namespace SimpleMVC.Controllers
{
    public class AccountController : Controller
    {
        public UserManager userManager = new UserManager();

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

        [Authentication]
        public ViewResult Account()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Login(LoginModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return new Json
        //        {
        //            isSuccess = false,
        //            code = 100,
        //            message = ModelState.FirstOrDefault().Value.Errors.FirstOrDefault().ErrorMessage
        //        };
        //    try
        //    {
        //        var user = userManager.LoginPasswordCheck(model.UserName, model.Password);
        //        if (user == null)
        //            return new Json
        //            {
        //                isSuccess = false,
        //                code = 100,
        //                message = "用户名或密码错误"
        //            };

        //        var login = new Login
        //        {
        //            UserId = user.Id,
        //            LoginTime = DateTime.Now,
        //            IP = MvcHelper.GetHostAddress(),
        //            AuthType = (int)AuthType.站内,
        //            DeviceType = (int)DeviceType.WebBrowser
        //        };
        //        userManager.WriteLoginInfo(login);
        //        AuthManager.SignIn(login.Id.ToString());
        //        return new Json
        //        {
        //            isSuccess = true,
        //            code = 0,
        //            message = "登录成功"
        //        };
        //    }
        //    catch(Exception e)
        //    {
        //        Response.Cookies.Add(new HttpCookie("error") { Value = HttpUtility.UrlEncode(e.Message,Encoding.Default)});
        //        return new Json
        //        {
        //            isSuccess = true,
        //            code = 999,
        //            message = "系统异常"
        //        };
        //    }
        //}

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
                    AuthManager.SignIn(user,login.Id.ToString());
                    return RedirectToAction("Account", "Account");
                }
                ModelState.AddModelError("", "用户名或密码错误");
            }
            catch (Exception e)
            {
                AddActionException(e);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                var userExist = userManager.GetUserByUserName(model.UserName);
                if (userExist == null)
                {
                    //创建用户
                    var user = new User
                    {
                        UserName = model.UserName,
                        PasswordHash = MvcHelper.MD5encryption(model.Password),
                        NickName = model.NickName,
                        HeadImage = model.HeadImage,
                        Birthday = new DateTime(1990, 1, 29),
                        Gender = (int)Gender.Male,
                        RegistTime = DateTime.Now,
                        LastUpdateTime = DateTime.Now
                    };
                    if (userManager.RegistNewUser(user))
                    {
                        //登录
                        var login = new Login
                        {
                            UserId = user.Id,
                            LoginTime = DateTime.Now,
                            IP = MvcHelper.GetHostAddress(),
                            AuthType = (int)AuthType.站内,
                            DeviceType = (int)DeviceType.WebBrowser
                        };
                        userManager.WriteLoginInfo(login);      //记录登录记录
                        AuthManager.SignIn(user,login.Id.ToString());//写入登录cookie
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "登录失败");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "用户名已存在");
                }
            }
            catch (Exception e)
            {
                AddActionException(e);
            }

            return View(model);
        }

        private void AddActionException(Exception e)
        {
            Response.Cookies.Add(new HttpCookie("error") { Value = HttpUtility.UrlEncode(e.Message, Encoding.Default) });
            ModelState.AddModelError("", e);
        }
    }

}