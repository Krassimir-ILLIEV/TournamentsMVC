using Moq;
using NUnit.Framework;
using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Mapping;
using TournamentsMVC.Controllers;


namespace TournamentsMVCTests.UnitTests.Controllers.HomeControllerTests
{
    [TestFixture]
    class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WithCorrectMessage_WhenPlayerServiceIsNull()
        {
            // Arrange
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new HomeController(null, mockedMapper.Object),
                Throws.ArgumentNullException.With.Message.Contains("playerService"));
        }

        [Test]
        public void ThrowArgumentNullException_WithCorrectMessage_WhenMapperIsNull()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();

            // Act & Assert
            Assert.That(() => new HomeController(mockedPlayerService.Object, null),
                Throws.ArgumentNullException.With.Message.Contains("mapper"));
        }

        [Test]
        public void NotThrow_WhenArgumentsAreNotNull()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.DoesNotThrow(() => new HomeController(mockedPlayerService.Object, mockedMapper.Object));
        }
    }
}
