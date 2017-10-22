using System.Web.Mvc;

namespace EF.RBAC.Areas.AdminPage
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "admin/{action}/{id}",
                new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_default2",
                "admin/{controller}/{action}/{id}",
                new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}