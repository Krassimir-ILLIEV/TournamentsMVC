using Moq;
using NUnit.Framework;
using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Mapping;
using TournamentsMVC.Controllers;

namespace TournamentsMVCTests.UnitTests.Controllers.SearchControllerTests
{
    [TestFixture]
    class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WithCorrectMessage_WhenPlayerServiceIsNull()
        {
            // Arrange
            var mockedTeamService = new Mock<ITeamService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new SearchController(null, mockedTeamService.Object, mockedMapper.Object),
                Throws.ArgumentNullException.With.Message.Contains("playerService"));
        }

        [Test]
        public void ThrowArgumentNullException_WithACorrectMessage_WhenTeamServiceIsNull()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.That(() => new SearchController(mockedPlayerService.Object, null, mockedMapper.Object),
                Throws.ArgumentNullException.With.Message.Contains("teamService"));
        }

        [Test]
        public void ThrowArgumentNullException_WithCorrectMessage_WhenMapperIsNull()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();

            // Act & Assert
            Assert.That(() => new SearchController(mockedPlayerService.Object, mockedTeamService.Object, null),
                Throws.ArgumentNullException.With.Message.Contains("mapper"));
        }

        [Test]
        public void NotThrow_WhenArgumentsAreNotNullAndOfRequiredType()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.DoesNotThrow(() => new SearchController(mockedPlayerService.Object, mockedTeamService.Object, mockedMapper.Object));
        }
    }
}

