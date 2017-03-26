using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TournamentsMVC.Mapping;

namespace TournamentsMVC.App_Start.NinjectModules
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMapperAdapter>().To<MapperAdapter>();
        }
    }
}