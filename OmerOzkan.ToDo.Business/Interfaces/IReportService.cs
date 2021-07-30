using OmerOzkan.ToDo.Entities.Domains;

namespace OmerOzkan.ToDo.Business.Interfaces
{
    public interface IReportService
    { 
        Report GetByReportId(int id);
        int GetReportCountByAppUserId(string id);
        int GetReportCount();
    }
}
