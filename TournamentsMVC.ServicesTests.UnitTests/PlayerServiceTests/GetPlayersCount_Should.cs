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
    public class GetPlayersCount_Should
    {
        [TestCase(1, "Zin")] //Zinedine Zidane
        [TestCase(2, "mi")]  // Daniel Borimirov, Dimitar Berbatov
        public void ReturnCorrectCount_WhenFilterBySearchWord(int expectedCount, string searchWord)
        {
            // Arrange
            var mockedData = new Mock<ITournamentSystemData>();
            var playerData = new List<Player>
            {
                new Player() {FirstName="Zinedine", LastName="Zidane", TeamId = 1 },
                new Player() {FirstName="Trifon", LastName="Ivanov", TeamId = 1},
                new Player() {FirstName="Daniel", LastName="Borimirov", TeamId = 1},
                new Player() {FirstName="Dimitar", LastName="Berbatov", TeamId = 3}
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var playerCount = playerService.GetPlayersCount(searchWord, null);

            // Assert
            Assert.AreEqual(expectedCount, playerCount);
        }

        [TestCase(1, 1)]
        [TestCase(2, 2, 4)]
        public void ReturnCorrectCount_WhenFilterByTeams(int expectedCount, params int[] teamIds)
        {
            // Arrange
            var mockedData = new Mock<ITournamentSystemData>();
            var playerData = new List<Player>
            {
                new Player() { TeamId = 1 },
                new Player() { TeamId = 2 },
                new Player() { TeamId = 4 },
                new Player() { TeamId = 5 }
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var playerCount = playerService.GetPlayersCount(null, teamIds);

            // Assert
            Assert.AreEqual(expectedCount, playerCount);
        }

        [Test]
        public void ReturnCorrectCount_WhenFilterBySearchWordAndTeams()
        {
            // Arrange
            var searchWord = "ov";
            var teamIds = new int[] { 1, 3 };
            var mockedData = new Mock<ITournamentSystemData>();

            // only the last two fit
            var playerData = new List<Player>
            {
                new Player() {FirstName="Zinedine", LastName="Zidane", TeamId = 1 },
                new Player() {FirstName="Trifon", LastName="Ivanov", TeamId = 2},
                new Player() {FirstName="Daniel", LastName="Borimirov", TeamId = 1},
                new Player() {FirstName="Dimitar", LastName="Berbatov", TeamId = 3}
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var playerCount = playerService.GetPlayersCount(searchWord, teamIds);

            // Assert
            Assert.AreEqual(2, playerCount);
        }

        [Test]
        public void ReturnCountOfAllPlayers_WhenFiltersAreNull()
        {
            // Arrange
            var mockedData = new Mock<ITournamentSystemData>();

            var playerData = new List<Player>
            {
                new Mock<Player>().Object,
                new Mock<Player>().Object,
                new Mock<Player>().Object,
                new Mock<Player>().Object,
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var playerCount = playerService.GetPlayersCount(null, null);

            // Assert
            Assert.AreEqual(playerData.Count(), playerCount);
        }
    }
}
