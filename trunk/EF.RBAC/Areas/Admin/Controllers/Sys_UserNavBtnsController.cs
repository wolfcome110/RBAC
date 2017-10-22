using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF.RBAC.Controllers
{
    public class Sys_UserNavBtnsController : BaseController
    {
        private RBACEntities db = new RBACEntities();        
        
        //
        // GET: /Sys_UserNavBtns/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int? page, int? rows)
        {
            page = page == null ? 1 : page;
            rows = rows == null ? 20 : rows;

            //db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            List<Sys_UserNavBtns> rolesList = db.Sys_UserNavBtns.ToList();

            return Json(new
            {
                total = rolesList.Count(),
                rows = rolesList.ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Save(string Users, string Navigations, string Buttons)
        {
            try
            {
                List<Sys_Users> users = Common.JsonHelper.DeserializeData<List<Sys_Users>>(Users);
                List<Sys_Navigations> navigations = Common.JsonHelper.DeserializeData<List<Sys_Navigations>>(Navigations);
                List<Sys_Buttons> buttons = Common.JsonHelper.DeserializeData<List<Sys_Buttons>>(Buttons);

                foreach (var user in users)
                {
                    foreach (var nav in navigations)
                    {
                        foreach (var button in buttons)
                        {
                            if (db.Set<Sys_UserNavBtns>().Where(o => o.UserId == user.Id && o.NavId == nav.Id && o.BtnId == button.Id).Count() == 0)
                            {
                                db.Set<Sys_UserNavBtns>().Add(new Sys_UserNavBtns { UserId = user.Id, NavId = nav.Id, BtnId = button.Id, AddTime = DateTime.Now });
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
        /// 适合批量给用户分配权限
        /// </summary>
        /// <param name="Roles"></param>
        /// <param name="Navigations"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UserNavBtns(string Users, string Navigations)
        {
            try
            {
                var btns = db.Sys_Buttons;
                List<Sys_Users> users = Common.JsonHelper.DeserializeData<List<Sys_Users>>(Users);
                List<JObject> navigations = Common.JsonHelper.DeserializeData<List<JObject>>(Navigations);

                foreach (var user in users)
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
                                    int a = db.Sys_UserNavBtns.Where(o => o.UserId == user.Id && o.NavId == navId && o.BtnId == btnId).Count();
                                    if (db.Sys_UserNavBtns.Where(o => o.UserId == user.Id && o.NavId == navId && o.BtnId == btnId).Count() == 0)
                                    {
                                        db.Sys_UserNavBtns.Add(new Sys_UserNavBtns() { UserId = user.Id, NavId = navId, BtnId = btnId });
                                    }
                                }
                                else
                                {
                                    var userNavBtn = db.Sys_UserNavBtns.Where(o => o.UserId == user.Id && o.NavId == navId && o.BtnId == btnId).FirstOrDefault();
                                    if (userNavBtn != null)
                                    {
                                        db.Entry(userNavBtn).State = EntityState.Deleted;
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
