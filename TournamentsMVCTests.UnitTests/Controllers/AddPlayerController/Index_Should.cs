using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TournamentsMVC.Controllers;
using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Data.Contracts;
using TournamentsMVC.Models;
using System.Web.Mvc;
using TournamentsMVC.ViewModels.Models;
using TournamentsMVC.Areas.Admin.Controllers;
using TournamentsMVC.Areas.Admin.Models;

namespace TournamentsMVCTests.UnitTests.Controllers.AddPlayerControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnViewResult()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            mockedTeamService.Setup(x => x.GetAllTeams()).Returns(new List<Team>());

            var controller = new AddPlayerController(mockedPlayerService.Object, mockedTeamService.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void ReturnViewResultWithCorrectModelType()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            mockedTeamService.Setup(x => x.GetAllTeams()).Returns(new List<Team>());

            var controller = new AddPlayerController(mockedPlayerService.Object, mockedTeamService.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsInstanceOf<AddPlayerViewModel>(result.Model);
        }

        [Test]
        public void ReturnViewResult_WithCorrectTeam()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            var teams = new List<Team>()
            {
                new Team() {Id = 1, Name = "Barca" },
                new Team() {Id = 2, Name = "Real Madrid" },
                new Team() {Id = 3, Name = "Liechtenstein" }
            };
            mockedTeamService.Setup(x => x.GetAllTeams()).Returns(teams);

            var controller = new AddPlayerController(mockedPlayerService.Object, mockedTeamService.Object);

            // Act
            var result = (ViewResult)controller.Index();

            // Assert
            var model = (AddPlayerViewModel)result.Model;
            var selectList = model.Teams.ToList();
            Assert.AreEqual(teams.Count, selectList.Count);
            for (int i = 0; i < teams.Count; i++)
            {
                Assert.AreEqual(teams[i].Id.ToString(), selectList[i].Value);
                Assert.AreEqual(teams[i].Name, selectList[i].Text);
            }
        }
    }
}
