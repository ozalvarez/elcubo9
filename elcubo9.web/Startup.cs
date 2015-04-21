using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(elcubo9.web.Startup))]
namespace elcubo9.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
