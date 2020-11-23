using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UniversityCourseAndResultManagementApp.Startup))]
namespace UniversityCourseAndResultManagementApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
