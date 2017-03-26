using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentsMVC.Models;

namespace TournamentsMVC.Data.Contracts
{
    public interface ITournamentSystemDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Player> Players { get; set; }

        IDbSet<Team> Teams { get; set; }

        IDbSet<Tournament> Tournaments { get; set; }

        IDbSet<Game> Games { get; set; }

        IDbSet<Sponsor> Sponsors { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        int SaveChanges();
    }
}
