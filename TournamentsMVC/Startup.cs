using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TournamentsMVC.Startup))]
namespace TournamentsMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
