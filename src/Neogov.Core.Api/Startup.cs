using Microsoft.Owin;
using Neogov.Core.Common.Events;
using Owin;

[assembly: OwinStartup(typeof(Neogov.Core.Api.Startup))]

namespace Neogov.Core.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
