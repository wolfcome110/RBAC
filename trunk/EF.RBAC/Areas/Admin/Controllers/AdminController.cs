using EF.Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EF.RBAC.Areas.AdminPage.Controllers
{

    public class AdminController : Controller
    {
        private RBACEntities db = new RBACEntities();
        //public AdminController()
        //    : this(GlobalConfiguration.Configuration)
        //{
        //}

        //public AdminController(HttpConfiguration config)
        //{
        //    Configuration = config;
        //}

        //public HttpConfiguration Configuration { get; private set; }

        // GET: AdminPage/Admin
        public string Index()
        {
            return "aaaaaa";
        }

        public ActionResult Login()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Login(Model.Sys_Users model)
        {
            try
            {
                string userName = model.UserName;
                string pwd = model.Password;
                string checkCode = Request["code"];
                string remember = Request["isRemember"];

                if (string.IsNullOrEmpty(checkCode) && checkCode != Convert.ToString(Session["code"]))
                {
                    return Json(new
                    {
                        success = false,
                        message = "验证码错误！"
                    });
                }
                db.Configuration.ProxyCreationEnabled = false;
                var rows = db.Set<Sys_Users>().Where(o => o.UserName == userName);
                if (rows.Count() > 0)
                {
                    pwd = Common.Helper.MD5(Common.Helper.MD5(pwd).ToLower() + rows.FirstOrDefault().PassSalt).ToLower();
                    rows = db.Set<Sys_Users>()
                        //.Include(o => o.Sys_Users_Departments)
                        //.Include(o => o.Sys_Users_Groups)
                        //.Include(o => o.Sys_Users_Roles)
                        //.Include(o => o.Sys_UserNavBtns)
                        .Where(o => o.UserName == userName && o.Password == pwd);
                    if (rows.Count() > 0)
                    {
                        Sys_Users user = rows.FirstOrDefault();

                        // 1. 创建一个FormsAuthenticationTicket，它包含登录名以及额外的用户数据。
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, user.UserName, DateTime.Now, DateTime.Now.AddDays(2), true, JsonHelper.SerializeData(user));

                        // 2. 加密Ticket，变成一个加密的字符串。
                        string cookieValue = FormsAuthentication.Encrypt(ticket);

                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue);
                        cookie.HttpOnly = true;
                        cookie.Secure = FormsAuthentication.RequireSSL;
                        cookie.Domain = FormsAuthentication.CookieDomain;
                        cookie.Path = FormsAuthentication.FormsCookiePath;

                        HttpContext context = System.Web.HttpContext.Current;
                        context.Response.Cookies.Remove(cookie.Name);
                        context.Response.Cookies.Add(cookie);

                        return Json(new
                        {
                            success = true,
                            message = "ok！"
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            message = "用户名或密码错误！"
                        });
                    }
                }
                return Json(new
                {
                    success = false,
                    message = "用户名或密码错误！"
                });

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = true,
                    message = "操作异常！"
                });
            }
        }

        [HttpPost]
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
