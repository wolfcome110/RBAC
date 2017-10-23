using EF.RBAC.Models;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF.RBAC.Controllers
{
    [AuthenticateAttribute]
    //[MemberValidationAttribute]
    public class BaseController : Controller
    {

        /// <summary>
        /// 获取职员操作权限
        /// </summary>
        /// <param name="NavId">导航Id</param>
        /// <returns></returns>
        //[HttpPost]
        //public string UserAuthority(int NavId = 0)
        //{
        //    Sys_Users user = Session["user"] as Sys_Users;
        //    //var routeData = string.Format("{0}/{1}", RouteData.Values["controller"], RouteData.Values["action"]);// ;
        //    var authority = Users.UserAuthority(user, NavId);
        //    ViewBag.UserAuthority = authority;
        //    return authority;
        //}
	}
}