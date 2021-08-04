using Microsoft.AspNetCore.Mvc;
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
    public class DutyServiceTest
    {
        private Mock<IDutyDal> _mock;
        private DutyService _dutyService;
        private List<Duty> _duties;
        public DutyServiceTest()
        {
            _mock = new Mock<IDutyDal>();
            _dutyService = new DutyService(_mock.Object);
            _duties = new List<Duty>() { new Duty { Id = 1, Name = "Test", Status = true, Description = "description", CreatedDate = DateTime.Now, UrgencyId = 1, AppUserId = "1" },
                                         new Duty {Id = 2, Name = "Test2", Status = false, Description = "description2", CreatedDate = DateTime.Now, UrgencyId = 2, AppUserId = "2"} };
        }

        [Fact]
        public void GetAll_MethodExecute()
        {
            _mock.Setup(x => x.GetAll()).Returns(_duties);
            var result = _dutyService.GetAll();
            Assert.Equal(_duties, result);
        }


        [Theory]
        [InlineData("1")]
        public void GetByAppUserId_MethodExecute(string appUserId)
        {
            _mock.Setup(x => x.GetByAppUserId(appUserId)).Returns(_duties);
            var result = _dutyService.GetByAppUserId(appUserId);
            Assert.Equal(_duties, result);
        }

        [Fact]
        public void GetByIncompleteWithUrgency_MethodExecute()
        {
            _mock.Setup(x => x.GetByIncompleteWithUrgency()).Returns(_duties);
            var result = _dutyService.GetByIncompleteWithUrgency();
            Assert.Equal(_duties, result);
        }

        [Theory]
        [InlineData(1)]
        public void GetByReportId_MethodExecute(int id)
        {
            _mock.Setup(x => x.GetByReportId(id)).Returns(_duties.First());
            var result = _dutyService.GetByReportId(id);
            Assert.Equal(id, result.Id);
        }

        [Theory]
        [InlineData(1)]
        public void GetByUrgencyId_MethodExecute(int id)
        {
            _mock.Setup(x => x.GetByUrgencyId(id)).Returns(_duties.First());
            var result = _dutyService.GetByUrgencyId(id);
            Assert.Equal(id, result.Id);
        }

        [Theory]
        [InlineData("1")]
        public void GetDutyCountCompleteByAppUserId_MethodExecute(string id)
        {
            _mock.Setup(x => x.GetDutyCountCompleteByAppUserId(id)).Returns(1);
            var result = _dutyService.GetDutyCountCompleteByAppUserId(id);
            Assert.Equal(1, result);
        }

        [Fact]
        public void GetDutyCountCompleted_MethodExecute()
        {
            _mock.Setup(x => x.GetDutyCountCompleted()).Returns(1);
            var result = _dutyService.GetDutyCountCompleted();
            Assert.Equal(1, result);
        }


        [Fact]
        public void GetDutyCountPendingAssignment_MethodExecute()
        {
            _mock.Setup(x => x.GetDutyCountPendingAssignment()).Returns(1);
            var result = _dutyService.GetDutyCountPendingAssignment();
            Assert.Equal(1, result);
        }

        [Theory]
        [InlineData("1")]
        public void GetDutyCountToBeCompletedByAppUserId_MethodExecute(string id)
        {
            _mock.Setup(x => x.GetDutyCountToBeCompletedByAppUserId(id)).Returns(1);
            var result = _dutyService.GetDutyCountToBeCompletedByAppUserId(id);
            Assert.Equal(1, result);
        }
    }
}
