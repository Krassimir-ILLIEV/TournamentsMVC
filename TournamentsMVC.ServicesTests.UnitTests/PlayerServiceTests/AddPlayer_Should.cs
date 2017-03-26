using System;
using Moq;
using NUnit.Framework;
using TournamentsMVC.Services;
using TournamentsMVC.Data.Contracts;
using TournamentsMVC.Models;

namespace TournamentsMVC.TestsServices.UnitTests.PlayerServiceTests
{
    [TestFixture]
    public class AddPlayer_Should
    {
        [Test]
        public void CallDataPlayerAddMethod()
        {
            // Arrange
            var mockedPlayer = new Mock<Player>();
            var mockedData = new Mock<ITournamentSystemData>();
            mockedData.Setup(x => x.Players.Add(It.IsAny<Player>())).Verifiable();

            var playerService = new PlayerService(mockedData.Object);

            // Act
            playerService.AddPlayer(mockedPlayer.Object);

            // Assert
            mockedData.Verify(x => x.Players.Add(It.IsAny<Player>()), Times.Once);
        }

        [Test]
        public void CallDataPlayersAddMethod_WithCorrectPlayer()
        {
            // Arrange
            var mockedPlayer = new Mock<Player>();
            var mockedData = new Mock<ITournamentSystemData>();
            mockedData.Setup(x => x.Players.Add(mockedPlayer.Object)).Verifiable();

            var playerService = new PlayerService(mockedData.Object);

            // Act
            playerService.AddPlayer(mockedPlayer.Object);

            // Assert
            mockedData.Verify(x => x.Players.Add(mockedPlayer.Object), Times.Once);
        }

        [Test]
        public void CallDataSaveChanges()
        {
            // Arrange
            var mockedPlayer = new Mock<Player>();
            var mockedData = new Mock<ITournamentSystemData>();
            mockedData.Setup(x => x.Players.Add(It.IsAny<Player>())).Verifiable();
            mockedData.Setup(x => x.SaveChanges()).Verifiable();
            var playerService = new PlayerService(mockedData.Object);

            // Act
            playerService.AddPlayer(mockedPlayer.Object);

            // Assert
            mockedData.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}

