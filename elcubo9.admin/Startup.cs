using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(elcubo9.admin.Startup))]
namespace elcubo9.admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
