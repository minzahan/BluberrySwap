using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlueberrySwap.Startup))]
namespace BlueberrySwap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
