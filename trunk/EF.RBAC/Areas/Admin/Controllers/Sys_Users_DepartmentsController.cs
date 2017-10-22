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
    public class Sys_Users_DepartmentsController : BaseController
    {
        private RBACEntities db = new RBACEntities();

        // GET: /Sys_Users_Departments/
        public ActionResult Index()
        {
            var sys_users_departments = db.Sys_Users_Departments.Include(s => s.Sys_Departments).Include(s => s.Sys_Users);
            return View();
        }

        /// <summary>
        /// 员工所属的部门
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult User_Departments(Sys_Users User)
        {
            try
            {
                
                var sys_users_departments = db.Set<Sys_Users_Departments>().Include(o => o.Sys_Departments).Where(o => o.UserId == User.Id);
                db.Configuration.ProxyCreationEnabled = false;
                var departments = from user_dept in sys_users_departments select user_dept.Sys_Departments;
                return Json(new
                {
                    success = true, 
                    Message = "",
                    total = departments.Count(),
                    rows = departments,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = "保存失败！" }, "text/json", JsonRequestBehavior.AllowGet);
            }


        }

        /// <summary>
        /// 分配员工跟部门
        /// </summary>
        /// <param name="Users">员工</param>
        /// <param name="Departments">部门</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Save(string Users, string Departments)
        {
            try
            {
                List<Sys_Users> users = Common.JsonHelper.DeserializeData<List<Sys_Users>>(Users);
                List<Sys_Departments> departments = Common.JsonHelper.DeserializeData<List<Sys_Departments>>(Departments);

                foreach (var user in users)
                {
                    var users_Departments = db.Set<Sys_Users_Departments>().Where(o => o.UserId == user.Id);
                    db.Set<Sys_Users_Departments>().RemoveRange(users_Departments);
                    db.SaveChanges();

                    foreach (var dep in departments)
                    {
                        if (db.Set<Sys_Users_Departments>().Where(o => o.UserId == user.Id && o.DepId == dep.Id).Count() == 0)
                        {
                            db.Set<Sys_Users_Departments>().Add(new Sys_Users_Departments { UserId = user.Id, DepId = dep.Id, AddTime = DateTime.Now });
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
