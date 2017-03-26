using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Mapping;
using TournamentsMVC.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TournamentsMVC.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly ITeamService teamService;
        private readonly IMapperAdapter mapper;

        public SearchController(IPlayerService playerService, ITeamService teamService, IMapperAdapter mapper)
        {
            if (playerService == null)
            {
                throw new ArgumentNullException("playerService");
            }

            if (teamService == null)
            {
                throw new ArgumentNullException("teamService");
            }

            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }

            this.playerService = playerService;
            this.teamService = teamService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var model = new SearchViewModel();
            var teams = this.teamService.GetAllTeams();
            model.Teams = this.mapper.Map<IEnumerable<TeamViewModel>>(teams);

            return View(model);
        }
        
        public PartialViewResult SearchPlayers(SearchSubmitModel submitModel, int? page)
        {            
            int actualPage = page ?? 1;
            int playersPerPage = 3;

            var result = this.playerService.SearchPlayers(submitModel.SearchWord, submitModel.ChosenTeamIds, submitModel.SortBy, actualPage, playersPerPage);
            var count = this.playerService.GetPlayersCount(submitModel.SearchWord, submitModel.ChosenTeamIds);

            var resultViewModel = new SearchResultsViewModel();
            resultViewModel.PlayersCount = count;
            resultViewModel.SubmitModel = submitModel;
            resultViewModel.Pages = (int)Math.Ceiling((double)count / playersPerPage);

            resultViewModel.Players = mapper.Map<IEnumerable<PlayerViewModel>>(result);

            return this.PartialView("_ResultsPartial", resultViewModel);
        }
    }
}