using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentsMVC.Services.Contracts
{
    public interface IUserService
    {
        bool CheckIfUserExists(string username);
    }
}
