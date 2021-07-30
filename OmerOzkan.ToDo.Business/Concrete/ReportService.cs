using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;

namespace OmerOzkan.ToDo.Business.Concrete
{
    public class ReportService : IReportService
    {
        private readonly IReportDal _reportDal;
        public ReportService(IReportDal reportDal)
        {
            _reportDal = reportDal;
        }

        public Report GetByReportId(int id)
        {
            return _reportDal.GetDutyById(id);
        }

        public int GetReportCount()
        {
            return _reportDal.GetReportCount();
        }

        public int GetReportCountByAppUserId(string id)
        {
            return _reportDal.GetReportCountByAppUserId(id);
        }
    }
}
