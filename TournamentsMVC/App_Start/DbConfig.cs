using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TournamentsMVC.Data;


namespace TournamentsMVC.App_Start
{
    public class DbConfig
    {
        public static void Intitalize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TournamentSystemDbContext, TournamentsMVC.Data.Migrations.Configuration>());
            TournamentSystemDbContext.Create().Database.Initialize(true);
        }
    }
}