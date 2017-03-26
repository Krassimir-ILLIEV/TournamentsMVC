using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Mapping;
using TournamentsMVC.Controllers;

namespace TournamentsMVC.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    public class About_Should
    {
        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            var controller = new HomeController(mockedPlayerService.Object, mockedMapper.Object);

            // Act & Assert
            controller.WithCallTo(c => c.About()).ShouldRenderDefaultView();
        }
    }
}
