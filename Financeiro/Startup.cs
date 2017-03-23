using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Financeiro.Startup))]
namespace Financeiro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
