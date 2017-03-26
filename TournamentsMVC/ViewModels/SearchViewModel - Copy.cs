using TournamentsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TournamentsMVC.ViewModels.Models
{
    public class SearchViewModel
    {
        public IEnumerable<GenreViewModel> Genres { get; set; }
        
        public IEnumerable<BookViewModel> Books { get; set; }
    }
}