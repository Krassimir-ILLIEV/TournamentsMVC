using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TournamentsMVC.Models;
using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Mapping;
using TournamentsMVC.Controllers;
using TestStack.FluentMVCTesting;
using TournamentsMVC.ViewModels.Models;

namespace TournamentsMVCTests.UnitTests.Controllers.PlayerControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void CallPlayerServiceGetById()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedRatingService = new Mock<IRatingService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedPlayerService.Setup(x => x.GetById(It.IsAny<int>())).Verifiable();

            var controller = new PlayerController(mockedPlayerService.Object, mockedRatingService.Object, mockedMapper.Object);

            // Act
            controller.Index(5);

            // Assert
            mockedPlayerService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestCase(36)]
        [TestCase(42)]
        public void CallPlayerServiceGetById_WithCorrectId(int id)
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedRatingService = new Mock<IRatingService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedPlayerService.Setup(x => x.GetById(It.IsAny<int>())).Verifiable();

            var controller = new PlayerController(mockedPlayerService.Object, mockedRatingService.Object, mockedMapper.Object);

            // Act
            controller.Index(id);

            // Assert
            mockedPlayerService.Verify(x => x.GetById(id), Times.Once);
        }

        [Test]
        public void ReturnErrorView_WhenPlayerNotFound()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedRatingService = new Mock<IRatingService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedPlayerService.Setup(x => x.GetById(It.IsAny<int>())).Returns((Player)null);

            var controller = new PlayerController(mockedPlayerService.Object, mockedRatingService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index(5)).ShouldRenderView("Error");
        }

        [Test]
        public void MapPlayerToViewModel()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedRatingService = new Mock<IRatingService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedPlayer = new Mock<Player>();
            mockedPlayerService.Setup(x => x.GetById(It.IsAny<int>())).Returns(mockedPlayer.Object);
            mockedMapper.Setup(x => x.Map<PlayerDetailsViewModel>(It.IsAny<Player>())).Verifiable();

            var controller = new PlayerController(mockedPlayerService.Object, mockedRatingService.Object, mockedMapper.Object);

            // Act
            controller.Index(5);

            // Assert
            mockedMapper.Verify(x => x.Map<PlayerDetailsViewModel>(mockedPlayer.Object), Times.Once);
        }

        [Test]
        public void ReturnCorrectView_WhenPlayerIsNotNull()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedRatingService = new Mock<IRatingService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            mockedPlayerService.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Mock<Player>().Object);

            var controller = new PlayerController(mockedPlayerService.Object, mockedRatingService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index(5)).ShouldRenderDefaultView();
        }

        [Test]
        public void ReturnView_WithCorrectModel()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedRatingService = new Mock<IRatingService>();
            var mockedMapper = new Mock<IMapperAdapter>();
            var mockedPlayer = new Mock<Player>();
            var mockedPlayerDetailsViewModel = new Mock<PlayerDetailsViewModel>();
            mockedPlayerService.Setup(x => x.GetById(It.IsAny<int>())).Returns(mockedPlayer.Object);
            mockedMapper.Setup(x => x.Map<PlayerDetailsViewModel>(It.IsAny<Player>())).Returns(mockedPlayerDetailsViewModel.Object);

            var controller = new PlayerController(mockedPlayerService.Object, mockedRatingService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.Index(7)).ShouldRenderDefaultView()
                .WithModel(mockedPlayerDetailsViewModel.Object);
        }
    }
}
