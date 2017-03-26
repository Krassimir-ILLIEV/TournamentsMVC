using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Mapping;
using TournamentsMVC.ViewModels.Models;
using Microsoft.AspNet.Identity;

namespace TournamentsMVC.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly IRatingService ratingService;
        private readonly IMapperAdapter mapper;

        public PlayerController(IPlayerService playerService,IRatingService ratingService, IMapperAdapter mapper)
        {
            if(playerService == null)
            {
                throw new ArgumentNullException("playerService");
            }

            if (ratingService == null)
            {
                throw new ArgumentNullException("ratingService");
            }

            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }

            this.playerService = playerService;
            this.ratingService = ratingService;
            this.mapper = mapper;
        }

        public ActionResult Index(int id)
        {
            var player = this.playerService.GetById(id);

            if(player == null)
            {
                return this.View("Error");
            }
            
            var playerViewModel = this.mapper.Map<PlayerDetailsViewModel>(player);
                 
            return this.View(playerViewModel);
        }

        [Authorize]
        [ChildActionOnly]
        public PartialViewResult GetRatingPartial(int id)
        {
            // TODO: should not include team
            var ratingModel = new RatingViewModel();
            ratingModel.Id = id;

            var rating = this.playerService.GetPlayerRating(id);
            ratingModel.CalculatedRating = rating;

            return PartialView("_RatingPartial", ratingModel);
        }
        
        [Authorize]
        public JsonResult Rate(int id, int currentRating)
        {
            
            this.ratingService.RatePlayer(id, currentRating);
            var rating = this.playerService.GetPlayerRating(id);
            return Json(new { success = true, rating = rating }, JsonRequestBehavior.AllowGet);
        }
    }
}