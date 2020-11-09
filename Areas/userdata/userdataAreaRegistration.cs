using System.Web.Mvc;

namespace DTRS.Areas.userdata
{
    public class userdataAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "userdata";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "userdata_default",
                "userdata/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}