using System.Web.Mvc;

namespace WisdomLogistics.Web.Areas.MaintainManage
{
    public class MaintainManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MaintainManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MaintainManage_default",
                "MaintainManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}