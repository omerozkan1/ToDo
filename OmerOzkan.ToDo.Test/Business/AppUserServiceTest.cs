using Microsoft.AspNetCore.Mvc;
using Moq;
using OmerOzkan.ToDo.Business.Concrete;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OmerOzkan.ToDo.Test.Business
{
    public class AppUserServiceTest
    {
        private Mock<IGenericDal<AppUser>> _appUserGenericDalMock;
        private Mock<IAppUserDal> _appUserDalMock;
        private AppUserService _appUserService;
        private List<AppUser> _appUsers;
        private List<AppUserDutyInfo> _appUserDutyInfos;
        public AppUserServiceTest()
        {
            _appUserGenericDalMock = new Mock<IGenericDal<AppUser>>();
            _appUserDalMock = new Mock<IAppUserDal>();
            _appUserService = new AppUserService(_appUserGenericDalMock.Object, _appUserDalMock.Object);
            _appUsers = new List<AppUser>() { new AppUser { Id = 3, Email = "omeerozkan52@gmail.com", Name = "omer", SurName = "ozkan", UserName = "omerozkan" } };
            _appUserDutyInfos = new List<AppUserDutyInfo>() { new AppUserDutyInfo { Name = "test", DutyCount = 3 },
                                                              new AppUserDutyInfo { Name = "test2", DutyCount = 5 } };
        }

        [Fact]
        public void GetNonAdmins_MethodExecute()
        {
            _appUserDalMock.Setup(x => x.GetNonAdmins()).Returns(_appUsers);

            var result = _appUserService.GetNonAdmins();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<AppUser>>(okResult.Value);

            Assert.Equal<int>(2, returnValue.ToList().Count);
        }

        [Fact]
        public void GetMostEmployedUsers_MethodExecute()
        {
            _appUserDalMock.Setup(x => x.GetMostEmployedUsers()).Returns(_appUserDutyInfos);

            var result = _appUserService.GetMostEmployedUsers();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<AppUserDutyInfo>>(okResult.Value);

            Assert.Equal<int>(2, returnValue.ToList().Count);
        }

        [Fact]
        public void GetMostCompleteDutyUsers_MethodExecute()
        {
            _appUserDalMock.Setup(x => x.GetMostCompleteDutyUsers()).Returns(_appUserDutyInfos);

            var result = _appUserService.GetMostCompleteDutyUsers();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<AppUserDutyInfo>>(okResult.Value);

            Assert.Equal<int>(2, returnValue.ToList().Count);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("test2")]
        [InlineData("omerozkan")]
        public async void FindByNameAsync_MethodExecute(string userName)
        {
            _appUserGenericDalMock.Setup(x => x.FindByIdAsync(_appUsers.First().Id));

            var result = await _appUserService.FindByNameAsync(userName);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<AppUser>(okResult.Value);

            Assert.Equal(userName, returnValue.UserName);
        }

    }
}
