using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TournamentsMVC.Data.Contracts;
using TournamentsMVC.Models;
using TournamentsMVC.Services;

namespace TournamentsMVC.ServicesTests.UnitTests.PlayerServiceTests
{
    [TestFixture]
    public class GetPlayerById_Should
    {
        [TestCase(2)]
        [TestCase(78)]
        public void ReturnCorrectPlayer(int id)
        {
            // Arrange
            var mockedData = new Mock<ITournamentSystemData>();
            var expectedPlayer = new Player();
            expectedPlayer.Id = id;
            var players = new List<Player>
            {
                 new Mock<Player>().Object,
                 new Mock<Player>().Object,
                 expectedPlayer,
                 new Mock<Player>().Object,
                 new Mock<Player>().Object,
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(players);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var player = playerService.GetById(id);

            // Assert
            Assert.AreEqual(expectedPlayer, player);
        }

        [TestCase(-2)]
        [TestCase(78)]
        public void ReturnNull_WhenPlayerNotFound(int id)
        {
            // Arrange
            var mockedData = new Mock<ITournamentSystemData>();
            var players = new List<Player>
            {
                 new Mock<Player>().Object,
                 new Mock<Player>().Object,
                 new Mock<Player>().Object,
                 new Mock<Player>().Object
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(players);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var player = playerService.GetById(id);

            // Assert
            Assert.IsNull(player);
        }
    }
}
