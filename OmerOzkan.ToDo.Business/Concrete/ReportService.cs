using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Business.Concrete
{
    public class ReportService : IReportService
    {
        private readonly IReportDal _reportDal;
        public ReportService(IReportDal reportDal)
        {
            _reportDal = reportDal;
        }

        public async Task AddAsync(Report entity)
        {
            await _reportDal.AddAsync(entity);
        }

        public async Task<Report> FindByIdAsync(int id)
        {
            return await _reportDal.FindByIdAsync(id);
        }

        public async Task<List<Report>> GetAllAsync()
        {
            return await _reportDal.GetAllAsync();
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

        public async Task RemoveAsync(Report entity)
        {
            await _reportDal.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Report entity)
        {
            await _reportDal.UpdateAsync(entity);
        }
    }
}
