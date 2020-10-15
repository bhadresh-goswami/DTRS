using System.Web.Mvc;

namespace DTRS.Areas.TechnicalLead
{
    public class TechnicalLeadAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TechnicalLead";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TechnicalLead_default",
                "TechnicalLead/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}