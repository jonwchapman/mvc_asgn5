using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mvc_race_v3.Startup))]
namespace mvc_race_v3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
