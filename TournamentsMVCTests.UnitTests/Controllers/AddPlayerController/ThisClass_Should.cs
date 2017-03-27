using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NUnit.Framework;
using TournamentsMVC.Areas.Admin.Controllers;
using TournamentsMVC.Models;

namespace TournamentsMVCTests.UnitTests.Controllers.AddPlayerControllerTests
{
    [TestFixture]
    public class ThisClass_Should
    {
        [Test]
        public void HaveAuthorizeAttribute()
        {
            var attr = Attribute.GetCustomAttribute(typeof(AddPlayerController), typeof(AuthorizeAttribute));

            Assert.IsNotNull(attr);
        }

        [Test]
        public void HaveAuthorizeAttribute_RequiringWithAdminRole()
        {
            var attr = Attribute.GetCustomAttribute(typeof(AddPlayerController), typeof(AuthorizeAttribute)) as AuthorizeAttribute;

            Assert.AreEqual(RoleNames.Admin, attr.Roles);
        }
    }
}
