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

namespace TournamentsMVC.Servicesests.UnitTests.TeamServiceTests
{
    [TestFixture]
    public class GetAllTeams_Should
    {
        [Test]
        public void ReturnCorrectListOfTeams()
        {
            // Arrange
            var mockedData = new Mock<ITournamentSystemData>();
            var teams = new List<Team>()
            {
                new Mock<Team>().Object,
                new Mock<Team>().Object,
                new Mock<Team>().Object
            }.AsQueryable();

            mockedData.Setup(x => x.Teams.All).Returns(teams);

            var teamService = new TeamService(mockedData.Object);

            // Act
            var result = teamService.GetAllTeams();

            // Assert
            CollectionAssert.AreEqual(teams.ToList(), result);
        }
    }
}
