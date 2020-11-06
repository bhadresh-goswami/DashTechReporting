using System.Web.Mvc;

namespace DTRS.Areas.sales
{
    public class salesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "sales";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "sales_default",
                "sales/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}