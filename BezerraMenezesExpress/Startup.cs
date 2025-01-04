using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BezerraMenezesExpress.Startup))]
namespace BezerraMenezesExpress
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
               ConfigureAuth(app);
        }
    }
}
