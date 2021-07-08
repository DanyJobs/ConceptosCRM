using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebFacturaMvc.Startup))]
namespace WebFacturaMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
