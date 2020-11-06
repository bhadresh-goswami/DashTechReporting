using System.Web.Mvc;

namespace DTRS.Areas.leadgenerate
{
    public class leadgenerateAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "leadgenerate";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "leadgenerate_default",
                "leadgenerate/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}