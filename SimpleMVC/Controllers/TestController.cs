using SimpleMvc.Common;
using SimpleMvc.DAL;
using SimpleMvc.Entitys;
using SimpleMvc.Identity;
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
        public DbService dbservice = new DbService();

        #region Views
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Test2()
        {
            return View();
        }

        public ViewResult Login()
        {
            return View();
        }
        #endregion

        #region Apis
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return new Json("参数错误");
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
                    return new Json();
                }
                return new Json(false, 200, "用户名或密码错误", null);
            }
            catch (Exception e)
            {
                AddActionException(e);
                return new Json(false, 999, "系统异常", null);
            }
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