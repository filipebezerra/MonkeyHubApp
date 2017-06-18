using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MonkeyHubApp.Backend.Startup))]

namespace MonkeyHubApp.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}