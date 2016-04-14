using System.Web.Mvc;

namespace Web.Areas.MemberArea
{
    public class MemberAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MemberArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "MemberArea_default",
                url: "MemberArea/{controller}/{action}/{id}",
                defaults: new
                {
                    area = "MemberArea",
                    controller = "Account",
                    action = "Login",
                    id = UrlParameter.Optional
                }
            );
        }

    }
}