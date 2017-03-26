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

namespace TournamentsMVC.ServicesTests.UnitTests.UsersServiceTests
{
    [TestFixture]
    public class CheckIfUserExists_Should
    {
        [Test]
        public void ReturnTrue_WhenUserFound()
        {
            // Arrange
            var username = "t3hL33Tuser";
            var mockedData = new Mock<ITournamentSystemData>();
            var existingUser = new Mock<User>();
            existingUser.Setup(x => x.UserName).Returns(username);
            var users = new List<User>()
            {
                new Mock<User>().Object,
                new Mock<User>().Object,
                new Mock<User>().Object,
                existingUser.Object
            }.AsQueryable();

            mockedData.Setup(x => x.Users.All).Returns(users);

            var service = new UserService(mockedData.Object);

            // Act
            var result = service.CheckIfUserExists(username);

            // Assert 
            Assert.IsTrue(result);
        }

        [Test]
        public void ReturnFalse_WhenUserIsNotFound()
        {
            // Arrange
            var username = "t3hL33Tuser";
            var mockedData = new Mock<ITournamentSystemData>();
            var users = new List<User>()
            {
                new Mock<User>().Object,
                new Mock<User>().Object,
                new Mock<User>().Object
            }.AsQueryable();

            mockedData.Setup(x => x.Users.All).Returns(users);

            var service = new UserService(mockedData.Object);

            // Act
            var result = service.CheckIfUserExists(username);

            // Assert 
            Assert.IsFalse(result);
        }
    }
}
