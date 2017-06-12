

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SimpleMvc.Identity;
using System.Linq.Expressions;
using SimpleMVC.ViewModels;
using SimpleMvc.Entitys;
using SimpleMvc.Common;
using static SimpleMvc.Entitys.Enums;
using SimpleMvc.DAL;

namespace SimpleMVC.Controllers
{
    [Authentication]
    public class AdminController : Controller
    {
        public DbService dbservice = new DbService();

        #region Views
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult Unauthorized()
        {
            return View();
        }

        public ViewResult Unauthenticate()
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

        [SimpleAuthorize(Roles = "User")]
        public ViewResult UserManagement()
        {
            return View();
        }

        [SimpleAuthorize(Roles = "Admin")]
        public ViewResult MyAccount()
        {
            var user = User.GetSimpleInstance().GetUser<User>();
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
        public ActionResult Login(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                var user = dbservice.LoginPasswordCheck(model.UserName, model.Password);
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
                    dbservice.WriteLoginInfo(login);
                    SimpleAuthentication.SignIn(user, login.Id.ToString());
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

        [AllowAnonymous]
        public JsonResult GetUserList(UserSearchViewModel model)
        {
            try
            {
                Expression<Func<User, bool>> select = null;
                
                if (!string.IsNullOrEmpty(model.username))
                {
                    if (!string.IsNullOrEmpty(model.nickname))
                    {
                        select = u => u.UserName.Contains(model.username) && u.NickName.Contains(model.nickname);
                    }
                    else
                    {
                        select = u => u.UserName.Contains(model.username);
                    }
                }

                List<User> users;
                int total = 0;
                switch (model.sortname)
                {
                    case "nickname":
                        users = dbservice.getPageDate(select, u=>u.NickName, 0, model.page, model.pagesize, out total);
                        break;
                    case "Birthday":
                        users = dbservice.getPageDate(select, u => u.Birthday, 0, model.page, model.pagesize, out total);
                        break;
                    case "Gender":
                        users = dbservice.getPageDate(select, u => u.Gender, 0, model.page, model.pagesize, out total);
                        break;
                    default:
                        users = dbservice.getPageDate(select, u => u.RegistTime, 0, model.page, model.pagesize, out total);
                        break;

                }

                return new JsonPage(model.page, model.pagesize, total, users, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new Json(e.Message);
            }

        }
        #endregion



        private void AddActionException(Exception e)
        {
            Response.Cookies.Add(new HttpCookie("error") { Value = HttpUtility.UrlEncode(e.Message, Encoding.Default) });
            ModelState.AddModelError("", e);
        }


    }
}