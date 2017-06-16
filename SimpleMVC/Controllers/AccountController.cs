using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SimpleMvc.Identity;
using SimpleMVC.ViewModels;
using SimpleMvc.Entitys;
using static SimpleMvc.Entitys.Enums;
using SimpleMvc.Common;
using SimpleMvc.Business;

namespace SimpleMVC.Controllers
{
    public class AccountController : Controller
    {
        public UserService userManager = new UserService();

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

        
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
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
                        IP = Helper.GetHostAddress(),
                        AuthType = (int)AuthType.站内,
                        DeviceType = (int)DeviceType.WebBrowser
                    };
                    userManager.WriteLoginInfo(login);
                    SimpleAuthentication.SignIn(user,login.Id.ToString());
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
        public ActionResult Register(RegisterViewModel model)
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
                        PasswordHash = Helper.MD5encryption(model.Password),
                        NickName = model.NickName,
                        HeadImage = model.HeadImage,
                        Birthday = new DateTime(1990, 1, 29),
                        Gender = (int)Gender.Male,
                        RegistTime = DateTime.Now,
                        LastUpdateTime = DateTime.Now
                    };
                    if (userManager.AddUser(user))
                    {
                        //登录
                        var login = new Login
                        {
                            UserId = user.Id,
                            LoginTime = DateTime.Now,
                            IP = Helper.GetHostAddress(),
                            AuthType = (int)AuthType.站内,
                            DeviceType = (int)DeviceType.WebBrowser
                        };
                        userManager.WriteLoginInfo(login);      //记录登录记录
                        SimpleAuthentication.SignIn(user,login.Id.ToString());//写入登录cookie
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