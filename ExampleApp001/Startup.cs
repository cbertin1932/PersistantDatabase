using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExampleApp001.Startup))]
namespace ExampleApp001
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
