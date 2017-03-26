using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentsMVC.Models;

namespace TournamentsMVC.Data.Contracts
{
    public interface ITournamentSystemData
    {
        IEfRepository<User> Users { get; }

        IEfRepository<Player> Players { get; }

        IEfRepository<Team> Teams { get; }

        IEfRepository<Tournament> Tournaments { get; }

        IEfRepository<Game> Games { get; }

        IEfRepository<Sponsor> Sponsors { get; }

        void SaveChanges();
    }
}
