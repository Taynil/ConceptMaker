using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConceptMaker.Startup))]
namespace ConceptMaker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
