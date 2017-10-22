using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using EF.Common;
using EF.RBAC.Models;
using System.Web.Security;


namespace EF.RBAC.Areas.AdminPage.Controllers
{
    public class Sys_UsersController : Controller
    {
        private RBACEntities db = new RBACEntities();

        // GET: /Sys_Users/
        //[AuthenticateAttribute]
        public ActionResult Index(int NavId = 0)
        {
            string cookie = CookieHelper.GetCookie(FormsAuthentication.FormsCookieName);
            
            //FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie);
            //Sys_Users user = JsonHelper.DeserializeData<Sys_Users>(ticket.UserData);
            ////user=
            ////var routeData = string.Format("{0}/{1}", RouteData.Values["controller"], RouteData.Values["action"]);// ;
            //var authority = Users.UserAuthority(user, NavId);
            //ViewBag.UserAuthority = authority;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
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

        /// <summary>
        /// 获取职员操作权限
        /// </summary>
        /// <param name="NavId"></param>
        /// <returns></returns>
        //[HttpPost]
        //public string UserAuthority(int NavId = 0)
        //{
        //    Sys_Users user = Session["user"] as Sys_Users;
        //    var routeData = string.Format("{0}/{1}", RouteData.Values["controller"], RouteData.Values["action"]);// ;
        //    return Users.UserAuthority(user, NavId);
        //}


        /// <summary>
        /// 获取职员信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="DepartmentId">所属部门的编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(int? page, int? rows, int DepartmentId = 0)
        {
            page = page == null ? 1 : page;
            rows = rows == null ? 20 : rows;
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            List<Sys_Users> userList;
            if (DepartmentId > 0)
            {
                var departments = (from dep1 in db.Sys_Departments
                                   from dep2 in db.Sys_Departments
                                   where dep1.Id == DepartmentId && dep2.ParentId == dep1.Id
                                   select dep2).Union(
                                   from dep in db.Sys_Departments
                                   where dep.Id == DepartmentId
                                   select dep
                                  );

                userList = db.Sys_Users.Include(o => o.Sys_Users_Departments).Where(o => o.Sys_Users_Departments.Where(m => departments.Select(dep => dep.Id).Contains(m.DepId)).Count() > 0).ToList();
            }
            else
            {
                userList = db.Sys_Users.ToList();
            }

            return Json(new
            {
                total = userList.Count(),
                rows = userList.Select(o => new { Id = o.Id, UserName = o.UserName, TrueName = o.TrueName, IsDisabled = o.IsDisabled, Mobile = o.Mobile })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BatchSave()
        {
            try
            {
                string insertedRows = Request["insertedRows"];
                string updatedRows = Request["updatedRows"];
                string deletedRows = Request["deletedRows"];

                //添加
                if (!string.IsNullOrEmpty(insertedRows))
                {
                    List<Sys_Users> insertedList = JsonHelper.DeserializeData<List<Sys_Users>>(insertedRows);
                    foreach (var btn in insertedList)
                    {
                        btn.Password = Helper.MD5(Helper.MD5(btn.Password) + btn.PassSalt);
                        db.Sys_Users.Add(btn);
                    }
                    //db.Sys_Buttons.Add(unicorn);//添加到上下文中
                    //插入到数据库中
                }

                //修改
                if (!string.IsNullOrEmpty(updatedRows))
                {
                    List<Sys_Users> updatedList = JsonHelper.DeserializeData<List<Sys_Users>>(updatedRows);
                    foreach (var btn in updatedList)
                    {
                        db.Entry(btn).State = EntityState.Modified;
                    }

                }

                //删除
                if (!string.IsNullOrEmpty(deletedRows))
                {
                    List<Sys_Users> deletedList = JsonHelper.DeserializeData<List<Sys_Users>>(deletedRows);
                    foreach (var btn in deletedList)
                    {
                        db.Entry(btn).State = EntityState.Deleted;
                        //db.Sys_Buttons.Remove(btn);//也可以使用这个方法
                    }
                }
                db.SaveChanges();
                return Json(new { success = true, Message = "保存成功！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = "保存失败！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Creates([Bind(Include = "Id,UserName,Password,PassSalt,Email,IsDisabled,TrueName,Mobile,QQ,MenusJson,ConfigJson,Remark,AddTime")] Sys_Users sys_users)
        {

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        // GET: /Sys_Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_Users sys_users = db.Sys_Users.Find(id);
            if (sys_users == null)
            {
                return HttpNotFound();
            }
            return View(sys_users);
        }

        public ActionResult CheckCode()
        {
            //生成验证码
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["code"] = code;
            byte[] bytes = validateCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }


        #region MyRegion

        // GET: /Sys_Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Sys_Users/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Password,PassSalt,Email,IsDisabled,TrueName,Mobile,QQ,MenusJson,ConfigJson,Remark,AddTime")] Sys_Users sys_users)
        {
            if (ModelState.IsValid)
            {
                db.Sys_Users.Add(sys_users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sys_users);
        }

        // GET: /Sys_Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_Users sys_users = db.Sys_Users.Find(id);
            if (sys_users == null)
            {
                return HttpNotFound();
            }
            return View(sys_users);
        }

        // POST: /Sys_Users/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Password,PassSalt,Email,IsDisabled,TrueName,Mobile,QQ,MenusJson,ConfigJson,Remark,AddTime")] Sys_Users sys_users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sys_users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sys_users);
        }

        // GET: /Sys_Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_Users sys_users = db.Sys_Users.Find(id);
            if (sys_users == null)
            {
                return HttpNotFound();
            }
            return View(sys_users);
        }

        // POST: /Sys_Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sys_Users sys_users = db.Sys_Users.Find(id);
            db.Sys_Users.Remove(sys_users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
