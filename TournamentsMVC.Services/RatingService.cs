using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentsMVC.Data.Contracts;
using TournamentsMVC.Models;
using TournamentsMVC.Services.Contracts;

namespace TournamentsMVC.Services
{
    public class RatingService : IRatingService
    {
        private readonly ITournamentSystemData data;

        public RatingService(ITournamentSystemData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            this.data = data;
        }

        public double GetRating(int playerId)
        {
            var player = this.data.Players.All.FirstOrDefault(x => x.Id == playerId);
            if (player != null && player.Rating!=null)
            {                
                return (double) player.Rating;
            }
            else
            {
                return 0;
            }
        }

        public Player RatePlayer(int playerId, int currentRating)
        {
            // TODO: check rating range
            var player = this.data.Players.All.Where(x => x.Id == playerId).FirstOrDefault();            
            if (player != null)
            {
                player.Rating = (player.Rating*player.Votes+currentRating)/(player.Votes+1);
                player.Votes++;
                this.data.SaveChanges();
                return player;
            }
            else
            {
                return null;// throw new ArgumentException("No player with id {playerId} found in database");
            }            
        }
    }
}
