using OmerOzkan.ToDo.Entities.Domains;

namespace OmerOzkan.ToDo.DataAccess.Interfaces
{
    public interface IReportDal : IGenericDal<Report>
    {
        Report GetDutyById(int id);
        int GetReportCountByAppUserId(string id);
        int GetReportCount();
    }
}
