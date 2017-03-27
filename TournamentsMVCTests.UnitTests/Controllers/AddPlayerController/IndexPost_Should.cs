using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TournamentsMVC.Services.Contracts;
using TournamentsMVC.Mapping;
using TournamentsMVC.ViewModels.Models;
using TestStack.FluentMVCTesting;
using System.Web.Mvc;
using TournamentsMVC.Models;
using System.Web;
using TournamentsMVC.Areas.Admin.Models;
using TournamentsMVC.Areas.Admin.Controllers;
using TournamentsMVC.Common.Constants;

namespace TournamentsMVCTests.UnitTests.Controllers.AddPlayerControllerTests
{
    [TestFixture]
    public class IndexPost_Should
    {
        private Mock<IPlayerService> mockedPlayerService;
        private Mock<ITeamService> mockedTeamService;       
        private AddPlayerController controller;
        private Mock<IMapperAdapter> mockedMapper;             

        [SetUp]
        public void TestSetup()
        {
            this.mockedPlayerService = new Mock<IPlayerService>();
            this.mockedTeamService = new Mock<ITeamService>();            
            this.mockedMapper = new Mock<IMapperAdapter>();

            this.controller = new AddPlayerController(
                mockedPlayerService.Object,
                mockedTeamService.Object);               
        }

        [Test]
        public void HaveHttpPostAttribute()
        {
            var method = typeof(AddPlayerController).GetMethod("Index", new Type[] { typeof(AddPlayerViewModel) });
            var hasHttpPostAttr = method.GetCustomAttributes(typeof(HttpPostAttribute), false).Any();

            Assert.IsTrue(hasHttpPostAttr);
        }

        [Test]
        public void HaveValidateAntiForgeryAttribute()
        {
            var method = typeof(AddPlayerController).GetMethod("Index", new Type[] { typeof(AddPlayerViewModel) });
            var hasHttpPostAttr = method.GetCustomAttributes(typeof(ValidateAntiForgeryTokenAttribute), false).Any();

            Assert.IsTrue(hasHttpPostAttr);
        }

        [Test]
        public void HaveValidateInputAttribute()
        {
            var method = typeof(AddPlayerController).GetMethod("Index", new Type[] { typeof(AddPlayerViewModel) });
            var hasValidateAttr = method.GetCustomAttributes(typeof(ValidateInputAttribute), false).Any();

            Assert.IsTrue(hasValidateAttr);
        }

        [Test]
        public void HaveValidateInputAttribute_WithValueFalse()
        {
            var method = typeof(AddPlayerController).GetMethod("Index", new Type[] { typeof(AddPlayerViewModel) });
            var attr = method.GetCustomAttributes(typeof(ValidateInputAttribute), false)[0] as ValidateInputAttribute;

            Assert.IsFalse(attr.EnableValidation);
        }

        [Test]
        public void ReturnViewWithModel_WhenModelStateIsNotValid()
        {
            // Arrange
            this.controller.ModelState.AddModelError("error", "message");

            var submitModel = new AddPlayerViewModel();

            // Act & Assert
            controller.WithCallTo(c => c.Index(submitModel))
                .ShouldRenderDefaultView()
                .WithModel(submitModel)
                .AndModelError("error");
        }        
      

        [Test]
        public void ReturnViewWithModelError_WhenFileIsNull()
        {
            // Arrange
            var submitModel = new AddPlayerViewModel();
            submitModel.PhotoFile = null;

            // Act & Assert
            this.controller.WithCallTo(x => x.Index(submitModel))
                .ShouldRenderDefaultView()
                .WithModel(submitModel)
                .AndModelErrorFor(m => m.PhotoFile)
                .Containing("photo");
        }

        [Test]
        public void ReturnViewWithModelError_WhenFileIsNotImage()
        {
            // Arrange
            var submitModel = new AddPlayerViewModel();
            var mockedFile = new Mock<HttpPostedFileBase>();
            mockedFile.Setup(x => x.ContentType).Returns("not image");
            submitModel.PhotoFile = mockedFile.Object;

            // Act & Assert
            this.controller.WithCallTo(x => x.Index(submitModel))
                .ShouldRenderDefaultView()
                .WithModel(submitModel)
                .AndModelErrorFor(m => m.PhotoFile)
                .Containing("photo");
        }      

         
    }
}
