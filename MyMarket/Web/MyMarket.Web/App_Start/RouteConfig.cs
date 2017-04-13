namespace MyMarket.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DefaultGetByid",
                url: "{controller}/{id}",
                defaults: new { action = "Get" },
                constraints: new { id = @"\d+" },
                namespaces: new[] { "MyMarket.Web.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: new[] { "MyMarket.Web.Controllers" });
        }
    }
}
