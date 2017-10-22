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

namespace EF.RBAC.Controllers
{
    public class Sys_DepartmentsController : BaseController
    {
        private RBACEntities db = new RBACEntities();
        
        //Include="Id,DepartmentName,ParentId,SortNum,Remark,AddTime"
        // GET: /Sys_Departments/
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
        public JsonResult Index(int? page, int? rows)
        {
            page = page == null ? 1 : page;
            rows = rows == null ? 20 : rows;

            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            List<Sys_Departments> depList = db.Sys_Departments.ToList();
            var list = MvcHelper.TreeGrid<Sys_Departments>(0, depList);
            return Json(new
            {
                total = depList.Count(),
                rows = list.ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<Sys_Departments> DepList(int DepID, List<Sys_Departments> Departments)
        {
            var deplist = Departments.Where(t => t.ParentId == DepID);

            foreach (var model in deplist)
            {
                model.children = Departments.Where(t => t.ParentId == model.Id) as IEnumerable<Sys_Departments>;
                DepList(model.Id, Departments);
            }
            return deplist as IEnumerable<Sys_Departments>;
        }

        [HttpPost]
        public JsonResult BacthSave()
        {
            try
            {
                string insertedRows = Request["insertedRows"];
                string updatedRows = Request["updatedRows"];
                string deletedRows = Request["deletedRows"];
                var obj = MvcHelper.BacthSave<Sys_Departments>(insertedRows, updatedRows, deletedRows);
                return Json(obj, "text/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = "保存失败！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult MoveNode(int SrcId, int DestId)
        {
            try
            {
                var model = db.Sys_Departments.Where(o => o.Id == SrcId).FirstOrDefault();
                model.ParentId = DestId;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, Message = "保存成功！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, Message = "保存失败！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Adds(Sys_Departments model)
        {
            try
            {
                db.Sys_Departments.Add(model);
                db.SaveChanges();

                return Json(new { success = true, Message = "保存成功！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = "保存失败！" }, "text/json", JsonRequestBehavior.AllowGet);
            }

        }



        #region MyRegion

        // GET: /Sys_Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_Departments sys_departments = db.Sys_Departments.Find(id);
            if (sys_departments == null)
            {
                return HttpNotFound();
            }
            return View(sys_departments);
        }

        // GET: /Sys_Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Sys_Departments/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DepartmentName,ParentId,SortNum,Remark,AddTime")] Sys_Departments sys_departments)
        {
            if (ModelState.IsValid)
            {
                db.Sys_Departments.Add(sys_departments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sys_departments);
        }

        // GET: /Sys_Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_Departments sys_departments = db.Sys_Departments.Find(id);
            if (sys_departments == null)
            {
                return HttpNotFound();
            }
            return View(sys_departments);
        }

        // POST: /Sys_Departments/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepartmentName,ParentId,SortNum,Remark,AddTime")] Sys_Departments sys_departments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sys_departments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sys_departments);
        }

        // GET: /Sys_Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_Departments sys_departments = db.Sys_Departments.Find(id);
            if (sys_departments == null)
            {
                return HttpNotFound();
            }
            return View(sys_departments);
        }

        // POST: /Sys_Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sys_Departments sys_departments = db.Sys_Departments.Find(id);
            db.Sys_Departments.Remove(sys_departments);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        #endregion
    }
}
