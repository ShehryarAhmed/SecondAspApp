using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(shehryar.Startup))]
namespace shehryar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
