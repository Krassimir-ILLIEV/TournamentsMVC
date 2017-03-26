using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentsMVC.Data.Contracts;
using TournamentsMVC.Models;

namespace TournamentsMVC.Data
{
    public class TournamentSystemData : ITournamentSystemData
    {
        private readonly ITournamentSystemDbContext dbContext;

        public TournamentSystemData(
            TournamentSystemDbContext dbContext,
            IEfRepository<User> usersRepo,
            IEfRepository<Player> playersRepo,
            IEfRepository<Team> teamsRepo,
            IEfRepository<Tournament> tournamentsRepo,
            IEfRepository<Game> gamesRepo,
            IEfRepository<Sponsor> sponsorsRepo
            )
        {
            // TODO: Guard or otherwise deal with pesky strings

            if (dbContext == null)
            {
                throw new ArgumentNullException("Database context cannot be null.");
            }

            if (usersRepo == null)
            {
                throw new ArgumentNullException("Users entity framework repository cannot be null.");
            }

            if (playersRepo == null)
            {
                throw new ArgumentNullException("Players entity framework repository cannot be null.");
            }

            if (teamsRepo == null)
            {
                throw new ArgumentNullException("Teams entity framework repository cannot be null.");
            }

            if (tournamentsRepo == null)
            {
                throw new ArgumentNullException("Tournaments entity framework repository cannot be null.");
            }

            if (gamesRepo == null)
            {
                throw new ArgumentNullException("Games entity framework repository cannot be null.");
            }

            if (sponsorsRepo == null)
            {
                throw new ArgumentNullException("Sponsors entity framework repository cannot be null.");
            }

            this.dbContext = dbContext;
            this.Users = usersRepo;
            this.Players = playersRepo;
            this.Teams = teamsRepo;
            this.Tournaments = tournamentsRepo;
            this.Games = gamesRepo;
            this.Sponsors = sponsorsRepo;
        }

        // TODO: readonly fields having only getter

        public IEfRepository<User> Users { get; private set; }

        public IEfRepository<Player> Players { get; private set; }

        public IEfRepository<Team> Teams { get; private set; }

        public IEfRepository<Tournament> Tournaments { get; private set; }

        public IEfRepository<Game> Games { get; private set; }

        public IEfRepository<Sponsor> Sponsors { get; private set; }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
