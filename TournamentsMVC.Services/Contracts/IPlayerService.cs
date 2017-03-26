using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentsMVC.Models;

namespace TournamentsMVC.Services.Contracts
{
    public interface IPlayerService
    {
        void AddPlayer(Player player);

        IEnumerable<Player> GetHighestRatedPlayers(int count);

        Player GetById(int id);

        double GetPlayerRating(int id);

        IEnumerable<Player> SearchPlayers(string searchWord, IEnumerable<int> teamIds, string orderProperty, int page = 1, int numberOfPages = 9);

        int GetPlayersCount(string searchWord, IEnumerable<int> teamIds);
    }
}
