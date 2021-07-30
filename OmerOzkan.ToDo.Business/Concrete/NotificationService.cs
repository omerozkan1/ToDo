using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;

namespace OmerOzkan.ToDo.Business.Concrete
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationDal _notificationDal;

        public NotificationService(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public List<Notification> GetNotReadUsers(string appUserId)
        {
            return _notificationDal.GetNotReadUsers(appUserId);
        }

        public int GetNotReadCountByAppUserId(string appUserId)
        {
            return _notificationDal.GetNotReadCountByAppUserId(appUserId);
        }
    }
}
