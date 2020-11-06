using System.Web.Mvc;

namespace DTRS.Areas.marketing
{
    public class marketingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "marketing";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "marketing_default",
                "marketing/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}