using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tachey001.Startup))]
namespace Tachey001
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
