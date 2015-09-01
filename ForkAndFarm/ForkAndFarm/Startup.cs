using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ForkAndFarm.Startup))]
namespace ForkAndFarm
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
