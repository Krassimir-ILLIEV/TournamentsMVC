using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TournamentsMVC.Data;
using TournamentsMVC.Data.Contracts;
using TournamentsMVC.Data.Repositories;
using TournamentsMVC.Services;
using TournamentsMVC.Services.Contracts;

namespace TournamentsMVC.App_Start.NinjectModules
{
    public class DataNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ITournamentSystemDbContext>().To<TournamentSystemDbContext>().InRequestScope();
            this.Bind(typeof(IEfRepository<>)).To(typeof(EfRepository<>)).InRequestScope();
            this.Bind<ITournamentSystemData>().To<TournamentSystemData>().InRequestScope();
            this.Bind<IPlayerService>().To<PlayerService>().InRequestScope();
            this.Bind<ITeamService>().To<TeamService>().InRequestScope();
            this.Bind<IRatingService>().To<RatingService>().InRequestScope();
            this.Bind<IUserService>().To<UserService>().InRequestScope();

        }
    }
}