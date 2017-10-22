using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EF.Common;
using System.Web.Security;

namespace EF.RBAC.Models
{
    public class Users
    {
        private static RBACEntities db = new RBACEntities();

        /// <summary>
        /// 获取登录用户的操作权限
        /// </summary>
        /// <param name="User">当前登录的用户</param>
        /// <param name="NavId">导航Id</param>
        /// <returns></returns>
        public static string UserAuthority(Model.Sys_Users User, int NavId = 0)
        {
            Sys_Navigations Nav = db.Set<Sys_Navigations>().Where(o => o.Id == NavId).FirstOrDefault();

            IQueryable<string> buttons = null;

            if (Nav == null)
            {
                goto result;
            }

            if (User.UserName.ToLower().Trim() == "admin")
            {
                buttons = from btn in db.Sys_Buttons
                          from nav in db.Sys_NavButtons
                          where btn.Id == nav.BtnId && nav.NavId == Nav.Id
                          orderby btn.SortNum
                          select btn.ButtonHtml;
                goto result;
            }

            var UserNavBtns = from btn in db.Sys_Buttons
                              from btn2 in db.Sys_UserNavBtns
                              where btn.Id == btn2.BtnId && btn2.NavId == Nav.Id && btn2.UserId == User.Id
                              orderby btn.SortNum
                              select btn.ButtonHtml;

            var GroupNavBtns = from btn in db.Sys_Buttons
                               from btn2 in db.Sys_GroupNavBtns
                               from grp in db.Sys_Users_Groups
                               where btn.Id == btn2.BtnId && btn2.NavId == Nav.Id && grp.UserId == User.Id && grp.GroupId == btn2.GroupId
                               orderby btn.SortNum
                               select btn.ButtonHtml;

            var RoleNavBtns = from btn in db.Sys_Buttons
                              from btn2 in db.Sys_RoleNavBtns
                              from role in db.Sys_Users_Roles
                              where btn.Id == btn2.BtnId && btn2.NavId == Nav.Id && role.Id == User.Id && role.RoleId == btn2.RoleId
                              orderby btn.SortNum
                              select btn.ButtonHtml;

            buttons = UserNavBtns.Union(GroupNavBtns).Union(RoleNavBtns);

        result:
            if (buttons != null)
            {
                return string.Join("<div class=\"datagrid-btn-separator\"></div>", buttons.ToArray());
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// 获取登录用户的操作权限
        /// </summary>
        /// <returns></returns>
        public static string UserDesktop()
        {
            string cookie = CookieHelper.GetCookie(FormsAuthentication.FormsCookieName);

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie);
            Sys_Users User = JsonHelper.DeserializeData<Sys_Users>(ticket.UserData);
            //var Nav = db.Set<Sys_Navigations>();

            IQueryable<Sys_Navigations> Navigations = null;

            if (User.UserName.ToLower().Trim() == "admin")
            {
                Navigations = from nav in db.Sys_Navigations
                              select nav;
                goto result;
            }

            var UserNav = from btn in db.Sys_Buttons
                          from btn2 in db.Sys_UserNavBtns
                          from nav in db.Sys_Navigations
                          where btn.Id == btn2.BtnId && btn2.UserId == User.Id && btn2.NavId == nav.Id
                          orderby btn.SortNum
                          select nav;

            var GroupNav = from btn in db.Sys_Buttons
                           from btn2 in db.Sys_GroupNavBtns
                           from grp in db.Sys_Users_Groups
                           from nav in db.Sys_Navigations
                           where btn.Id == btn2.BtnId && grp.UserId == User.Id && grp.GroupId == btn2.GroupId && btn2.NavId == nav.Id
                           orderby btn.SortNum
                           select nav;

            var RoleNav = from btn in db.Sys_Buttons
                          from btn2 in db.Sys_RoleNavBtns
                          from role in db.Sys_Users_Roles
                          from nav in db.Sys_Navigations
                          where btn.Id == btn2.BtnId && role.Id == User.Id && role.RoleId == btn2.RoleId && nav.Id == btn2.NavId
                          orderby btn.SortNum
                          select nav;

            Navigations = UserNav.Union(GroupNav).Union(RoleNav); ;

        result:
            if (Navigations != null)
            {
                var obj = from nav in Navigations
                          select new { id = nav.Id, text = nav.NavTitle, href = nav.LinkUrl + "?NavId=" + nav.Id, icon = nav.BigImageUrl };
                return EF.Common.JsonHelper.SerializeData(obj);
            }
            else
            {
                return null;
            }
        }
    }
}
