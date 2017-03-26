using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TournamentsMVC.Models;
using TournamentsMVC.Mapping;

namespace TournamentsMVC.ViewModels.Models
{
    public class PlayerDetailsViewModel : IMapFrom<Player>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }
    
        public string Picture { get; set; }

        public string Email { get; set; }

        public double? Rating { get; set; }

        public bool? IsCoach { get; set; }

        public string CV { get; set; }

        public int? TeamId { get; set; }

        public virtual Team Team { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}