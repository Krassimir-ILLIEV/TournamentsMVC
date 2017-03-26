using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentsMVC.Data;
using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Data.Contracts;
using TournamentsMVC.Models;
using System.Data.Entity;
using TournamentsMVC.Services;

namespace TournamentsMVC.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ITournamentSystemData data;

        public PlayerService(ITournamentSystemData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            this.data = data;
        }

        public void AddPlayer(Player player)
        {
            this.data.Players.Add(player);
            this.data.SaveChanges();
        }

        public IEnumerable<Player> GetHighestRatedPlayers(int count)
        {
            var players = this.data.Players.All
                .OrderByDescending(x => x.Rating)
                .Take(count)
                .ToList();

            return players;
        }

        public Player GetById(int id)
        {
            var player = this.data.Players.All
                .Where(x => x.Id == id)
                .Include(x => x.Team)
                .FirstOrDefault();

            return player;
        }

        public double GetPlayerRating(int id)
        {      
            var player = this.data.Players.All
                .Where(x => x.Id == id)
                //.Include(x => x.Rating) // TODO check
                .FirstOrDefault();

            if (player != null)
            {
                return (double) player.Rating;
            }
            else
            {
                return 0;
            }
        }

        public IEnumerable<Player> SearchPlayers(string searchWord, IEnumerable<int> teamIds, string orderProperty, int page = 1, int playersPerPage = 9)
        {
            var skip = (page - 1) * playersPerPage;

            var players = this.BuildFilterQuery(searchWord, teamIds);

            orderProperty = orderProperty == null ? string.Empty : orderProperty;

            switch (orderProperty)
            {
                case "FirstName": players = players.OrderBy(x => x.FirstName); break;
                case "TeamName": players = players.OrderBy(x => x.Team.Name); break;
                case "LastName":
                default: players = players.OrderBy(x => x.LastName); break;
            }

            var resultPlayers = players
                .Skip(skip)
                .Take(playersPerPage)
                .ToList();

            return resultPlayers;
        }

        public int GetPlayersCount(string searchWord, IEnumerable<int> teamIds)
        {
            var players = this.BuildFilterQuery(searchWord, teamIds);
            return players.Count();
        }

        private IQueryable<Player> BuildFilterQuery(string searchWord, IEnumerable<int> teamIds)
        {
            var players = this.data.Players.All;

            if (searchWord != null)
            {
                players = players.Where(x => x.FirstName.Contains(searchWord) || x.LastName.Contains(searchWord));
            }

            if (teamIds != null && teamIds.Any())
            {
                players = players.Where(x => teamIds.Contains((int)x.TeamId));
            }

            return players;
        }
    }
}
