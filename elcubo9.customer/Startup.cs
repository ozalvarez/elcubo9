using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(elcubo9.customer.Startup))]
namespace elcubo9.customer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
