using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LunchPicker.Startup))]
namespace LunchPicker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
