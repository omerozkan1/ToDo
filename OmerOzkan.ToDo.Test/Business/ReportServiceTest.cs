using Moq;
using OmerOzkan.ToDo.Business.Concrete;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;
using System;
using Xunit;

namespace OmerOzkan.ToDo.Test.Business
{
    public class ReportServiceTest
    {
        private Mock<IReportDal> _mock;
        private ReportService _reportService;
        private Report _report;
        public ReportServiceTest()
        {
            _mock = new Mock<IReportDal>();
            _reportService = new ReportService(_mock.Object);
            _report = new Report() { Id = 1, Description = "test", CreatedDate = DateTime.Now, Detail = "detail", Status = true };
        }

        [Theory]
        [InlineData(1)]
        public void GetByReportId_MethodExecute(int id)
        {
            _mock.Setup(x => x.GetDutyById(id)).Returns(_report);

            var result = _reportService.GetByReportId(id);
            var returnValue = Assert.IsType<Report>(result);

            Assert.Equal(id, returnValue.Id);
            Assert.Equal(_report.Detail, returnValue.Detail);
            Assert.Equal(_report.Description, returnValue.Description);
        }

        [Fact]
        public void GetReportCount_MethodExecute()
        {
            _mock.Setup(x => x.GetReportCount()).Returns(1);
            var result = _reportService.GetReportCount();
            Assert.Equal(1, result);
        }

        [Theory]
        [InlineData("1")]
        public void GetReportCountByAppUserId_MethodExecute(string id)
        {
            _mock.Setup(x => x.GetReportCountByAppUserId(id)).Returns(1);
            var result = _reportService.GetReportCountByAppUserId(id);
            Assert.Equal(1, result);
        }
    }
}
