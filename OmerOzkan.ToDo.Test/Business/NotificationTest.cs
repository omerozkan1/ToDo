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
    public class NotificationTest
    {
        private Mock<INotificationDal> _mock;
        private NotificationService _notificationService;
        private List<Notification> _notifications;
        public NotificationTest()
        {
            _mock = new Mock<INotificationDal>();
            _notificationService = new NotificationService(_mock.Object);
            _notifications = new List<Notification>() { new Notification { Id = 1, AppUserId = "1", Description = "notification", CreatedDate = DateTime.Now, Status = true },
                                                        new Notification { Id = 2, AppUserId = "2", Description = "notification2", CreatedDate = DateTime.Now, Status = false }};
        }

        [Theory]
        [InlineData("3")]
        [InlineData("4")]
        public void GetNotReadUsers_MethodExecute(string appUserId)
        {
            _mock.Setup(x => x.GetNotReadUsers(appUserId)).Returns(_notifications);

            var result = _notificationService.GetNotReadUsers(appUserId);
            var returnValue = Assert.IsType<Notification>(result);

            Assert.Equal(appUserId, returnValue.AppUserId);
            Assert.Equal(_notifications.First().Description, returnValue.Description);
        }


        [Theory]
        [InlineData("3")]
        [InlineData("4")]
        public void GetNotReadCountByAppUserId_MethodExecute(string appUserId)
        {
            _mock.Setup(x => x.GetNotReadCountByAppUserId(appUserId)).Returns(2);

            var result = _notificationService.GetNotReadUsers(appUserId);
            var returnValue = Assert.IsType<List<Notification>>(result);

            Assert.Equal<int>(2, returnValue.ToList().Count);
        }
    }
}
