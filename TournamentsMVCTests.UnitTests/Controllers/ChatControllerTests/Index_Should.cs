using NUnit.Framework;
using TournamentsMVC.Controllers;
using TestStack.FluentMVCTesting;

namespace TournamentsMVCTests.UnitTests.Controllers.ChatControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var controller = new ChatController();

            // Act & Assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }
    }
}
