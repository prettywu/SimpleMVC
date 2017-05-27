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
using System.Linq.Expressions;

namespace SimpleMVC.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public UserManager userManager = new UserManager();

        #region Views
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }

        public ViewResult Lock()
        {
            return View();
        }
        
        public ViewResult UserManagement()
        {
            return View();
        }

        public ViewResult MyAccount()
        {
            var user = User.GetEntity().GetUserInfo<User>();
            return View(user);
        }

        public ViewResult ReSetPassword()
        {
            return View();
        }

        #region Demo
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
        #endregion
        #endregion

        #region Apis
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(AdminLoginModel model)
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
                    return RedirectToAction("Index", "Admin");
                }
                ModelState.AddModelError("", "用户名或密码错误");
            }
            catch (Exception e)
            {
                AddActionException(e);
            }
            return View(model);
        }

        
        public JsonResult GetUserList(string username,string nickname,int role=0,int state=0,int page=1,int pagesize=10)
        {
            Expression<Func<User, bool>> select = u => u.UserName == username && u.NickName == nickname;
            Expression<Func<User, DateTime>> sort = u => u.RegistTime;
            int total = 0;
            var user = userManager.getPageDate<User, DateTime>(select, sort, 1, 10, out total);
            return new JsonResult()
            {
                Data = user
            };
        }
        #endregion



        private void AddActionException(Exception e)
        {
            Response.Cookies.Add(new HttpCookie("error") { Value = HttpUtility.UrlEncode(e.Message, Encoding.Default) });
            ModelState.AddModelError("", e);
        }
    }
}