using System.Web.Mvc;

namespace MyMarket.Web.Areas.Users
{
    public class UsersAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Users";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "UsersProfile",
                "Users/Profile/{action}",
                new { controller = "Profile", action = "Index", id = UrlParameter.Optional });
        }
    }
}