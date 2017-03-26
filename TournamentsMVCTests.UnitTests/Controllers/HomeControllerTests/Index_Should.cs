using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Mapping;
using TournamentsMVC.Controllers;
using TournamentsMVC.Models;
using TournamentsMVC.ViewModels.Models;

namespace TournamentsMVCTests.UnitTests.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void CallPlayerServiceHighestRatedPlayers()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedPlayerService.Setup(x => x.GetHighestRatedPlayers(It.IsAny<int>())).Returns(new List<Player>());

            var controller = new HomeController(mockedPlayerService.Object, mockedMapper.Object);

            // Act 
            controller.Index();

            // Assert
            mockedPlayerService.Verify(x => x.GetHighestRatedPlayers(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CallPlayerServiceHighestRatedPlayersWith_CorrectCount()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>(); //10?
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedPlayerService.Setup(x => x.GetHighestRatedPlayers(It.IsAny<int>())).Returns(new List<Player>());

            var controller = new HomeController(mockedPlayerService.Object, mockedMapper.Object);

            // Act 
            controller.Index();

            // Assert
            mockedPlayerService.Verify(x => x.GetHighestRatedPlayers(10), Times.Once);
        }
        
        [Test]
        public void CallMapperWithCorrectCollection()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var players = new List<Player>();
            mockedPlayerService.Setup(x => x.GetHighestRatedPlayers(It.IsAny<int>())).Returns(players);
            mockedMapper.Setup(x => x.Map<IEnumerable<PlayerViewModel>>(It.IsAny<IEnumerable<Player>>())).Verifiable();
            var controller = new HomeController(mockedPlayerService.Object, mockedMapper.Object);

            // Act 
            controller.Index();

            // Assert
            mockedMapper.Verify(x => x.Map<IEnumerable<PlayerViewModel>>(players), Times.Once);
        }

        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            var controller = new HomeController(mockedPlayerService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }

        [Test]
        public void ReturnViewWithCorrectModel()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var players = new List<Player>();
            var mappedPlayers = new List<PlayerViewModel>();
            mockedPlayerService.Setup(x => x.GetHighestRatedPlayers(It.IsAny<int>())).Returns(players);
            mockedMapper.Setup(x => x.Map<IEnumerable<PlayerViewModel>>(players)).Returns(mappedPlayers);
            var controller = new HomeController(mockedPlayerService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView()
                .WithModel(mappedPlayers);

        }
    }
}
