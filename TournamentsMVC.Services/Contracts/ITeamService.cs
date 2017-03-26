using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentsMVC.Models;

namespace TournamentsMVC.Services.Contracts
{
    public interface ITeamService
    {
        IEnumerable<Team> GetAllTeams();
    }
}
