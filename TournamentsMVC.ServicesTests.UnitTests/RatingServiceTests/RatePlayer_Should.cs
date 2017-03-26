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
    public class RatePlayer_Should
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void ReturnPlayerWithCorrectRating_WhenPlayerIsFound(int ratingGivenByUser)
        {
            // Arrange            
            var mockedData = new Mock<ITournamentSystemData>();
            var id = 1;
            var player = new Player() { Id = id, FirstName = "Borislav", LastName = "Mihaylov", TeamId = 2, Rating = 2.0, Votes = 2 };

            var playerData = new List<Player>
            {
                 player
            }.AsQueryable();

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var ratingService = new RatingService(mockedData.Object);

            // Act            
            var expectedVotes = player.Votes + 1;
            var actualRating = ((player.Rating * player.Votes) + ratingGivenByUser) / (expectedVotes);
            var playerAfterRating = ratingService.RatePlayer(id, ratingGivenByUser);
            var expectedRating = playerAfterRating.Rating;
            var epsilon = 0.00001d;

            // Assert            
            Assert.IsTrue(Math.Abs((double)(expectedRating - actualRating)) < epsilon);
            Assert.AreEqual(expectedVotes, playerAfterRating.Votes);
        }

        public void ReturnNull_WhenPlayerIsNOTFound()
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
            var expectedRating = (Player) null;

            mockedData.Setup(x => x.Players.All).Returns(playerData);

            var ratingService = new RatingService(mockedData.Object);

            // Act
            var actualRating = ratingService.RatePlayer(notFoundId, 3); //null

            // Assert            
            Assert.AreEqual(expectedRating, actualRating);
        }
    }
}
