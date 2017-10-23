using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;

namespace EF.RBAC.Controllers
{
    public class Sys_Users_GroupsController : BaseController
    {
        private RBACEntities db = new RBACEntities();

        // GET: /Sys_Users_Groups/
        public ActionResult Index()
        {
            var sys_users_groups = db.Sys_Users_Groups.Include(s => s.Sys_Groups).Include(s => s.Sys_Users);
            return View();
        }

        /// <summary>
        /// 小组的成员
        /// </summary>
        /// <param name="Group">小组信息</param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public JsonResult Users_Groups(string Group)
        {
            try
            {
                Model.Sys_Groups group = Common.JsonHelper.DeserializeData<Sys_Groups>(Group);
                db.Configuration.ProxyCreationEnabled = false;
                var users = db.Sys_Users_Groups.Include(o => o.Sys_Users).Where(o => o.GroupId == group.Id).Select(o=>o.Sys_Users);
                return Json(new { total = users.Count(), rows = users.ToArray() }, "text/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = "Error" }, "text/json", JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 创建一个小组中的成员（可以批量进行分配）
        /// </summary>
        /// <param name="Users">成员信息</param>
        /// <param name="Groups">小组信息</param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateInput(false)]
        public JsonResult Save(string Users, string Groups)
        {
            try
            {
                List<Sys_Users> users = Common.JsonHelper.DeserializeData<List<Sys_Users>>(Users);
                List<Sys_Groups> groups = Common.JsonHelper.DeserializeData<List<Sys_Groups>>(Groups);

                foreach (var user in users)
                {
                    foreach (var dep in groups)
                    {
                        if (db.Set<Sys_Users_Groups>().Where(o => o.UserId == user.Id && o.GroupId == dep.Id).Count() == 0)
                        {
                            db.Set<Sys_Users_Groups>().Add(new Sys_Users_Groups { UserId = user.Id, GroupId = dep.Id, AddTime = DateTime.Now });
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
