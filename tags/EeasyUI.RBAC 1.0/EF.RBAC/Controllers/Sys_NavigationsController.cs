using EF.Common;
using EF.RBAC.Models;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EF.RBAC.Controllers
{
    public class Sys_NavigationsController : BaseController
    {
        private RBACEntities db = new RBACEntities();
        //
        // GET: /Sys_Navigations/
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
            db.Configuration.ProxyCreationEnabled = false;
            List<Sys_Navigations> navList = db.Sys_Navigations.OrderBy(o => o.SortNum).ToList();
            var list = MvcHelper.TreeGrid<Sys_Navigations>(0, navList);
            return Json(new
            {
                total = navList.Count(),
                rows = list.ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<Sys_Navigations> NavList(int DepID, List<Sys_Navigations> Navigations)
        {
            var navlist = Navigations.Where(o => o.ParentId == DepID);

            foreach (var model in navlist)
            {
                model.children = Navigations.Where(o => o.ParentId == model.Id) as IEnumerable<Sys_Navigations>;
                NavList(model.Id, Navigations);
            }
            return navlist as IEnumerable<Sys_Navigations>;
        }

        [HttpPost]
        public JsonResult BacthSave()
        {
            try
            {
                string insertedRows = Request["insertedRows"];
                string updatedRows = Request["updatedRows"];
                string deletedRows = Request["deletedRows"];
                var obj = MvcHelper.BacthSave<Sys_Navigations>(insertedRows, updatedRows, deletedRows);
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
                //MvcHelper.MoveNode<Sys_Navigations>(SrcId, DestId);

                var model = db.Sys_Navigations.Where(o => o.Id == SrcId).FirstOrDefault();
                model.ParentId = DestId;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, Message = "保存成功！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = "保存失败！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 生成js脚本
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        [HttpPost]
        public string JsCode(int NavId = 0)
        {
            var toolbar = db.Set<Sys_NavButtons>().Include(o => o.Sys_Buttons).Where(o => o.NavId == NavId).Select(o => o.Sys_Buttons).OrderBy(o => o.SortNum);
            StringBuilder strJs = new StringBuilder();
            StringBuilder strBtn = new StringBuilder();
            StringBuilder strHtml = new StringBuilder();
            
            strJs.AppendLine("//js");
            strJs.AppendLine("var toolbar = {");

            foreach (var btn in toolbar)
            {
                if (btn.ButtonTag.ToLower() == "help")
                {
                    strJs.AppendLine(string.Format("        {0}: function ($dg) {1}", btn.ButtonTag, "{"));
                    strJs.AppendLine(string.Format("            top.$(\"ul.app-container li:contains('帮助')\").dblclick();"));
                    strJs.AppendLine(string.Format("//            $('#dlgHelper').dialog({0}", "{"));
                    strJs.AppendLine(string.Format("//                title: '帮助',", ""));
                    strJs.AppendLine(string.Format("//                iconCls: 'icon-help',", ""));
                    strJs.AppendLine(string.Format("//                width: 800,", ""));
                    strJs.AppendLine(string.Format("//                height: 500,", ""));
                    strJs.AppendLine(string.Format("//                closed: false,", ""));
                    strJs.AppendLine(string.Format("//                cache: false,", ""));
                    strJs.AppendLine(string.Format("//                href: '/Sys_Helper/index#role',", ""));
                    strJs.AppendLine(string.Format("//                modal: true", ""));
                    strJs.AppendLine(string.Format("//            {0});", "}"));

                    strJs.AppendLine(string.Format("         {0},//{1}", "}", btn.Remark));

                }
                else
                {
                    strJs.AppendLine(string.Format("        {0}: function ($dg) {1}{2},//{3}", btn.ButtonTag, "{", "}", btn.Remark));
                }

                //<a id="btn_Help"  style="float:left;" href="javascript:void(0);" plain="true" class="easyui-linkbutton" icon="icon-lightning" title="帮助">帮助</a>
                Regex reg = new Regex(@"btn_[A-Za-z0-9_]*");
                string btnID = reg.Match(btn.ButtonHtml).Value;

                strBtn.AppendLine(string.Format("$(\"#toolbar\").find(\"#{0}\").on(\"click\", function () {1} toolbar.{2}($dg); {3});", btnID, "{", btn.ButtonTag, "}"));


            }
            strJs.AppendLine("};");
            strJs.AppendLine();

            strBtn.AppendLine();

            strHtml.AppendLine();
            strHtml.AppendLine("<!-- 工具栏 -->");
            strHtml.AppendLine("<div id=\"toolbar\" style=\"height:28px;\">");
            strHtml.AppendLine("    @Html.Raw(ViewBag.UserAuthority)");
            strHtml.AppendLine("</div> ");

           

            //strJs.AppendLine(string.Format("$(\"#toolbar\").find(\"#btn_Add\").on(\"click\", function () { toolbar.add($dg); });"));

            return strJs.ToString() + strBtn.ToString() + strHtml.ToString();
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

