using OmerOzkan.ToDo.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OmerOzkan.ToDo.Business.Interfaces
{
    public interface IDutyService : IGenericService<Duty>
    {
        List<Duty> GetByIncompleteWithUrgency();
        List<Duty> GetAll();
        Duty GetByUrgencyId(int id);
        List<Duty> GetByAppUserId(string appUserId);
        Duty GetByReportId(int id);
        List<Duty> GetAll(Expression<Func<Duty, bool>> filter);
        List<Duty> GetAllByIncomplete(out int totalPage, string userId, int activePage = 1);
        int GetDutyCountCompleteByAppUserId(string id);
        int GetDutyCountToBeCompletedByAppUserId(string id);
        int GetDutyCountPendingAssignment();
        int GetDutyCountCompleted();
    }
}
