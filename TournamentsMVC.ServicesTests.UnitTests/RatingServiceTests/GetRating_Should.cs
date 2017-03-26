using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TournamentsMVC.Services;
using TournamentsMVC.Data.Contracts;
using TournamentsMVC.Models;

namespace TournamentsMVC.ServicesTests.UnitTest.RatingServiceTests
{
    [TestFixture]
    public class GetRating_Should
    {
        [TestCase(1,4.4)]
        [TestCase(2,5.5)]
        [TestCase(3, 6.6)]
        [TestCase(4, 7.7)]
        public void ReturnCorrectRating_WhenPlayerIsFound(int id, double expectedRating)
        {
            // Arrange            
            var mockedData = new Mock<ITournamentSystemData>();

            var playerData = new List<Player>
            {
                new Player() { Id=1, FirstName = "Borislav", LastName="Mihaylov", TeamId = 2, Rating=4.4},
                new Player() { Id=2, FirstName = "Daniel", LastName="Borimirov", TeamId = 2,Rating=5.5},
                new Player() { Id=3, FirstName = "Peter", LastName="Hubchev", TeamId = 1,Rating=6.6},
                new Player() { Id=4, FirstName = "Yordan", LastName="Letchkov", TeamId = 3, Rating=7.7}
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var ratingService = new RatingService(mockedData.Object);

            // Act
            var actualRating = ratingService.GetRating(id);

            // Assert            
            Assert.AreEqual(expectedRating, actualRating);
        }

        public void ReturnZero_WhenPlayerIsNOTFound()
        {
            // Arrange            
            var mockedData = new Mock<ITournamentSystemData>();

            var playerData = new List<Player>
            {
                new Player() { Id=1, FirstName = "Borislav", LastName="Mihaylov", TeamId = 2, Rating=4.4},
                new Player() { Id=2, FirstName = "Daniel", LastName="Borimirov", TeamId = 2,Rating=5.5},
                new Player() { Id=3, FirstName = "Peter", LastName="Hubchev", TeamId = 1,Rating=6.6},
                new Player() { Id=4, FirstName = "Yordan", LastName="Letchkov", TeamId = 3, Rating=7.7}
            }.AsQueryable();

            var notFoundId = 5;
            var expectedRating = 0;

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var ratingService = new RatingService(mockedData.Object);

            // Act
            var actualRating = ratingService.GetRating(notFoundId);

            // Assert            
            Assert.AreEqual(expectedRating, actualRating);
        }        
    }
}
