using Moq;
using OmerOzkan.ToDo.Business.Concrete;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OmerOzkan.ToDo.Test.Business
{
    public class NotificationServiceTest
    {
        private Mock<INotificationDal> _mock;
        private NotificationService _notificationService;
        private List<Notification> _notifications;
        public NotificationServiceTest()
        {
            _mock = new Mock<INotificationDal>();
            _notificationService = new NotificationService(_mock.Object);
            _notifications = new List<Notification>() { new Notification { Id = 1, AppUserId = "1", Description = "notification", CreatedDate = DateTime.Now, Status = true },
                                                        new Notification { Id = 2, AppUserId = "2", Description = "notification2", CreatedDate = DateTime.Now, Status = false }};
        }

        [Theory]
        [InlineData("1")]
        public void GetNotReadUsers_MethodExecute(string appUserId)
        {
            _mock.Setup(x => x.GetNotReadUsers(appUserId)).Returns(_notifications);

            var result = _notificationService.GetNotReadUsers(appUserId);
            var returnValue = Assert.IsType<List<Notification>>(result);

            Assert.Equal(appUserId, returnValue.First().AppUserId);
            Assert.Equal(_notifications.First().Description, returnValue.First().Description);
        }


        [Theory]
        [InlineData("1")]
        public void GetNotReadCountByAppUserId_MethodExecute(string appUserId)
        {
            _mock.Setup(x => x.GetNotReadCountByAppUserId(appUserId)).Returns(1);
            var result = _notificationService.GetNotReadCountByAppUserId(appUserId);
            Assert.Equal(1, result);
        }
    }
}
