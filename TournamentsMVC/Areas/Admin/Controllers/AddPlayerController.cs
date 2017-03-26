﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TournamentsMVC.Models;
using AutoMapper;
using TournamentsMVC.Data.Contracts;
using TournamentsMVC.Services.Contracts;
using System.IO;
using TournamentsMVC.Areas.Admin.Models;

namespace TournamentsMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AddPlayerController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly ITeamService teamService;

        public AddPlayerController(IPlayerService playerService, ITeamService teamService)
        {
            if (playerService == null)
            {
                throw new ArgumentNullException("playerService");
            }

            if (teamService == null)
            {
                throw new ArgumentNullException("teamService");
            }

            this.playerService = playerService;
            this.teamService = teamService;
        }

        public ActionResult Index()
        {
            var model = new AddPlayerViewModel();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Exclude = "Teams")]AddPlayerViewModel playerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(playerModel);
            }

            if (!this.IsImageFile(playerModel.PhotoFile))
            {
                this.ModelState.AddModelError("Photofile", "Player photo photo should be an image file.");
                return View(playerModel);
            }

            var player = this.GetPlayer(playerModel);
            this.playerService.AddPlayer(player);

            return this.Redirect("/home");
        }

        private Player GetPlayer(AddPlayerViewModel playerModel)
        {
            var filename = playerModel.PhotoFile.FileName;
            var path = this.Server.MapPath($"~/Content/Images/{filename}");
            playerModel.PhotoFile.SaveAs(path);

            // TODO map?
            var player = new Player()
            {
                FirstName = playerModel.FirstName,
                LastName = playerModel.LastName,
                NickName = playerModel.NickName,
                CV = playerModel.CV,
                Email = playerModel.Email,
                IsCoach = playerModel.IsCoach,
                Picture = filename,
                Rating = 0,
                Votes = 0,
                //Team = playerModel.Team,
                TeamId = playerModel.TeamId,
                User = playerModel.User,
                UserId = playerModel.UserId
            };

            return player;
        }

        private bool IsImageFile(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return false;
            }

            var contentType = file.ContentType.ToLower();
            var result = contentType == "image/jpg"
                || contentType == "image/jpeg"
                || contentType == "image/png";

            return result;
        }
    }
}