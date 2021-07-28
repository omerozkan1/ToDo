using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Business.Concrete
{
    public class UrgencyService : IUrgencyService
    {
        private readonly IUrgencyDal _urgencyDal;

        public UrgencyService(IUrgencyDal urgencyDal)
        {
            _urgencyDal = urgencyDal;
        }
        public async Task AddAsync(Urgency entity)
        {
            await _urgencyDal.AddAsync(entity);
        }

        public async Task<Urgency> FindByIdAsync(int id)
        {
            return await _urgencyDal.FindByIdAsync(id);
        }

        public async Task<List<Urgency>> GetAllAsync()
        {
            return await _urgencyDal.GetAllAsync();
        }

        public async Task RemoveAsync(Urgency entity)
        {
            await _urgencyDal.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Urgency entity)
        {
            await _urgencyDal.UpdateAsync(entity);
        }
    }
}
