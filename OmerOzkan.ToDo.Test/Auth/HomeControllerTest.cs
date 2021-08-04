using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OmerOzkan.ToDo.Business.Concrete;
using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.Business.StringInfos;
using OmerOzkan.ToDo.Dto.Dtos.AppUserDtos;
using OmerOzkan.ToDo.Entities.Domains;
using OmerOzkan.ToDo.Web.Controllers;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OmerOzkan.ToDo.Test
{
    public class HomeControllerTest
    {
        private Mock<SignInManager<AppUser>> _signInManagerMock;
        private Mock<UserManager<AppUser>> _userManagerMock;
        private Mock<AppUserService> _appUserMock;
        private Mock<ICustomLogger> _customLoggerMock;
        private HomeController _homeController;
        private List<AppUserLoginDto> _appUserLogin;
        private List<AppUserSignUpDto> _appUserSignUp;

        public HomeControllerTest()
        {
            _signInManagerMock = new Mock<SignInManager<AppUser>>();
            _userManagerMock = new Mock<UserManager<AppUser>>();
            _customLoggerMock = new Mock<ICustomLogger>();
            _appUserMock = new Mock<AppUserService>();
            _homeController = new HomeController(_appUserMock.Object, _customLoggerMock.Object, _userManagerMock.Object,_signInManagerMock.Object);

            _appUserLogin = new List<AppUserLoginDto>() { new AppUserLoginDto { Email = "omeerozkan52@gmail.com",Password = "Test",RememberMe = true },
                                                      new AppUserLoginDto{ Email = "ugurozkan@gmail.com",Password = "Test2",RememberMe = false }};

            _appUserSignUp = new List<AppUserSignUpDto> { new AppUserSignUpDto { Name = "omer", Surname = "ozkan", Email = "omeerozkan52@gmail.com", Password = "Test", UserName = "omerozkan", ConfirmPassword = "Test" },
            new AppUserSignUpDto { Name = "ugur", Surname = "ozkan", Email = "ugurozkan@gmail.com", Password = "Test2", ConfirmPassword = "Test2", UserName = "ugurozkan"  }};
        }

        [Fact]
        public async void Login_FindByEmailAsyncMethodExecute()
        {
            AppUser appUser = null;
            _userManagerMock.Setup(repo => repo.FindByEmailAsync(_appUserLogin.First().Email)).Callback<AppUser>(x => appUser = x);

            var result = await _homeController.Login(_appUserLogin.First());

            _userManagerMock.Verify(repo => repo.FindByEmailAsync(_appUserLogin.First().Email), Times.Once);
            Assert.Equal(_appUserLogin.First().Email, appUser.Email);
        }

        [Fact]
        public async void Login_PasswordSignInAsyncMethodExecute()
        {
            AppUser appUser = null;

            _userManagerMock.Setup(repo => repo.FindByEmailAsync(_appUserLogin.First().Email)).Callback<AppUser>(x => appUser = x);
            _signInManagerMock.Setup(repo => repo.PasswordSignInAsync(appUser, _appUserLogin.First().Password, _appUserLogin.First().RememberMe, false));
            var result = await _homeController.Login(_appUserLogin.First());

            _signInManagerMock.Verify(repo => repo.PasswordSignInAsync(appUser, _appUserLogin.First().Password, _appUserLogin.First().RememberMe, false), Times.Once);
            Assert.Equal(_appUserLogin.First().Email, appUser.Email);
        }

        [Fact]
        public async void Login_InValidModelState_NeverLoginExecute()
        {
            _homeController.ModelState.AddModelError("Email", "");

            AppUser appUser = null;
            _userManagerMock.Setup(repo => repo.FindByEmailAsync(_appUserLogin.First().Email)).Callback<AppUser>(x => appUser = x);

            var result = await _homeController.Login(_appUserLogin.First());
            _signInManagerMock.Verify(repo => repo.PasswordSignInAsync(appUser, _appUserLogin.First().Password, _appUserLogin.First().RememberMe, false), Times.Never);
        }

        [Fact]
        public async void SignUp_InValidModelState_ReturnView()
        {
            _homeController.ModelState.AddModelError("Email", "Email alanı gereklidir.");

            var result = await _homeController.SignUp(_appUserSignUp.First());
            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.IsType<AppUserSignUpDto>(viewResult.Model);
        }

        [Fact]
        public async void SignUp_ValidModelState_ReturnRedirectToIndexAction()
        {
            var result = await _homeController.SignUp(_appUserSignUp.First());
            var redirect = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async void SignUp_ValidModelState_CreateAsyncMethodExecute()
        {
            AppUser appUser = null;
            IdentityResult identityResult = null;
            _userManagerMock.Setup(repo => repo.FindByEmailAsync(_appUserLogin.First().Email)).Callback<AppUser>(x => appUser = x);
            _userManagerMock.Setup(repo => repo.CreateAsync(appUser, _appUserSignUp.First().Password)).Callback<IdentityResult>(x => identityResult = x);

            var result = await _homeController.SignUp(_appUserSignUp.First());

            _userManagerMock.Verify(repo => repo.CreateAsync(appUser, _appUserSignUp.First().Password), Times.Once);

            Assert.Equal(_appUserSignUp.First().Name, appUser.Name);
            Assert.Equal(_appUserSignUp.First().Surname, appUser.SurName);
            Assert.Equal(_appUserSignUp.First().Email, appUser.Email);
            Assert.Equal(_appUserSignUp.First().UserName, appUser.UserName);
        }


        [Fact]
        public async void SignUp_ValidModelState_AddToRoleAsyncMethodExecute()
        {
            AppUser appUser = null;
            IdentityResult identityResult = null;
            _userManagerMock.Setup(repo => repo.FindByEmailAsync(_appUserLogin.First().Email)).Callback<AppUser>(x => appUser = x);
            _userManagerMock.Setup(repo => repo.CreateAsync(appUser, _appUserSignUp.First().Password)).Callback<IdentityResult>(x => identityResult = x);

            var result = await _homeController.SignUp(_appUserSignUp.First());
            _userManagerMock.Verify(repo => repo.AddToRoleAsync(appUser, RoleInfo.Member), Times.Once);
        }

        [Fact]
        public async void Logout_ValidModelState_ReturnRedirectToIndexAction()
        {
            var result = await _homeController.Logout();
            var redirect = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async void Logout_ActionExecutes_SignOutAsyncMethodExecute()
        {
            var result = await _homeController.Logout();
            _signInManagerMock.Verify(repo => repo.SignOutAsync(), Times.Once);
        }
    }
}
