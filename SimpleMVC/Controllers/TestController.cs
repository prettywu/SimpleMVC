using Microsoft.AspNet.SignalR;
using SimpleMvc.Business;
using SimpleMvc.Common;
using SimpleMvc.DAL;
using SimpleMvc.Entitys;
using SimpleMvc.Identity;
using SimpleMVC.Hubs;
using SimpleMVC.Models;
using SimpleMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
        private DbService _dbservice;
        private UserService _userService;
        public DbService dbservice
        {
            get
            {
                if (_dbservice == null)
                    _dbservice = new DbService();
                return _dbservice;
            }
        }
        public UserService userService
        {
            get
            {
                if (_userService == null)
                    _userService = new UserService();
                return _userService;
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

        [SimpleAuthorize(Roles = "管理员,超级管理员")]
        public ActionResult AddUser()
        {
            return View();
        }

        public ViewResult LawsuitAdd()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Test");
            return View();
        }

        [Authentication(DisLock = true)]
        public ViewResult Lock(string backurl = "/Test/index")
        {
            SimpleAuthentication.LockOut();
            ViewBag.backurl = backurl;
            return View();
        }

        [Authentication]
        public ActionResult Logout()
        {
            SimpleAuthentication.Singout();
            return RedirectToAction("Login", "Test");
        }
        #endregion

        #region Apis

        public ActionResult Message(string user, string message,string ids="")
        {
            if (string.IsNullOrEmpty(user))
                GlobalHost.ConnectionManager.GetHubContext<MessageHub>().Clients.All.receive(user, message);
            else
                GlobalHost.ConnectionManager.GetHubContext<MessageHub>().Clients.Clients(ids.Split(new char[] { ',' })).receive(user, message);
            return Json("true", JsonRequestBehavior.AllowGet);
        }

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
                    //广播消息
                    GlobalHost.ConnectionManager.GetHubContext<MessageHub>().Clients.All.receiveMessage(user.NickName, "登陆了");

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
        [Authentication(DisLock = true)]
        public ActionResult UnLock(string password, string backurl = "/Test/index")
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
                int total = 0;
                List<User> users = userService.GetUserList(model.username, model.nickname, model.state, model.sortname, model.sorttype, model.page, model.pagesize, out total);
                var usermodels = users.Select(u => u.ConvertToModel());


                return new JsonPage(model.page, model.pagesize, total, usermodels, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new Json(e.Message);
            }

        }

        [HttpPost]
        [Authentication]
        public JsonResult AddUser(AddUserViewModel model)
        {
            try
            {
                var user = new User()
                {
                    Id=Guid.NewGuid(),
                    UserName = model.email,
                    PasswordHash = Helper.MD5encryption(model.email),
                    NickName = model.nickname,
                    Birthday = Convert.ToDateTime(model.birthday),
                    Gender = model.gender,
                    HeadImage = model.headimage,
                    IsDeleted = false,
                    State = 1,
                    RegistTime = DateTime.Now,
                    LastUpdateTime = DateTime.Now
                };
                bool result = new UserService().AddUser(user);

                if (result)
                    return new Json(true, 200, "添加用户成功");
                else
                    return new Json(false, 500, "添加用户失败");
            }
            catch (Exception e)
            {
                return new Json(false, 999, e.Message);
            }

        }

        [HttpPost]
        public ActionResult FileUpload()
        {
            HttpPostedFileBase file = Request.Files["file"];
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string directory = HttpContext.Server.MapPath("/HeadImages/");
                string filePath = directory + filename;
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                file.SaveAs(filePath);
                return new Json(true, 200, "上传成功", "/HeadImages/" + filename);
            }
            else
            {
                return new Json("上传失败");
            }
        }

        [Authentication]
        public JsonResult GetRoles()
        {
            return null;
        }
        #endregion

        private void AddActionException(Exception e)
        {
            Response.Cookies.Add(new HttpCookie("error") { Value = HttpUtility.UrlEncode(e.Message, Encoding.Default) });
            ModelState.AddModelError("", e);
        }
    }
}