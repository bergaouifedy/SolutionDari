using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DariTn.Startup))]
namespace DariTn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
