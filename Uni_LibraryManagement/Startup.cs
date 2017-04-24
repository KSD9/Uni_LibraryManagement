using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Uni_LibraryManagement.Startup))]
namespace Uni_LibraryManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
