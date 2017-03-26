using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentsMVC.Models;

namespace TournamentsMVC.Services.Contracts
{
    public interface IRatingService
    {
        double GetRating(int playerId);

        Player RatePlayer(int playerId, int currentRating);
    }
}
