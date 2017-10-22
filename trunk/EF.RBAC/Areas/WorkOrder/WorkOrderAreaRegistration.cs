using System.Web.Mvc;

namespace EF.RBAC.Areas.WorkOrder
{
    public class WorkOrderAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WorkOrder";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WorkOrder_default",
                "WorkOrder/{action}/{id}",
                new {controller= "WorkOrder", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}