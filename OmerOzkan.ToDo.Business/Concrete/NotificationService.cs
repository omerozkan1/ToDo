using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Business.Concrete
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationDal _notificationDal;

        public NotificationService(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public async Task AddAsync(Notification entity)
        {
            await _notificationDal.AddAsync(entity);
        }

        public async Task<Notification> FindByIdAsync(int id)
        {
            return await _notificationDal.FindByIdAsync(id);
        }

        public async Task<List<Notification>> GetAllAsync()
        {
            return await _notificationDal.GetAllAsync();
        }

        public List<Notification> GetNotReadUsers(string appUserId)
        {
            return _notificationDal.GetNotReadUsers(appUserId);
        }

        public int GetNotReadCountByAppUserId(string appUserId)
        {
            return _notificationDal.GetNotReadCountByAppUserId(appUserId);
        }

        public async Task RemoveAsync(Notification entity)
        {
            await _notificationDal.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Notification entity)
        {
            await _notificationDal.UpdateAsync(entity);
        }
    }
}
