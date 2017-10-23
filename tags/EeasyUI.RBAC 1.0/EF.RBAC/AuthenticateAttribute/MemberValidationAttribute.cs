using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EF.RBAC
{
    public class MemberValidationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                var Url = new UrlHelper(filterContext.RequestContext);
                var url = Url.Action("Login", "Sys_Users");
                filterContext.Result = new RedirectResult(url);
            }

            return;
            //获取Cookies中的Login
            var memberValidation = System.Web.HttpContext.Current.Request.Cookies.Get("Login");
            //如果memberValidation为null  或者 memberValidation不等于Success
            if (memberValidation == null || memberValidation.Value != "Success")
            {
                //页面跳转到 登录页面
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Sys_Users", action = "Login" }));
                return;
            }
            //通过验证
            return;
        }

    }
}