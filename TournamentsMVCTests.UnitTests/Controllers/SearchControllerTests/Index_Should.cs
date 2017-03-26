using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Mapping;
using TournamentsMVC.Controllers;
using TournamentsMVC.ViewModels.Models;
using TournamentsMVC.Models;

namespace CourseProject.Web.Tests.Controllers.SearchControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void CallTeamServiceGetAllTeams()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedTeamService.Setup(x => x.GetAllTeams()).Returns(new List<Team>());

            var controller = new SearchController(mockedPlayerService.Object, mockedTeamService.Object, mockedMapper.Object);

            // Act
            controller.Index();

            // Assert
            mockedTeamService.Verify(x => x.GetAllTeams(), Times.Once);
        }

        [Test]
        public void CallMapper()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var teams = new List<Team>();
            mockedTeamService.Setup(x => x.GetAllTeams()).Returns(teams);
            mockedMapper.Setup(x => x.Map<IEnumerable<TeamViewModel>>(teams)).Verifiable();
            var controller = new SearchController(mockedPlayerService.Object, mockedTeamService.Object, mockedMapper.Object);

            // Act
            controller.Index();

            // Assert
            mockedMapper.Verify(x => x.Map<IEnumerable<TeamViewModel>>(teams), Times.Once);
        }

        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var controller = new SearchController(mockedPlayerService.Object, mockedTeamService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }

        [Test]
        public void RanderTheViewAModelWithCorrectTeams()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var teams = new List<Team>();
            var mappedTeams = new List<TeamViewModel>();
            mockedTeamService.Setup(x => x.GetAllTeams()).Returns(teams);
            mockedMapper.Setup(x => x.Map<IEnumerable<TeamViewModel>>(teams)).Returns(mappedTeams);

            var controller = new SearchController(mockedPlayerService.Object, mockedTeamService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView()
                .WithModel<SearchViewModel>(m => m.Teams == mappedTeams);
        }
    }
}
