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
    public class GenericServiceTest
    {
        private Mock<IGenericDal<Duty>> _dutyMock;
        private Mock<IGenericDal<Urgency>> _urgencyMock;
        private Mock<IGenericDal<Notification>> _notificationMock;
        private Mock<IGenericDal<Report>> _reportMock;
        private GenericService<Duty> _dutyService;
        private GenericService<Urgency> _urgencyService;
        private GenericService<Notification> _notificationService;
        private GenericService<Report> _reportService;
        private List<Duty> _duties;
        private List<Urgency> _urgencies;
        private List<Notification> _notifications;
        private List<Report> _reports;
        public GenericServiceTest()
        {
            _dutyMock = new Mock<IGenericDal<Duty>>();
            _dutyService = new GenericService<Duty>(_dutyMock.Object);
            _urgencyMock = new Mock<IGenericDal<Urgency>>();
            _urgencyService = new GenericService<Urgency>(_urgencyMock.Object);
            _reportMock = new Mock<IGenericDal<Report>>();
            _reportService = new GenericService<Report>(_reportMock.Object);
            _notificationMock = new Mock<IGenericDal<Notification>>();
            _notificationService = new GenericService<Notification>(_notificationMock.Object);

            _duties = new List<Duty>() { new Duty { Id = 1, Name = "Test", Status = true, Description = "description", CreatedDate = DateTime.Now, UrgencyId = 1, AppUserId = "3" } };
            _urgencies = new List<Urgency> { new Urgency { Id = 1, Description = "description", Status = true, CreatedDate = DateTime.Now } };
            _notifications = new List<Notification> { new Notification { AppUserId = "3", Id = 1, Description = "description", Status = true, CreatedDate = DateTime.Now } };
            _reports = new List<Report> { new Report { Id = 1, Description = "description", Detail = "detail", DutyId = 2, Status = false, CreatedDate = DateTime.Now } };
        }

        #region Duty (Görev)

        [Fact]
        public async void AddAsync_DutyMethodExecute()
        {
            Duty duty = null;
            _dutyMock.Setup(repo => repo.AddAsync(It.IsAny<Duty>())).Callback<Duty>(x => duty = x);

            await _dutyService.AddAsync(_duties.First());

            _dutyMock.Verify(repo => repo.AddAsync(It.IsAny<Duty>()), Times.Once);
            Assert.Equal(_duties.First().Id, duty.Id);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public async void FindByIdAsync_DutyMethodExecute(int id)
        {
            var duty = GetDutyModel(id);
            _dutyMock.Setup(repo => repo.FindByIdAsync(id)).ReturnsAsync(duty);

            var result = await _dutyService.FindByIdAsync(id);

            Assert.Equal(duty.Id, result.Id);
            Assert.Equal(duty.Name, result.Name);
        }

        [Fact]
        public async void GetAllAsync_DutyMethodExecute()
        {
            _dutyMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_duties);

            var result = await _dutyService.GetAllAsync();

            Assert.Equal<int>(2, result.ToList().Count);
        }

        [Fact]
        public async void RemoveAsync_DutyMethodExecute()
        {
            Duty duty = null;
            _dutyMock.Setup(repo => repo.RemoveAsync(It.IsAny<Duty>())).Callback<Duty>(x => duty = x);

            await _dutyService.RemoveAsync(_duties.First());

            _dutyMock.Verify(repo => repo.RemoveAsync(It.IsAny<Duty>()), Times.Once);
            Assert.Equal(_duties.First().Id, duty.Id);
        }

        [Fact]
        public async void UpdateAsync_DutyMethodExecute()
        {
            Duty duty = null;
            _dutyMock.Setup(repo => repo.UpdateAsync(It.IsAny<Duty>())).Callback<Duty>(x => duty = x);

            await _dutyService.UpdateAsync(_duties.First());

            _dutyMock.Verify(repo => repo.UpdateAsync(It.IsAny<Duty>()), Times.Once);
            Assert.Equal(_duties.First().Id, duty.Id);
        }

        private Duty GetDutyModel(int id)
        {
            return _duties.First(x => x.Id == id);
        }


        #endregion

        #region Urgency(Aciliyet)

        [Fact]
        public async void AddAsync_UrgencyMethodExecute()
        {
            Urgency urgency = null;
            _urgencyMock.Setup(repo => repo.AddAsync(It.IsAny<Urgency>())).Callback<Urgency>(x => urgency = x);

            await _urgencyService.AddAsync(_urgencies.First());

            _urgencyMock.Verify(repo => repo.AddAsync(It.IsAny<Urgency>()), Times.Once);
            Assert.Equal(_urgencies.First().Id, urgency.Id);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public async void FindByIdAsync_UrgencyMethodExecute(int id)
        {
            var urgency = GetUrgencyModel(id);
            _urgencyMock.Setup(repo => repo.FindByIdAsync(id)).ReturnsAsync(urgency);

            var result = await _urgencyService.FindByIdAsync(id);

            Assert.Equal(urgency.Id, result.Id);
            Assert.Equal(urgency.Description, result.Description);
        }

        [Fact]
        public async void GetAllAsync_UrgencyMethodExecute()
        {
            _urgencyMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_urgencies);

            var result = await _urgencyService.GetAllAsync();

            Assert.Equal<int>(2, result.ToList().Count);
        }

        [Fact]
        public async void RemoveAsync_UrgencyMethodExecute()
        {
            Urgency urgency = null;
            _urgencyMock.Setup(repo => repo.RemoveAsync(It.IsAny<Urgency>())).Callback<Urgency>(x => urgency = x);

            await _urgencyService.RemoveAsync(_urgencies.First());

            _urgencyMock.Verify(repo => repo.RemoveAsync(It.IsAny<Urgency>()), Times.Once);
            Assert.Equal(_urgencies.First().Id, urgency.Id);
        }

        [Fact]
        public async void UpdateAsync_UrgencyMethodExecute()
        {
            Urgency urgency = null;
            _urgencyMock.Setup(repo => repo.UpdateAsync(It.IsAny<Urgency>())).Callback<Urgency>(x => urgency = x);

            await _dutyService.UpdateAsync(_duties.First());

            _urgencyMock.Verify(repo => repo.UpdateAsync(It.IsAny<Urgency>()), Times.Once);
            Assert.Equal(_urgencies.First().Id, urgency.Id);
        }

        private Urgency GetUrgencyModel(int id)
        {
            return _urgencies.First(x => x.Id == id);
        }

        #endregion

        #region Notification (Bildirim)

        [Fact]
        public async void AddAsync_NotificationMethodExecute()
        {
            Notification notification = null;
            _notificationMock.Setup(repo => repo.AddAsync(It.IsAny<Notification>())).Callback<Notification>(x => notification = x);

            await _notificationService.AddAsync(_notifications.First());

            _notificationMock.Verify(repo => repo.AddAsync(It.IsAny<Notification>()), Times.Once);
            Assert.Equal(_urgencies.First().Id, notification.Id);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public async void FindByIdAsync_NotificationMethodExecute(int id)
        {
            var notification = GetNotificationModel(id);
            _notificationMock.Setup(repo => repo.FindByIdAsync(id)).ReturnsAsync(notification);

            var result = await _notificationService.FindByIdAsync(id);

            Assert.Equal(notification.Id, result.Id);
            Assert.Equal(notification.Description, result.Description);
        }

        [Fact]
        public async void GetAllAsync_NotificationMethodExecute()
        {
            _notificationMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_notifications);

            var result = await _notificationService.GetAllAsync();

            Assert.Equal<int>(2, result.ToList().Count);
        }

        [Fact]
        public async void RemoveAsync_NotificationMethodExecute()
        {
            Notification notification = null;
            _notificationMock.Setup(repo => repo.RemoveAsync(It.IsAny<Notification>())).Callback<Notification>(x => notification = x);

            await _notificationService.RemoveAsync(_notifications.First());

            _notificationMock.Verify(repo => repo.RemoveAsync(It.IsAny<Notification>()), Times.Once);
            Assert.Equal(_urgencies.First().Id, notification.Id);
        }

        [Fact]
        public async void UpdateAsync_NotificationMethodExecute()
        {
            Notification notification = null;
            _notificationMock.Setup(repo => repo.UpdateAsync(It.IsAny<Notification>())).Callback<Notification>(x => notification = x);

            await _notificationService.UpdateAsync(_notifications.First());

            _notificationMock.Verify(repo => repo.UpdateAsync(It.IsAny<Notification>()), Times.Once);
            Assert.Equal(_notifications.First().Id, notification.Id);
        }

        private Notification GetNotificationModel(int id)
        {
            return _notifications.First(x => x.Id == id);
        }

        #endregion

        #region Report (Rapor)

        [Fact]
        public async void AddAsync_ReportMethodExecute()
        {
            Report report = null;
            _reportMock.Setup(repo => repo.AddAsync(It.IsAny<Report>())).Callback<Report>(x => report = x);

            await _reportService.AddAsync(_reports.First());

            _reportMock.Verify(repo => repo.AddAsync(It.IsAny<Report>()), Times.Once);
            Assert.Equal(_urgencies.First().Id, report.Id);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public async void FindByIdAsync_ReportMethodExecute(int id)
        {
            var report = GetReportModel(id);
            _reportMock.Setup(repo => repo.FindByIdAsync(id)).ReturnsAsync(report);

            var result = await _reportService.FindByIdAsync(id);

            Assert.Equal(report.Id, result.Id);
            Assert.Equal(report.Description, result.Description);
        }

        [Fact]
        public async void GetAllAsync_ReportMethodExecute()
        {
            _reportMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_reports);

            var result = await _reportService.GetAllAsync();

            Assert.Equal<int>(2, result.ToList().Count);
        }

        [Fact]
        public async void RemoveAsync_ReportMethodExecute()
        {
            Report report = null;
            _reportMock.Setup(repo => repo.RemoveAsync(It.IsAny<Report>())).Callback<Report>(x => report = x);

            await _reportService.RemoveAsync(_reports.First());

            _reportMock.Verify(repo => repo.RemoveAsync(It.IsAny<Report>()), Times.Once);
            Assert.Equal(_urgencies.First().Id, report.Id);
        }

        [Fact]
        public async void UpdateAsync_ReportMethodExecute()
        {
            Report report = null;
            _reportMock.Setup(repo => repo.UpdateAsync(It.IsAny<Report>())).Callback<Report>(x => report = x);

            await _reportService.UpdateAsync(_reports.First());

            _reportMock.Verify(repo => repo.UpdateAsync(It.IsAny<Report>()), Times.Once);
            Assert.Equal(_reports.First().Id, report.Id);
        }

        private Report GetReportModel(int id)
        {
            return _reports.First(x => x.Id == id);
        }


        #endregion
    }
}
