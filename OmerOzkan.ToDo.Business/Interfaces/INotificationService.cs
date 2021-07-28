using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;

namespace OmerOzkan.ToDo.Business.Interfaces
{
    public interface INotificationService : IGenericService<Notification>
    {
        List<Notification> GetNotReadUsers(string appUserId);
        int GetNotReadCountByAppUserId(string AppUserId);
    }
}
