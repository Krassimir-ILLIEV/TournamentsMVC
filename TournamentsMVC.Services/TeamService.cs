using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentsMVC.Data;
using TournamentsMVC.Data.Contracts;
using TournamentsMVC.Models;
using TournamentsMVC.Services.Contracts;

namespace TournamentsMVC.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITournamentSystemData data;

        public TeamService(ITournamentSystemData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            this.data = data;
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return this.data.Teams.All.ToList();
        }
    }
}
