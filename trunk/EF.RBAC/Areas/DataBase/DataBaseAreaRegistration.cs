using System.Web.Mvc;

namespace EF.RBAC.Areas.DataBase
{
    public class DataBaseAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DataBase";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DataBase_default",
                "database/{action}/{id}",
                new { controller= "DataBase", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}