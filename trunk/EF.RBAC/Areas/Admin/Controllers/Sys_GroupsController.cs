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
    public class Sys_GroupsController : BaseController
    {
        private RBACEntities db = new RBACEntities();

        // GET: /Sys_Groups/
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

            db.Configuration.ProxyCreationEnabled = false; ;
            List<Sys_Groups> groupList = db.Sys_Groups.ToList();
            var list = MvcHelper.TreeGrid<Sys_Groups>(0, groupList);
            return Json(new
            {
                total = groupList.Count(),
                rows = list.ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<Sys_Groups> TreeGrid(int GroupID, List<Sys_Groups> Sys_Groups)
        {
            var grouplist = Sys_Groups.Where(t => t.ParentId == GroupID);

            foreach (var model in grouplist)
            {
                model.children = Sys_Groups.Where(t => t.ParentId == model.Id) as IEnumerable<Sys_Groups>;
                TreeGrid(model.Id, Sys_Groups);
            }
            return grouplist as IEnumerable<Sys_Groups>;
        }

        [HttpPost]
        public JsonResult BacthSave()
        {
            try
            {
                string insertedRows = Request["insertedRows"];
                string updatedRows = Request["updatedRows"];
                string deletedRows = Request["deletedRows"];
                var obj = MvcHelper.BacthSave<Sys_Groups>(insertedRows, updatedRows, deletedRows);
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
