using Moq;
using NUnit.Framework;
using TournamentsMVC.Controllers;
using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Data.Contracts;
using TournamentsMVC.Areas.Admin.Controllers;

namespace TournamentsMVCTests.UnitTests.Controllers.AddPlayerControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WithCorrectMessage_WhenPlayerServiceIsNull()
        {
            // Arrange
            var mockedTeamService = new Mock<ITeamService>();

            // Act & Assert
            Assert.That(() => new AddPlayerController(null, mockedTeamService.Object), Throws.ArgumentNullException.With.Message.Contains("playerService"));
        }

        [Test]
        public void ThrowArgumentNullException_WithCorrectMessage_WhenTeamServiceIsNull()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();

            // Act & Assert
            Assert.That(() => new AddPlayerController(mockedPlayerService.Object, null), Throws.ArgumentNullException.With.Message.Contains("teamService"));
        }

        [Test]
        public void NotThrow_WhenAllParametersAreNotNull()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedTeamService = new Mock<ITeamService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new AddPlayerController(mockedPlayerService.Object, mockedTeamService.Object));
        }
    }
}
