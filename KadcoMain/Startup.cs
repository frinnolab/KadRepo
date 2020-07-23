using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KadcoMain.Startup))]
namespace KadcoMain
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
