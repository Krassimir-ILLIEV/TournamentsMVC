using Moq;
using NUnit.Framework;
using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Mapping;
using TournamentsMVC.Controllers;

namespace TournamentsMVCTests.UnitTests.Controllers.PlayerControllerTests
{
    [TestFixture]
    class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WithCorrectMessage_WhenPlayerServiceIsNull()
        {
            // Arrange
            var mockedRatingsService = new Mock<IRatingService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new PlayerController(null, mockedRatingsService.Object, mockedMapper.Object), 
                Throws.ArgumentNullException.With.Message.Contains("playerService"));
        }

        [Test]
        public void ThrowArgumentNullException_WithCorrectMessage_WhenRatingsServiceIsNull()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new PlayerController(mockedPlayerService.Object, null, mockedMapper.Object),
                Throws.ArgumentNullException.With.Message.Contains("ratingService"));
        }

        [Test]
        public void ThrowArgumentNullException_WithCorrectMessage_WhenMapperIsNull()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedRatingService = new Mock<IRatingService>();

            // Act & Assert
            Assert.That(() => new PlayerController(mockedPlayerService.Object, mockedRatingService.Object, null),
                Throws.ArgumentNullException.With.Message.Contains("mapper"));
        }

        [Test]
        public void NotThrow_WhenArgumentsAreNotNull()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedRatingsService = new Mock<IRatingService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.DoesNotThrow(() => new PlayerController(mockedPlayerService.Object, mockedRatingsService.Object, mockedMapper.Object));
        }
    }
}
