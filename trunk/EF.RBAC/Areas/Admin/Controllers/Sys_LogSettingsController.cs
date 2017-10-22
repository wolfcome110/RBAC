using EF.Common;
using EF.RBAC.Models;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EF.RBAC.Controllers
{
    public class Sys_LogSettingsController : BaseController
    {
        private RBACEntities db = new RBACEntities();
        //
        // GET: /Sys_LogSettings/
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

            List<Sys_LogSettings> list = db.Sys_LogSettings.ToList();

            return Json(new
            {
                total = list.Count(),
                rows = list.ToArray()
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
                var obj = MvcHelper.BacthSave<Sys_LogSettings>(insertedRows, updatedRows, deletedRows);
                return Json(obj, "text/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = "保存失败！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
            
        }
	}
}