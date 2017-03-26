﻿using System;
using Moq;
using NUnit.Framework;
using TournamentsMVC.Services;
using TournamentsMVC.Data.Contracts;

namespace TournamentsMVC.Services.Tests.UsersServiceTests
{
    [TestFixture]
    class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenDataIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UserService(null));
        }

        [Test]
        public void ThrowArgumentNullException_WithCorrectMessage_WhenDataIsNull()
        {
            // Act & Assert
            Assert.That(() => new UserService(null), Throws.ArgumentNullException.With.Message.Contains("data"));
        }

        [Test]
        public void NotThrow_WhenDataIsNotNull()
        {
            // Arrange
            var mockedData = new Mock<ITournamentSystemData>();

            // Act & Assert
            Assert.DoesNotThrow(() => new UserService(mockedData.Object));
        }
    }
}
