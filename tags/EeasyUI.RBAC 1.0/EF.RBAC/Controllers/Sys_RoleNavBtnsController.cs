using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using Newtonsoft.Json.Linq;

namespace EF.RBAC.Controllers
{
    public class Sys_RoleNavBtnsController : BaseController
    {
        private RBACEntities db = new RBACEntities();

        // GET: /Sys_RoleNavBtns/
        public ActionResult Index()
        {
            var sys_rolenavbtns = db.Sys_RoleNavBtns.Include(s => s.Sys_Buttons).Include(s => s.Sys_Navigations).Include(s => s.Sys_Roles);
            return View(sys_rolenavbtns.ToList());
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Save(string Roles, string Navigations, string Buttons)
        {
            try
            {
                string a = Request["Roles"];
                List<Sys_Roles> roles = Common.JsonHelper.DeserializeData<List<Sys_Roles>>(Roles);
                List<Sys_Navigations> navigations = Common.JsonHelper.DeserializeData<List<Sys_Navigations>>(Navigations);
                List<Sys_Buttons> buttons = Common.JsonHelper.DeserializeData<List<Sys_Buttons>>(Buttons);

                foreach (var role in roles)
                {
                    foreach (var nav in navigations)
                    {
                        foreach (var button in buttons)
                        {
                            if (db.Set<Sys_RoleNavBtns>().Where(o => o.RoleId == role.Id && o.NavId == nav.Id && o.BtnId == button.Id).Count() == 0)
                            {
                                db.Set<Sys_RoleNavBtns>().Add(new Sys_RoleNavBtns { RoleId = role.Id, NavId = nav.Id, BtnId = button.Id, AddTime = DateTime.Now });
                            }
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

        /// <summary>
        /// 适合批量给角色分配权限
        /// </summary>
        /// <param name="Roles"></param>
        /// <param name="Navigations"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RoleNavBtns(string Roles, string Navigations)
        {
            try
            {
                var btns = db.Sys_Buttons;
                List<Sys_Roles> roles = Common.JsonHelper.DeserializeData<List<Sys_Roles>>(Roles);
                List<JObject> navigations = Common.JsonHelper.DeserializeData<List<JObject>>(Navigations);

                foreach (var role in roles)
                {
                    foreach (var nav in navigations)
                    {
                        int navId = (int)nav["Id"];
                        IJEnumerable<JToken> tokens = nav.Values();
                        foreach (var token in tokens)
                        {
                            //检测是不是按钮
                            if (btns.Where(o => o.ButtonTag == token.Path).Count() > 0)
                            {
                                int btnId = btns.Where(o => o.ButtonTag == token.Path).FirstOrDefault().Id;

                                if (token.ToString().Trim() == "√")
                                {
                                    db.Sys_RoleNavBtns.Add(new Sys_RoleNavBtns() { RoleId = role.Id, NavId = navId, BtnId = btnId });
                                }
                                else
                                {
                                    var roleNavBtn = db.Sys_RoleNavBtns.Where(o => o.RoleId == role.Id && o.NavId == navId && o.BtnId == btnId).FirstOrDefault();
                                    if (roleNavBtn != null)
                                    {
                                        db.Entry(roleNavBtn).State = EntityState.Deleted;
                                    }

                                }
                            }
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
