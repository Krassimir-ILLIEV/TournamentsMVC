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
    public class SearchPlayers_Should
    {
        [TestCase("Leo")]
        [TestCase("Totti")]
        public void FilterPlayersBySearchWord_IfFoundInTheirFirstOrLastNames(string searchWord)
        {
            // Arrange
            var mockedData = new Mock<ITournamentSystemData>();
            var playerData = new List<Player>
            {
                new Player() { FirstName = "Paul", LastName = "Scholes"},
                new Player() { FirstName = "Francesco", LastName = "Totti"},
                new Player() { FirstName = "Leonel", LastName = "Messi"},
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var players = playerService.SearchPlayers(searchWord, null, null, 1, 10);

            // Assert
            var expected = playerData.Where(x => x.FirstName.Contains(searchWord) || x.LastName.Contains(searchWord)).ToList();
            CollectionAssert.AreEquivalent(expected, players);
        }

        [TestCase(1)]
        [TestCase(2, 4)]
        public void FilterByTeams(params int[] teamIds)
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
            var players = playerService.SearchPlayers(null, teamIds, null, 1, 10);

            // Assert
            var expected = playerData.Where(x => teamIds.Contains((int)x.TeamId)).ToList();
            CollectionAssert.AreEquivalent(expected, players);
        }

        [Test]
        public void FilterBySearchWordAndTeams()
        {
            // Arrange
            var searchWord = "Stoich";
            var teamIds = new int[] { 1, 3 };
            var mockedData = new Mock<ITournamentSystemData>();
            
            //only first and third players fit
            var playerData = new List<Player>
            {
                new Player() {FirstName="Hristo", LastName="Stoichkov", TeamId = 1 },
                new Player() {FirstName="Eric", LastName="Cantona", TeamId = 3 },
                new Player() {FirstName="Christo", LastName="Stoichkov", TeamId = 3 },
                new Player() {FirstName="Trifon", LastName="Ivanov", TeamId = 3 }
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var players = playerService.SearchPlayers(searchWord, teamIds, null, 1, 10);

            // Assert
            var expected = playerData
                .Where(x => x.FirstName.Contains(searchWord) || x.LastName.Contains(searchWord))
                .Where(x => teamIds.Contains((int)x.TeamId)).ToList();

            CollectionAssert.AreEquivalent(expected, players);
        }

        [Test]
        public void ReturnCompleteCollection_WhenFiltersAreNull()
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
            var players = playerService.SearchPlayers(null, null, null, 1, 10);

            // Assert
            CollectionAssert.AreEquivalent(playerData.ToList(), players);
        }
        
        [Test]
        public void ReturnCorrectlyOrderedCollection_WhenArgumentIsFirstName()
        {
            // Arrange
            var mockedData = new Mock<ITournamentSystemData>();
            var playerData = new List<Player>
            {
                new Player() { FirstName = "Zinedine" },
                new Player() { FirstName = "Mario" },
                new Player() { FirstName = "Eric" },
                new Player() { FirstName = "Christiano" }
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var players = playerService.SearchPlayers(null, null, "FirstName", 1, 10);

            // Assert
            var expected = playerData.OrderBy(x => x.FirstName).ToList();
            CollectionAssert.AreEqual(expected, players);
        }        

        [Test]
        public void OrderByLastName_WhenArgumentIsIncorrect()
        {
            // Arrange
            var mockedData = new Mock<ITournamentSystemData>();
            var playerData = new List<Player>
            {
                new Player() { LastName = "Zidane" },
                new Player() { LastName = "Ronaldo" },
                new Player() { LastName = "Iniesta" },
                new Player() { LastName = "Ancelotti" }
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var players = playerService.SearchPlayers(null, null, "noSuchProperty", 1, 10);

            // Assert
            var expected = playerData.OrderBy(x => x.LastName).ToList();
            CollectionAssert.AreEqual(expected, players);
        }

        [Test]
        public void OrderAndFilterCorrectly()
        {
            // Arrange
            var searchWord = "e";
            var teamIds = new int[] { 2, 3 };
            var mockedData = new Mock<ITournamentSystemData>();

            // only second and fourth fit
            var playerData = new List<Player>
            {
                new Player() { FirstName = "Borislav", LastName="Mihaylov", TeamId = 2},
                new Player() { FirstName = "Daniel", LastName="Borimirov", TeamId = 2},
                new Player() { FirstName = "Peter", LastName="Hubchev", TeamId = 1},
                new Player() { FirstName = "Yordan", LastName="Letchkov", TeamId = 3}
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var players = playerService.SearchPlayers(searchWord, teamIds, "FirstName", 1, 10);

            // Assert
            var expected = playerData
                .Where(x => x.FirstName.Contains(searchWord) || x.LastName.Contains(searchWord))
                .Where(x => teamIds.Contains((int)x.TeamId))
                .OrderBy(x=>x.FirstName)
                .ToList();
            CollectionAssert.AreEqual(expected, players);
        }
        
        [Test]
        public void SkipCorrectNumberOfPlayers()
        {
            int page = 2;
            int perPage = 3;
            var mockedData = new Mock<ITournamentSystemData>();
            var mockedNextToLastPlayer = new Mock<Player>();
            var mockedLastPlayer = new Mock<Player>();
            var playerData = new List<Player>
            {
                new Mock<Player>().Object,
                new Mock<Player>().Object,
                new Mock<Player>().Object,
                new Mock<Player>().Object,
                new Mock<Player>().Object
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var players = playerService.SearchPlayers(null, null, null, page, perPage);

            // Assert
            var expected = playerData.OrderBy(x => x.LastName).Skip(3).Take(perPage);
            CollectionAssert.AreEqual(expected, players);
        }
        
        [Test]
        public void TakeCorrectNumberOfPlayers()
        {
            int page = 3;
            int perPage = 2;
            var mockedData = new Mock<ITournamentSystemData>();
            var mockedNextToLastPlayer = new Mock<Player>();
            var mockedLastPlayer = new Mock<Player>();
            var playerData = new List<Player>
            {
                new Mock<Player>().Object,
                new Mock<Player>().Object,
                new Mock<Player>().Object,
                new Mock<Player>().Object,
                new Mock<Player>().Object,
                new Mock<Player>().Object,
                new Mock<Player>().Object,
                new Mock<Player>().Object
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var players = playerService.SearchPlayers(null, null, null, page, perPage);

            // Assert
            var expected = playerData.OrderBy(x => x.LastName).Skip(4).Take(perPage);
            CollectionAssert.AreEqual(expected, players);
        }
    }
}
