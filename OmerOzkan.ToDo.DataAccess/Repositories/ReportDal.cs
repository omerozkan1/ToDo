using Microsoft.EntityFrameworkCore;
using OmerOzkan.ToDo.DataAccess.Concrete.EfCore.Context;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;
using System.Linq;

namespace OmerOzkan.ToDo.DataAccess.Repositories
{
    public class ReportDal : GenericDal<Report>, IReportDal
    {
        private readonly ToDoContext _context;
        public ReportDal(ToDoContext context) : base(context)
        {
            _context = context;
        }
        public Report GetDutyById(int id)
        {
            return _context.Reports.Include(I => I.Duty).ThenInclude(I => I.Urgency).Where(I => I.Id == id).FirstOrDefault();
        }

        public int GetReportCount()
        {
            return _context.Reports.Count();
        }

        public int GetReportCountByAppUserId(string id)
        {
            var result = _context.Duties.Include(I => I.Reports).Where(I => I.AppUserId == id);
            return result.SelectMany(I => I.Reports).Count();
        }
    }
}
