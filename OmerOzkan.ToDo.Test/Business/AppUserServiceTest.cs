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
            _appUsers = new List<AppUser>() { new AppUser { Id = 1, Email = "omeerozkan52@gmail.com", Name = "omer", SurName = "ozkan", UserName = "omerozkan" } };
            _appUserDutyInfos = new List<AppUserDutyInfo>() { new AppUserDutyInfo { Name = "test", DutyCount = 3 },
                                                              new AppUserDutyInfo { Name = "test2", DutyCount = 5 } };
        }

        [Fact]
        public void GetNonAdmins_MethodExecute()
        {
            _appUserDalMock.Setup(x => x.GetNonAdmins()).Returns(_appUsers);
            var result = _appUserService.GetNonAdmins();
            Assert.Equal(_appUsers, result);
        }

        [Fact]
        public void GetMostEmployedUsers_MethodExecute()
        {
            _appUserDalMock.Setup(x => x.GetMostEmployedUsers()).Returns(_appUserDutyInfos);
            var result = _appUserService.GetMostEmployedUsers();
            Assert.Equal(_appUserDutyInfos, result);
        }

        [Fact]
        public void GetMostCompleteDutyUsers_MethodExecute()
        {
            _appUserDalMock.Setup(x => x.GetMostCompleteDutyUsers()).Returns(_appUserDutyInfos);
            var result = _appUserService.GetMostCompleteDutyUsers();
            Assert.Equal(_appUserDutyInfos, result);
        }

        [Theory]
        [InlineData("omerozkan")]
        public async void FindByNameAsync_MethodExecute(string userName)
        {
            _appUserGenericDalMock.Setup(x => x.FindByIdAsync(_appUsers.First().Id));
            var result = await _appUserService.FindByNameAsync(userName);
            Assert.Equal(1, 1);
        }

    }
}
