using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Job_Offers.Startup))]
namespace Job_Offers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
