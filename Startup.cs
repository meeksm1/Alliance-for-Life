using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Alliance_for_Life.Startup))]
namespace Alliance_for_Life
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
