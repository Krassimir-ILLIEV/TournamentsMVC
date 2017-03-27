using NUnit.Framework;
using System;
using System.Web.Mvc;
using TournamentsMVC.Controllers;

namespace TournamentsMVCTests.UnitTests.Controllers.ChatControllerTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void HaveAuthorizeAttribute()
        {
            var attr = Attribute.GetCustomAttribute(typeof(ChatController), typeof(AuthorizeAttribute));

            Assert.IsNotNull(attr);
        }
    }
}
