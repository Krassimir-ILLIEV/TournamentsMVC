using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TournamentsMVC.Mapping;
using TournamentsMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace TournamentsMVC.ViewModels.Models
{
    public class PlayerViewModel: IMapFrom<Player>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }

        public string Picture { get; set; }

        public int? TeamId { get; set; }

    }
}