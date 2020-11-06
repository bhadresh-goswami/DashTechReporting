using System.Web.Mvc;

namespace DTRS.Areas.imports
{
    public class importsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "imports";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "imports_default",
                "imports/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}