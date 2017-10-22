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
using System.Web.Security;
using EF.RBAC.Models;

namespace EF.RBAC.Controllers
{
    public class Sys_RolesController : BaseController
    {
        private RBACEntities db = new RBACEntities();

        // GET: /Sys_Roles/
        [AuthenticateAttribute]
        public ActionResult Index(int NavId = 0)
        {
            string cookie = CookieHelper.GetCookie(FormsAuthentication.FormsCookieName);

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie);
            Sys_Users user = JsonHelper.DeserializeData<Sys_Users>(ticket.UserData);
            //user=
            //var routeData = string.Format("{0}/{1}", RouteData.Values["controller"], RouteData.Values["action"]);// ;
            var authority = Users.UserAuthority(user, NavId);
            ViewBag.UserAuthority = authority;
            return View();
        }

        [HttpPost]
        public ActionResult Index(int? page, int? rows)
        {
            page = page == null ? 1 : page;
            rows = rows == null ? 20 : rows;

            //db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            List<Sys_Roles> rolesList = db.Sys_Roles.ToList();
            var list = MvcHelper.TreeGrid<Sys_Roles>(0, rolesList);
            return Json(new
            {
                total = rolesList.Count(),
                rows = rolesList.ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult BatchSave()
        {
            try
            {
                string insertedRows = Request["insertedRows"];
                string updatedRows = Request["updatedRows"];
                string deletedRows = Request["deletedRows"];
                var obj = MvcHelper.BacthSave<Sys_Roles>(insertedRows, updatedRows, deletedRows);
                return Json(obj, "text/json", JsonRequestBehavior.AllowGet);
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
