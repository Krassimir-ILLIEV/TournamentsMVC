using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TournamentsMVC.Models;
using TournamentsMVC.Mapping;

namespace TournamentsMVC.ViewModels.Models
{
    public class RatingViewModel : IMapFrom<Player>
    {
        public int Id { get; set; }

       public double? CalculatedRating { get; set; }

    }
}