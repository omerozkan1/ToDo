using OmerOzkan.ToDo.DataAccess.Concrete.EfCore.Context;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;
using System.Linq;

namespace OmerOzkan.ToDo.DataAccess.Repositories
{
    public class NotificationDal : GenericDal<Notification>, INotificationDal
    {
        private readonly ToDoContext _context;
        public NotificationDal(ToDoContext context) : base(context)
        {
            _context = context;
        }

        public int GetNotReadCountByAppUserId(string appUserId)
        {
            return _context.Notifications.Count(I => I.AppUserId == appUserId && !I.Status);
        }

        public List<Notification> GetNotReadUsers(string appUserId)
        {
            return _context.Notifications.Where(I => I.AppUserId == appUserId && !I.Status).ToList();
        }
    }
}
