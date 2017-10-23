using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EF.Common;
using Model;
using System.Web.Security;
using EF.RBAC.Models;

namespace EF.RBAC.Controllers
{
    public class Sys_ButtonsController : BaseController
    {
        private RBACEntities db = new RBACEntities();

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
            try
            {


                page = page == null ? 1 : page;
                rows = rows == null ? 20 : rows;
                db.Configuration.ProxyCreationEnabled = false;
                List<Sys_Buttons> btnsList = db.Sys_Buttons.OrderBy(o=>o.SortNum).ToList();

                return Json(new
                {
                    total = btnsList.Count(),
                    rows = btnsList.ToArray()
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = "保存失败！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
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
                    List<Sys_Buttons> insertedList = JsonHelper.DeserializeData<List<Sys_Buttons>>(insertedRows);
                    foreach (var btn in insertedList)
                    {
                        db.Sys_Buttons.Add(btn);
                    }
                    //db.Sys_Buttons.Add(unicorn);//添加到上下文中
                    //插入到数据库中
                }

                //修改
                if (!string.IsNullOrEmpty(updatedRows))
                {
                    List<Sys_Buttons> updatedList = JsonHelper.DeserializeData<List<Sys_Buttons>>(updatedRows);
                    foreach (var btn in updatedList)
                    {
                        db.Entry(btn).State = EntityState.Modified;
                    }

                }

                //删除
                if (!string.IsNullOrEmpty(deletedRows))
                {
                    List<Sys_Buttons> deletedList = JsonHelper.DeserializeData<List<Sys_Buttons>>(deletedRows);
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

        // GET: /Sys_Buttons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_Buttons sys_buttons = db.Sys_Buttons.Find(id);
            if (sys_buttons == null)
            {
                return HttpNotFound();
            }
            return View(sys_buttons);
        }

        // GET: /Sys_Buttons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Sys_Buttons/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ButtonText,SortNum,IconCls,IconUrl,ButtonTag,ButtonHtml,Remark,AddTime")] Sys_Buttons sys_buttons)
        {
            if (ModelState.IsValid)
            {
                db.Sys_Buttons.Add(sys_buttons);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sys_buttons);
        }

        // GET: /Sys_Buttons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_Buttons sys_buttons = db.Sys_Buttons.Find(id);
            if (sys_buttons == null)
            {
                return HttpNotFound();
            }
            return View(sys_buttons);
        }

        // POST: /Sys_Buttons/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ButtonText,SortNum,IconCls,IconUrl,ButtonTag,ButtonHtml,Remark,AddTime")] Sys_Buttons sys_buttons)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sys_buttons).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sys_buttons);
        }

        // GET: /Sys_Buttons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_Buttons sys_buttons = db.Sys_Buttons.Find(id);
            if (sys_buttons == null)
            {
                return HttpNotFound();
            }
            return View(sys_buttons);
        }

        // POST: /Sys_Buttons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sys_Buttons sys_buttons = db.Sys_Buttons.Find(id);
            db.Sys_Buttons.Remove(sys_buttons);
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
    }
}
