using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsIndia.Startup))]
namespace NewsIndia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
