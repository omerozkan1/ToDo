using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Business.Concrete
{
    public class DutyService : IDutyService
    {
        private readonly IDutyDal _dutyDal;

        public DutyService(IDutyDal dutyDal)
        {
            _dutyDal = dutyDal;
        }

        public async Task AddAsync(Duty entity)
        {
           await _dutyDal.AddAsync(entity);
        }

        public async Task<Duty> FindByIdAsync(int id)
        {
            return await _dutyDal.FindByIdAsync(id);
        }

        public List<Duty> GetAll()
        {
            return _dutyDal.GetAll();
        }

        public List<Duty> GetAll(Expression<Func<Duty, bool>> filter)
        {
            return _dutyDal.GetAll(filter);
        }

        public Task<List<Duty>> GetAllAsync()
        {
            return _dutyDal.GetAllAsync();
        }

        public List<Duty> GetAllByIncomplete(out int totalPage, string userId, int activePage = 1)
        {
            return _dutyDal.GetAllByIncomplete(out totalPage, userId, activePage);
        }

        public List<Duty> GetByAppUserId(string appUserId)
        {
            return _dutyDal.GetByAppUserId(appUserId);
        }

        public List<Duty> GetByIncompleteWithUrgency()
        {
            return _dutyDal.GetByIncompleteWithUrgency();
        }

        public Duty GetByReportId(int id)
        {
            return _dutyDal.GetByReportId(id);
        }

        public Duty GetByUrgencyId(int id)
        {
            return _dutyDal.GetByUrgencyId(id);
        }

        public int GetDutyCountCompleteByAppUserId(string id)
        {
            return _dutyDal.GetDutyCountCompleteByAppUserId(id);
        }

        public int GetDutyCountCompleted()
        {
            return _dutyDal.GetDutyCountCompleted();
        }

        public int GetDutyCountPendingAssignment()
        {
            return _dutyDal.GetDutyCountPendingAssignment();
        }

        public int GetDutyCountToBeCompletedByAppUserId(string id)
        {
            return _dutyDal.GetDutyCountToBeCompletedByAppUserId(id);
        }

        public async Task RemoveAsync(Duty entity)
        {
            await _dutyDal.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Duty entity)
        {
            await _dutyDal.UpdateAsync(entity);
        }
    }
}
