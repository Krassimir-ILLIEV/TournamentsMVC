using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentsMVC.Services.Contracts
{
    public interface IRatingService
    {
        double GetRating(int playerId);

        void RatePlayer(int playerId, int currentRating);
    }
}
