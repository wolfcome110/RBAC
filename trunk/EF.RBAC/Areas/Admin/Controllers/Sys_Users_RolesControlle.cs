using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;




namespace EF.RBAC.Controllers
{
    public class Sys_Users_RolesController : BaseController
    {
        private RBACEntities db = new RBACEntities();
        //
        // GET: /Sys_Users_Roles/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Users_Roles(Sys_Users User)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var UserRoles = db.Set<Sys_Users_Roles>().Include(o => o.Sys_Users).Where(o => o.UserId == User.Id);
                var roles = from role in UserRoles select role.Sys_Roles;

                return Json(new
                {
                    success = true, 
                    Message = "",
                    total = roles.Count(),
                    rows = roles,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = "保存失败！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Index(int? page, int? rows)
        {
            page = page == null ? 1 : page;
            rows = rows == null ? 20 : rows;

            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            List<Sys_Users_Roles> userRolesList = db.Sys_Users_Roles.ToList();
            var list = MvcHelper.TreeGrid<Sys_Users_Roles>(0, userRolesList);
            return Json(new
            {
                total = userRolesList.Count(),
                rows = list.ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Save(string Users, string Roles)
        {
            try
            {
                List<Sys_Users> users = Common.JsonHelper.DeserializeData<List<Sys_Users>>(Users);
                List<Sys_Roles> roles = Common.JsonHelper.DeserializeData<List<Sys_Roles>>(Roles);

                foreach (var user in users)
                {
                    foreach (var role in roles)
                    {
                        if (db.Set<Sys_Users_Roles>().Where(o => o.UserId == user.Id && o.RoleId == role.Id).Count() == 0)
                        {
                            db.Set<Sys_Users_Roles>().Add(new Sys_Users_Roles { UserId = user.Id, RoleId = role.Id, AddTime = DateTime.Now });
                        }
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