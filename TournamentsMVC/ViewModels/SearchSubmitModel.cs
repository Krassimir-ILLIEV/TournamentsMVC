using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentsMVC.ViewModels.Models
{
    public class SearchSubmitModel
    {
        public string SearchWord { get; set; }

        public IEnumerable<int> ChosenTeamIds { get; set; }

        public string SortBy { get; set; }
    }
}