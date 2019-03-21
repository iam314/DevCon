using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevConfSkopje.Web.Startup))]
namespace DevConfSkopje.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
