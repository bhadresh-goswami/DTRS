using System.Web.Mvc;

namespace DTRS.Areas.TechnicalExpert
{
    public class TechnicalExpertAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TechnicalExpert";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TechnicalExpert_default",
                "TechnicalExpert/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}