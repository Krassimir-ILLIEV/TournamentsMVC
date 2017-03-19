using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TournamentsMVC.Models;

namespace TournamentsMVC.IntegrationTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            ApplicationDbContext context = new ApplicationDbContext();

            // Act
            int usersCount = context.Users.Count();

            // Assert
            Assert.AreEqual(1, usersCount);
        }
    }
}
