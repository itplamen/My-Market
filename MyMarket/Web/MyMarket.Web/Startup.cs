using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyMarket.Web.Startup))]
namespace MyMarket.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
