using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using System.Collections.Specialized;
using EF.Common;

namespace EF.RBAC.Controllers
{
    public class Sys_Roles_DepartmentsController : BaseController
    {
        private RBACEntities db = new RBACEntities();

        // GET: /Sys_Roles_Departments/
        public ActionResult Index()
        {
            var sys_roles_departments = db.Sys_Roles_Departments.Include(s => s.Sys_Departments).Include(s => s.Sys_Roles);
            return View(sys_roles_departments.ToList());
        }

        /// <summary>
        /// 分配角色与部门
        /// </summary>
        /// <param name="Roles">角色信息，格式：{Roles : ""}</param>
        /// <param name="Departments">部门信息：{Departments : ""}</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Save(string Roles, string Departments)
        {
            try
            {
                List<Sys_Roles> roles = JsonHelper.DeserializeData<List<Sys_Roles>>(Roles);
                List<Sys_Departments> departments = JsonHelper.DeserializeData<List<Sys_Departments>>(Departments);
                foreach (var role in roles)
                {
                    foreach (var dep in departments)
                    {
                        if (db.Set<Sys_Roles_Departments>().Where(o => o.RoleId == role.Id && o.DepId == dep.Id).Count() == 0)
                        {
                            db.Set<Sys_Roles_Departments>().Add(new Sys_Roles_Departments() { RoleId = role.Id, DepId = dep.Id, Remark = string.Format("{0}--{1}", role.RoleName, dep.DepartmentName), AddTime = DateTime.Now });
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

        #region 参考

        // GET: /Sys_Roles_Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_Roles_Departments sys_roles_departments = db.Sys_Roles_Departments.Find(id);
            if (sys_roles_departments == null)
            {
                return HttpNotFound();
            }
            return View(sys_roles_departments);
        }

        // GET: /Sys_Roles_Departments/Create
        public ActionResult Create()
        {
            ViewBag.DepId = new SelectList(db.Sys_Departments, "Id", "DepartmentName");
            ViewBag.RoleId = new SelectList(db.Sys_Roles, "Id", "RoleName");
            return View();
        }

        // POST: /Sys_Roles_Departments/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoleId,DepId,AddTime,Remark")] Sys_Roles_Departments sys_roles_departments)
        {
            if (ModelState.IsValid)
            {
                db.Sys_Roles_Departments.Add(sys_roles_departments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepId = new SelectList(db.Sys_Departments, "Id", "DepartmentName", sys_roles_departments.DepId);
            ViewBag.RoleId = new SelectList(db.Sys_Roles, "Id", "RoleName", sys_roles_departments.RoleId);
            return View(sys_roles_departments);
        }

        // GET: /Sys_Roles_Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_Roles_Departments sys_roles_departments = db.Sys_Roles_Departments.Find(id);
            if (sys_roles_departments == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepId = new SelectList(db.Sys_Departments, "Id", "DepartmentName", sys_roles_departments.DepId);
            ViewBag.RoleId = new SelectList(db.Sys_Roles, "Id", "RoleName", sys_roles_departments.RoleId);
            return View(sys_roles_departments);
        }

        // POST: /Sys_Roles_Departments/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoleId,DepId,AddTime,Remark")] Sys_Roles_Departments sys_roles_departments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sys_roles_departments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepId = new SelectList(db.Sys_Departments, "Id", "DepartmentName", sys_roles_departments.DepId);
            ViewBag.RoleId = new SelectList(db.Sys_Roles, "Id", "RoleName", sys_roles_departments.RoleId);
            return View(sys_roles_departments);
        }

        // GET: /Sys_Roles_Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_Roles_Departments sys_roles_departments = db.Sys_Roles_Departments.Find(id);
            if (sys_roles_departments == null)
            {
                return HttpNotFound();
            }
            return View(sys_roles_departments);
        }

        // POST: /Sys_Roles_Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sys_Roles_Departments sys_roles_departments = db.Sys_Roles_Departments.Find(id);
            db.Sys_Roles_Departments.Remove(sys_roles_departments);
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
