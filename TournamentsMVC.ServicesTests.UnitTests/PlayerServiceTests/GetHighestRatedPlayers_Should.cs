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
    public class GetHighestRatedPlayers_Should
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void ReturnTopCountPlayers_InCorrectOrder(int count)
        {
            // Arrange            
            var mockedData = new Mock<ITournamentSystemData>();
            
            var playerData = new List<Player>
            {
                new Player() { FirstName = "Borislav", LastName="Mihaylov", TeamId = 2, Rating=4.4},
                new Player() { FirstName = "Daniel", LastName="Borimirov", TeamId = 2,Rating=5.5},
                new Player() { FirstName = "Peter", LastName="Hubchev", TeamId = 1,Rating=6.6},
                new Player() { FirstName = "Yordan", LastName="Letchkov", TeamId = 3, Rating=7.7}
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var players = playerService.GetHighestRatedPlayers(count).ToList();

            // Assert
            var expected = playerData
                .OrderByDescending(x => x.Rating)
                .Take(count).ToList();
            CollectionAssert.AreEqual(expected, players);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void ReturnTopPlayers_WhichCollectionCountIsEqualToTheCountArgument(int count)
        {
            // Arrange            
            var mockedData = new Mock<ITournamentSystemData>();
            
            var playerData = new List<Player>
            {
                new Player() { FirstName = "Borislav", LastName="Mihaylov", TeamId = 2, Rating=4.4},
                new Player() { FirstName = "Daniel", LastName="Borimirov", TeamId = 2,Rating=5.5},
                new Player() { FirstName = "Peter", LastName="Hubchev", TeamId = 1,Rating=6.6},
                new Player() { FirstName = "Yordan", LastName="Letchkov", TeamId = 3, Rating=7.7}
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var players = playerService.GetHighestRatedPlayers(count);

            // Assert            
            Assert.AreEqual(players.Count(), count);            
        }

        public void ReturnTopPlayers_WithCorrectOrder()
        {
            // Arrange            
            var mockedData = new Mock<ITournamentSystemData>();
            var topTwo = 2;

            var Hubchev = new Player() { FirstName = "Peter", LastName = "Hubchev", TeamId = 1, Rating = 7.7 };
            var Letchkov = new Player() { FirstName = "Yordan", LastName = "Letchkov", TeamId = 3, Rating = 6.6 };

            var playerData = new List<Player>
            {
                new Player() { FirstName = "Borislav", LastName="Mihaylov", TeamId = 2, Rating=4.4},
                new Player() { FirstName = "Daniel", LastName="Borimirov", TeamId = 2,Rating=5.5},
                Hubchev,
                Letchkov               
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var players = playerService.GetHighestRatedPlayers(topTwo).ToList();

            // Assert            
            Assert.AreEqual(Hubchev, players[0]);
            Assert.AreEqual(Letchkov, players[1]);
        }

        [TestCase(10)]
        [TestCase(20)]
        public void ReturnCompleteCollection_WhenArgumentIsHigherThanCollectionCount(int count)
        {
            // Arrange            
            var mockedData = new Mock<ITournamentSystemData>();

            var playerData = new List<Player>
            {
                new Player() { FirstName = "Borislav", LastName="Mihaylov", TeamId = 2, Rating=4.4},
                new Player() { FirstName = "Daniel", LastName="Borimirov", TeamId = 2,Rating=5.5},
                new Player() { FirstName = "Peter", LastName="Hubchev", TeamId = 1,Rating=6.6},
                new Player() { FirstName = "Yordan", LastName="Letchkov", TeamId = 3, Rating=7.7}
            }.AsQueryable();

            var expectedCount = playerData.Count();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var players = playerService.GetHighestRatedPlayers(count);

            // Assert
            var expected = playerData
                .OrderBy(x => x.Rating)
                .Take(count);
            Assert.AreEqual(expectedCount, players.Count());
        }        
    }
}
