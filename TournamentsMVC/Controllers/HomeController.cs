using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using TournamentsMVC.Data.Contracts;
using TournamentsMVC.ViewModels.Models;
using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Mapping;
using TournamentsMVC.Services;

namespace TournamentsMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly IMapperAdapter mapper;

        public HomeController(IPlayerService playerService, IMapperAdapter mapper)
        {
            if(playerService == null)
            {
                throw new ArgumentNullException("playerService");
            }

            if(mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }

            this.playerService = playerService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            // TODO: extract constant
            var players = this.playerService.GetHighestRatedPlayers(10).ToList();
            var mappedPlayers = this.mapper.Map<IEnumerable<PlayerViewModel>>(players);
            return View(mappedPlayers);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}