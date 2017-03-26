using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Mapping;
using TournamentsMVC.Controllers;

namespace TournamentsMVCTests.UnitTests.Controllers.PlayerControllerTests
{
    [TestFixture]
    public class GetRatingPartial_Should
    {
        public void NotThrow_WhenDataIsNotNull()
        {
            // Arrange
            var mockedPlayerService = new Mock<IPlayerService>();
            var mockedRatingService = new Mock<IRatingService>();
            var mockedMapper = new Mock<IMapperAdapter>();

            // Act & Assert
            Assert.DoesNotThrow(() => new PlayerController(mockedPlayerService.Object, mockedRatingService.Object, mockedMapper.Object));
        }
    }
}
