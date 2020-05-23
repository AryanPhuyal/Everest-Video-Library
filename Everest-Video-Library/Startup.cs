using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Everest_Video_Library.Startup))]
namespace Everest_Video_Library
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
