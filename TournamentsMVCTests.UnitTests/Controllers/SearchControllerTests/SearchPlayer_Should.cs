using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Mapping;
using TournamentsMVC.Controllers;
using TournamentsMVC.Models;
using TournamentsMVC.ViewModels.Models;

namespace TournamentsMVC.Tests.Controllers.SearchControllerTests
{
    [TestFixture]
    public class SearchPlayers_Should
    {
        [Test]
        public void CallPlayerServiceSearchPlayer_WithCorrectParams()
        {
            // Arrange
            var searchWord = "Mes";
            var chosenTeamIds = new List<int> { 2, 3 };
            var sortBy = "FirstName";       
            var page = 2;
            var playersPerPage = 3; // TODO magic..

            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var submitModel = new SearchSubmitModel()
            {
                SearchWord = searchWord,
                SortBy = sortBy,
                ChosenTeamIds = chosenTeamIds
            };

            mockedPlayerService.Setup(x => x.SearchPlayers(searchWord, chosenTeamIds, sortBy, page, playersPerPage)).Verifiable();

            var controller = new SearchController(mockedPlayerService.Object, mockedTeamService.Object, mockedMapper.Object);

            // Act
            controller.SearchPlayers(submitModel, page);

            // Assert
            mockedPlayerService.Verify(x => x.SearchPlayers(searchWord, chosenTeamIds, sortBy, page, playersPerPage), Times.Once);
        }
        
        [Test]
        public void CallPlayerServiceSearchPlayer_WithPage1_WhenPageIsNull()
        {
            // Arrange
            var searchWord = "Mes";
            var sortBy = "probablyInvalidProperty";  //then default sorting by player's last name kicks in
            var chosenTeamIds = new List<int> { 2, 4 };
            var playersPerPage = 3;

            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var submitModel = new SearchSubmitModel()
            {
                SearchWord = searchWord,
                SortBy = sortBy,
                ChosenTeamIds = chosenTeamIds
            };

            mockedPlayerService.Setup(x => x.SearchPlayers(searchWord, chosenTeamIds, sortBy, 1, playersPerPage)).Verifiable();

            var controller = new SearchController(mockedPlayerService.Object, mockedTeamService.Object, mockedMapper.Object);

            // Act
            controller.SearchPlayers(submitModel, null);

            // Assert
            mockedPlayerService.Verify(x => x.SearchPlayers(searchWord, chosenTeamIds, sortBy, 1, playersPerPage), Times.Once);
        }
        
        [Test]
        public void CallPlayerService_GetPlayersCount_WithCorrectParams()
        {
            // Arrange
            var searchWord = "Messi";
            var chosenTeamIds = new List<int> { 3, 4 };

            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var submitModel = new SearchSubmitModel()
            {
                SearchWord = searchWord,
                ChosenTeamIds = chosenTeamIds
            };

            mockedPlayerService.Setup(x => x.GetPlayersCount(searchWord, chosenTeamIds)).Verifiable();

            var controller = new SearchController(mockedPlayerService.Object, mockedTeamService.Object, mockedMapper.Object);

            // Act
            controller.SearchPlayers(submitModel, null);

            // Assert
            mockedPlayerService.Verify(x => x.GetPlayersCount(searchWord, chosenTeamIds), Times.Once);
        }
        
        [Test]
        public void CallMapper()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var players = new List<Player>();
            mockedPlayerService.Setup(x => x.SearchPlayers(It.IsAny<string>(), It.IsAny<IEnumerable<int>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(players);
            mockedMapper.Setup(x => x.Map<IEnumerable<PlayerViewModel>>(It.IsAny<IEnumerable<Player>>())).Verifiable();

            var controller = new SearchController(mockedPlayerService.Object, mockedTeamService.Object, mockedMapper.Object);

            // Act
            controller.SearchPlayers(new SearchSubmitModel(), null);

            // Assert
            mockedMapper.Verify(x => x.Map<IEnumerable<PlayerViewModel>>(players), Times.Once);
        }
        
        [Test]
        public void ReturnCorrectPartialView()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var controller = new SearchController(mockedPlayerService.Object, mockedTeamService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.SearchPlayers(new SearchSubmitModel(), null))
                .ShouldRenderPartialView("_ResultsPartial");
        }
        
        [Test]
        public void ReturnViewModel_WithCorrectCount()
        {
            // Arrange
            var count = 666;
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            mockedPlayerService.Setup(x => x.GetPlayersCount(It.IsAny<string>(), It.IsAny<IEnumerable<int>>())).Returns(count);

            var controller = new SearchController(mockedPlayerService.Object, mockedTeamService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.SearchPlayers(new SearchSubmitModel(), null))
                .ShouldRenderPartialView("_ResultsPartial")
                .WithModel<SearchResultsViewModel>(x => x.PlayersCount == count);
        }
        
        [Test]
        public void ReturnViewModel_WithCorrectSubmitModel()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var submitModel = new SearchSubmitModel();

            var controller = new SearchController(mockedPlayerService.Object, mockedTeamService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.SearchPlayers(submitModel, null))
                .ShouldRenderPartialView("_ResultsPartial")
                .WithModel<SearchResultsViewModel>(x => x.SubmitModel == submitModel);
        }
        
        [TestCase(9, 3)]  //check magic values
        [TestCase(16, 6)]
        public void ReturnViewModel_WithCorrectSubmitModel(int count, int pages)
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            mockedPlayerService.Setup(x => x.GetPlayersCount(It.IsAny<string>(), It.IsAny<IEnumerable<int>>()))
                .Returns(count);

            var controller = new SearchController(mockedPlayerService.Object, mockedTeamService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.SearchPlayers(new SearchSubmitModel(), null))
                .ShouldRenderPartialView("_ResultsPartial")
                .WithModel<SearchResultsViewModel>(x => x.Pages == pages);
        }
        
        [Test]
        public void ReturnViewModel_WithCorrectPlayers()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mappedPlayers = new List<PlayerViewModel>();
            mockedMapper.Setup(x => x.Map<IEnumerable<PlayerViewModel>>(It.IsAny<IEnumerable<Player>>()))
                .Returns(mappedPlayers);

            var controller = new SearchController(mockedPlayerService.Object, mockedTeamService.Object, mockedMapper.Object);
            
            // Act & Assert
            controller.WithCallTo(c => c.SearchPlayers(new SearchSubmitModel(), null))
                .ShouldRenderPartialView("_ResultsPartial")
                .WithModel<SearchResultsViewModel>(x => x.Players == mappedPlayers);
        }
    }
}
