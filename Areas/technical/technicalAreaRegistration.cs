using System.Web.Mvc;

namespace DTRS.Areas.technical
{
    public class technicalAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "technical";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "technical_default",
                "technical/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}