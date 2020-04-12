using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LocalDestination.WebMVC.Startup))]
namespace LocalDestination.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
