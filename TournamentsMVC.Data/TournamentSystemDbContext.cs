using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentsMVC.Data.Contracts;
using TournamentsMVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;


namespace TournamentsMVC.Data
{
    public class TournamentSystemDbContext : IdentityDbContext<User>, ITournamentSystemDbContext
    {
        public TournamentSystemDbContext()
            : base("DefaultConnection")
        {
        }

        public static TournamentSystemDbContext Create()
        {
            return new TournamentSystemDbContext();
        }

        IDbSet<T> ITournamentSystemDbContext.Set<T>()
        {
            return this.Set<T>();
        }

        public IDbSet<Team> Teams { get; set; }

        public IDbSet<Tournament> Tournaments { get; set; }

        public IDbSet<Player> Players { get; set; }

        public IDbSet<Game> Games { get; set; }

        public IDbSet<Sponsor> Sponsors { get; set; }

        //public object DbSet { get; set; }

        //public IDbSet<SponsorsTournaments> SponsorsTournamentsTable;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasMany(x => x.WantToRead).WithMany(x => x.UsersWhoWantToRead);
            //modelBuilder.Entity<User>().HasMany(x => x.CurrentlyReading).WithMany(x => x.UsersCurrentlyReading);
            //modelBuilder.Entity<User>().HasMany(x => x.Read).WithMany(x => x.UsersRead);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

    }
}
