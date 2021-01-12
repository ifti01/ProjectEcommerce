using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalProject.Web.Startup))]
namespace FinalProject.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
