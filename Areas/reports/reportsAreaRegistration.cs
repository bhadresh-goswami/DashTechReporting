using System.Web.Mvc;

namespace DTRS.Areas.reports
{
    public class reportsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "reports";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "reports_default",
                "reports/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}