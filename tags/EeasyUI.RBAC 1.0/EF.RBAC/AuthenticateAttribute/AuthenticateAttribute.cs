using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace EF.RBAC
{
    public class AuthenticateAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                var Url = new UrlHelper(filterContext.RequestContext);
                var url = Url.Action("Login", "Sys_Users");
                filterContext.Result = new RedirectResult(url);
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //throw new NotImplementedException();
        }
    }
}