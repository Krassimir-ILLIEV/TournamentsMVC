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
    public class GetPlayerRating_Should
    {
        [Test]
        public void ReturnRatingOfTheCorrectPlayer()
        {
            // Arrange
            int id = 5;            
            var mockedData = new Mock<ITournamentSystemData>();
            var expectedRating = 8.5;
            var playerData = new List<Player>
            {
                new Player() {FirstName="Zinedine", LastName="Zidane", TeamId = 1,  Rating = expectedRating, Id=id },
                new Player() {FirstName="Trifon", LastName="Ivanov", TeamId = 1, Rating=7.0},
                new Player() {FirstName="Daniel", LastName="Borimirov", TeamId = 1, Rating=6.0},
                new Player() {FirstName="Dimitar", LastName="Berbatov", TeamId = 3, Rating=5.0}
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var rating = playerService.GetPlayerRating(id);

            // Assert
            Assert.AreEqual(expectedRating, rating);
        }

        [Test]
        public void ReturnZero_WhenNoPlayerIsFound()
        {
            var mockedData = new Mock<ITournamentSystemData>();

            mockedData.Setup(x => x.Players.All).Returns(new List<Player>().AsQueryable());

            var playerService = new PlayerService(mockedData.Object);

            // Act
            var rating = playerService.GetPlayerRating(345);

            // Assert
            Assert.AreEqual(0, rating);
        }
    }
}
