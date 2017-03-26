using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentsMVC.Data.Contracts;
using TournamentsMVC.Services.Contracts;

namespace TournamentsMVC.Services
{
    public class UserService : IUserService
    {
        private readonly ITournamentSystemData data;

        public UserService(ITournamentSystemData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            this.data = data;
        }

        public bool CheckIfUserExists(string username)
        {
            var exists = this.data.Users.All.Any(x => x.UserName == username);
            return exists;
        }
    }
}
