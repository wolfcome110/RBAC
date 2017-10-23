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
using EF.Common;
using System.Web.Security;
using EF.RBAC.Models;

namespace EF.RBAC.Controllers
{
    public class Sys_NavButtonsController : BaseController
    {
        private RBACEntities db = new RBACEntities();

        // GET: /Sys_NavButtons/
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
        public JsonResult NavButtons(int NavId = 0)
        {
            try
            {
                var sys_navbuttons = db.Set<Sys_NavButtons>().Include(o => o.Sys_Buttons).Include(o => o.Sys_Navigations).Where(o => o.NavId == NavId);
                var btns = from navbutton in sys_navbuttons select new { Id = navbutton.Sys_Buttons.Id, ButtonText = navbutton.Sys_Buttons.ButtonText, IconCls = navbutton.Sys_Buttons.IconCls };

                return Json(new { total = btns.Count(), rows = btns.ToArray() }, "text/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = "保存失败！" }, "text/json", JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 批量保存导航包含的操作铵钮
        /// </summary>
        /// <param name="Navigations">Sys_Navigations类的数组格式</param>
        /// <param name="Buttons">Sys_Buttons类的数组格式</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Save(string Navigations, string Buttons)
        {
            try
            {

                List<Sys_Navigations> navigations = Common.JsonHelper.DeserializeData<List<Sys_Navigations>>(Navigations);
                List<Sys_Buttons> buttons = Common.JsonHelper.DeserializeData<List<Sys_Buttons>>(Buttons);

                foreach (var nav in navigations)
                {
                    var NavButtons = db.Set<Sys_NavButtons>().Where(o => o.NavId == nav.Id);
                    db.Set<Sys_NavButtons>().RemoveRange(NavButtons);
                    db.SaveChanges();

                    foreach (var button in buttons)
                    {
                        if (db.Set<Sys_NavButtons>().Where(o => o.NavId == nav.Id && o.BtnId == button.Id).Count() == 0)
                        {
                            db.Set<Sys_NavButtons>().Add(new Sys_NavButtons { NavId = nav.Id, BtnId = button.Id, SortNum = 0, AddTime = DateTime.Now });
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
        /// 查询角色具备权限
        /// </summary>
        /// <param name="RoldId"></param>
        /// <returns></returns>
        [HttpPost]
        public string NavBtns(int RoldId)
        {
            var navigations = db.Sys_RoleNavBtns.Include(o => o.Sys_Buttons).Where(o => o.RoleId == RoldId).ToList();
            var roleNavBtns = GetRoleNavBtns(navigations, 0);
            return JsonHelper.SerializeData(roleNavBtns);

        }

        /// <summary>
        /// 获取角色在导航栏目中具备按钮的操作权限
        /// </summary>
        /// <param name="RoleNavBtns"></param>
        /// <param name="ParentId">导航编号</param>
        /// <returns></returns>
        private JArray GetRoleNavBtns(List<Model.Sys_RoleNavBtns> RoleNavBtns, int ParentId)
        {
            JArray navBtns = new JArray();

            var navigations = db.Sys_Navigations.Where(o => o.ParentId == ParentId);

            foreach (var nav in navigations)
            {
                var buttons = db.Sys_NavButtons.Include(o => o.Sys_Buttons).Where(o => o.NavId == nav.Id).Select(o => o.Sys_Buttons.ButtonTag);
                var obj = new JObject();
                obj.Add(new JProperty("Id", nav.Id));
                obj.Add(new JProperty("NavTitle", nav.NavTitle));
                obj.Add(new JProperty("iconCls", nav.IconCls));
                obj.Add(new JProperty("ParentId", nav.ParentId));
                obj.Add(new JProperty("Buttons", buttons));
                obj.Add(new JProperty("children", GetRoleNavBtns(RoleNavBtns, nav.Id)));
                var btns = db.Sys_Buttons;
                var buttonTags = from btn in RoleNavBtns
                                 where btn.NavId==nav.Id
                                 select btn.Sys_Buttons.ButtonTag;

                foreach (var btn in btns)
                {
                    obj.Add(new JProperty(btn.ButtonTag, buttonTags.Where(o => o == btn.ButtonTag).Count() > 0 ? "√" : "x"));
                }
                navBtns.Add(obj);
            }
            return navBtns;
        }


        [HttpPost]
        public string GetUserNavBtns(int UserId)
        {
            var navigations = db.Sys_UserNavBtns.Include(o => o.Sys_Buttons).Where(o => o.UserId == UserId).ToList();
            var userNavBtns = GetUserNavBtns(navigations, 0);
            return JsonHelper.SerializeData(userNavBtns);

        }
        /// <summary>
        /// 获取职员在导航栏目中具备按钮的操作权限
        /// </summary>
        /// <param name="RoleNavBtns"></param>
        /// <param name="ParentId">导航编号</param>
        /// <returns></returns>
        private JArray GetUserNavBtns(List<Model.Sys_UserNavBtns> UserNavBtns, int ParentId)
        {
            JArray navBtns = new JArray();

            var navigations = db.Sys_Navigations.Where(o => o.ParentId == ParentId);

            foreach (var nav in navigations)
            {
                var buttons = db.Sys_NavButtons.Include(o => o.Sys_Buttons).Where(o => o.NavId == nav.Id).OrderBy(o=>o.Sys_Buttons.SortNum).Select(o => o.Sys_Buttons.ButtonTag);
                var obj = new JObject();
                obj.Add(new JProperty("Id", nav.Id));
                obj.Add(new JProperty("NavTitle", nav.NavTitle));
                obj.Add(new JProperty("iconCls", nav.IconCls));
                obj.Add(new JProperty("ParentId", nav.ParentId));
                obj.Add(new JProperty("Buttons", buttons));
                obj.Add(new JProperty("children", GetUserNavBtns(UserNavBtns, nav.Id)));
                var btns = db.Sys_Buttons;
                var buttonTags = from btn in UserNavBtns
                                 where btn.NavId == nav.Id
                                 select btn.Sys_Buttons.ButtonTag;

                foreach (var btn in btns)
                {
                    obj.Add(new JProperty(btn.ButtonTag, buttonTags.Where(o => o == btn.ButtonTag).Count() > 0 ? "√" : "x"));
                }
                navBtns.Add(obj);
            }
            return navBtns;
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
