using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using TournamentsMVC.App_Start.NinjectModules;

[assembly: OwinStartupAttribute(typeof(TournamentsMVC.Startup))]
namespace TournamentsMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}