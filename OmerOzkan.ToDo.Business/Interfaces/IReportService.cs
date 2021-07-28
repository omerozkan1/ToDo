using OmerOzkan.ToDo.Entities.Domains;

namespace OmerOzkan.ToDo.Business.Interfaces
{
    public interface IReportService : IGenericService<Report>
    {
        Report GetByReportId(int id);
        int GetReportCountByAppUserId(string id);
        int GetReportCount();
    }
}
