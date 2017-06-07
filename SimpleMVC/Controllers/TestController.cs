using SimpleMvc.Common;
using SimpleMvc.DAL;
using SimpleMvc.Entitys;
using SimpleMvc.Identity;
using SimpleMVC.Models;
using SimpleMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static SimpleMvc.Entitys.Enums;

namespace SimpleMVC.Controllers
{
    public class TestController : Controller
    {
        private DbService _dbservice = new DbService();

        public DbService dbservice
        {
            get
            {
                if (_dbservice == null)
                    _dbservice = new DbService();
                return _dbservice;
            }
        }

        #region Views
        // GET: Test
        [Authentication]
        public ActionResult Index()
        {
            return View();
        }
        [Authentication]
        public ActionResult Test()
        {
            return View();
        }
        [Authentication]
        public ActionResult Test2()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Test");
            return View();
        }

        [Authentication(DisLock =true)]
        public ViewResult Lock(string backurl = "/Test/index")
        {
            SimpleAuthentication.LockOut();
            ViewBag.backurl = backurl;
            return View();
        }
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
                    return RedirectToAction("Index", "Test");
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
        [Authentication(DisLock =true)]
        public ActionResult UnLock(string password,string backurl="/Test/index")
        {
            if (!string.IsNullOrEmpty(password))
            {
                var username = User.GetSimpleInstance().SimpleIdentity.GetUser<User>();
                var user = dbservice.LoginPasswordCheck(username.UserName, password);
                if (user != null)
                {
                    SimpleAuthentication.Unlock();
                    return Redirect(backurl);
                }
                else
                {
                    ModelState.AddModelError("", "用户名或密码错误");
                }
            }
            else
            {
                ModelState.AddModelError("", "参数不合法");
            }
            return View("~/Views/Test/Lock.cshtml");
        }

        [Authentication]
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
                IEnumerable<UserModel> usermodels=null;
                int total = 0;
                switch (model.sortname)
                {
                    case "nickname":
                        users = dbservice.getPageDate(select, u => u.NickName, model.page, model.pagesize, out total);
                        break;
                    case "Birthday":
                        users = dbservice.getPageDate(select, u => u.Birthday, model.page, model.pagesize, out total);
                        break;
                    case "Gender":
                        users = dbservice.getPageDate(select, u => u.Gender, model.page, model.pagesize, out total);
                        break;
                    default:
                        users = dbservice.getPageDate(select, u => u.RegistTime, model.page, model.pagesize, out total);
                        break;

                }

                if (users != null)
                {
                    usermodels = users.Select(u => u.ConvertToModel());
                }

                return new JsonPage(model.page, model.pagesize, total, usermodels, JsonRequestBehavior.AllowGet);
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