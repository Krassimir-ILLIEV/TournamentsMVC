using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TournamentsMVC.Models;

namespace TournamentsMVC.ViewModels.Models
{
    public class SearchResultsViewModel
    {
        public IEnumerable<PlayerViewModel> Players { get; set; }

        public int PlayersCount { get; set; }

        public int Pages { get; set; }

        public SearchSubmitModel SubmitModel { get; set; }
    }
}