
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Web.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using ApplicationCore.Models.Client;
using Ardalis.Result;
using Microsoft.AspNetCore.Identity.UI.Services;
using ApplicationCore.Entities;
using Humanizer;
using ApplicationCore.Entities.Abstract;

namespace WebTests
{
    public class RegisterPageTests
    {
        [Fact]
        public async Task OnPostAsync_ShouldCreateUserAndClient_WhenModelIsValid()
        {

            // Arrange
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);

            _ = mockUserManager.Setup(x => x.SupportsUserEmail)
                .Returns(true);

            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();

            var mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
                mockUserManager.Object,
                _contextAccessor.Object, 
                _userPrincipalFactory.Object,
                null, null, null, null);

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var mockEmailStore = new Mock<IUserEmailStore<ApplicationUser>>();
            var mockClientService = new Mock<IClientService>();
            var mockLogger = new Mock<ILogger<RegisterModel>>();

            var model = new RegisterModel(
                mockUserManager.Object,
                mockEmailStore.Object,
                mockSignInManager.Object,
                mockLogger.Object,
                mockClientService.Object)
            {
                Input = new RegisterModel.InputModel
                {
                    Email = "test@example.com",
                    Password = "Password123!",
                    ConfirmPassword = "Password123!",
                    Name = "John",
                    Surname = "Doe"
                }
            };

            mockUserManager
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            mockUserManager
                .Setup(x => x.GetUserIdAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync("userId123");

            mockClientService
                .Setup(x => x.AddClient(It.IsAny<NewClientModel>()))
                .ReturnsAsync(Result.Success);

            // Act
            var returnUrl = "/home";
            var result = await model.OnPostAsync(returnUrl);

            // Assert
            Assert.IsType<LocalRedirectResult>(result);
            var redirectResult = (LocalRedirectResult)result;
            Assert.Equal(returnUrl, redirectResult.Url);

            mockUserManager.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), "Password123!"), Times.Once);
            mockClientService.Verify(x => x.AddClient(It.IsAny<NewClientModel>()), Times.Once);
        }

        [Fact]
        public async Task OnPostAsync_ShouldRedirectToError_WhenClientCreationFails()
        {
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);

            _ = mockUserManager.Setup(x => x.SupportsUserEmail)
                .Returns(true);

            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();

            var mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
                mockUserManager.Object,
                _contextAccessor.Object,
                _userPrincipalFactory.Object,
                null, null, null, null);

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var mockEmailStore = new Mock<IUserEmailStore<ApplicationUser>>();
            var mockClientService = new Mock<IClientService>();
            var mockLogger = new Mock<ILogger<RegisterModel>>();

            var model = new RegisterModel(
                mockUserManager.Object,
                mockEmailStore.Object,
                mockSignInManager.Object,
                mockLogger.Object,
                mockClientService.Object)
            {
                Input = new RegisterModel.InputModel
                {
                    Email = "test@example.com",
                    Password = "Password123!",
                    ConfirmPassword = "Password123!",
                    Name = "John",
                    Surname = "Doe"
                }
            };

            mockUserManager
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            mockUserManager
                .Setup(x => x.GetUserIdAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync("userId123");

            mockClientService
                .Setup(x => x.AddClient(It.IsAny<NewClientModel>()))
                .ReturnsAsync(Result.Error(("Unable to add client")));

            // Act
            var returnUrl = "/home";
            var result = await model.OnPostAsync(returnUrl);

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
            var redirectResult = (RedirectToPageResult)result;
            Assert.Equal("Error", redirectResult.PageName);

            mockUserManager.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), "Password123!"), Times.Once);
            mockClientService.Verify(x => x.AddClient(It.IsAny<NewClientModel>()), Times.Once);
        }
    }
}